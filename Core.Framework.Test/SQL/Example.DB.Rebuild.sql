

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_CreateDynamicVals]'))
DROP FUNCTION [civic].[udf_CreateDynamicVals]
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Split]'))
DROP FUNCTION [civic].[udf_Split]
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetSysDate]'))
DROP FUNCTION [civic].[udf_GetSysDate]
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[usp_ProcessFilter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [civic].[usp_ProcessFilter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [civic].[udf_GetSysDate]()
RETURNS datetime
AS
BEGIN
	RETURN getdate()
END
GO


CREATE FUNCTION [civic].[udf_Split] (
      @InputString                  VARCHAR(8000),
      @Delimiter                    VARCHAR(50)
)
 
RETURNS @Items TABLE (
	  Pos							int identity(1,1),
      Item                          VARCHAR(8000)
)
 
AS
BEGIN
      IF @Delimiter = ' '
      BEGIN
            SET @Delimiter = ','
            SET @InputString = REPLACE(@InputString, ' ', @Delimiter)
      END
 
      IF (@Delimiter IS NULL OR @Delimiter = '')
            SET @Delimiter = ','
 
--INSERT INTO @Items VALUES (@Delimiter) -- Diagnostic
--INSERT INTO @Items VALUES (@InputString) -- Diagnostic
 
      DECLARE @Item                 VARCHAR(8000)
      DECLARE @ItemList       VARCHAR(8000)
      DECLARE @DelimIndex     INT
 
      SET @ItemList = @InputString
      SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      WHILE (@DelimIndex != 0)
      BEGIN
            SET @Item = SUBSTRING(@ItemList, 0, @DelimIndex)
            INSERT INTO @Items VALUES (@Item)
 
            -- Set @ItemList = @ItemList minus one less item
            SET @ItemList = SUBSTRING(@ItemList, @DelimIndex+1, LEN(@ItemList)-@DelimIndex)
            SET @DelimIndex = CHARINDEX(@Delimiter, @ItemList, 0)
      END -- End WHILE
 
      IF @Item IS NOT NULL -- At least one delimiter was encountered in @InputString
      BEGIN
            SET @Item = @ItemList
            INSERT INTO @Items VALUES (@Item)
      END
 
      -- No delimiters were encountered in @InputString, so just return @InputString
      ELSE INSERT INTO @Items VALUES (@InputString)
 
      RETURN
 
END -- End Function

GO


CREATE FUNCTION [civic].[udf_CreateDynamicVals] (
      @name VARCHAR(50),
      @count int
) RETURNS NVARCHAR(MAX) 
AS
BEGIN
	DECLARE @RETVAL NVARCHAR(MAX)
	SET @RETVAL=' '

	DECLARE @COUNT2 int
	SET @COUNT2=0
	WHILE @COUNT2<@COUNT
	BEGIN
		SET @COUNT2=@COUNT2+1
		SET @RETVAL=LTRIM(@RETVAL)+',@_'+@NAME+CONVERT(NVARCHAR(50),@COUNT2) + ' nvarchar(512)'
	END
	
	RETURN SUBSTRING(@RETVAL,2,DATALENGTH(@RETVAL)-1)
END

GO
CREATE PROCEDURE [civic].[usp_ProcessFilter]
     @skip int
    ,@count int out
	,@select nvarchar(max)
	,@where nvarchar(max) = null
	,@filterBy nvarchar(512)
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @POS int
	DECLARE @POS2 int
	DECLARE @SQL nvarchar(max),@SQLCOUNT nvarchar(max)
	DECLARE @WHEREEXT NVARCHAR(MAX)
	DECLARE @order nvarchar(max)

	set @WHEREEXT = @where

	set @skip = isnull(@skip,0)
	set @count = isnull(@count,100)
	set @orderBy = isnull(@orderBy,'')
	set @filterBy = isnull(@filterBy,'')
	
	DECLARE @BLOCKS TABLE(pos int,blk nvarchar(512))

	--- escape out space in a string query
	SET @filterBy=REPLACE(@filterBy,' ','___')
	insert into @BLOCKS
	select [POS],[item] from [civic].[udf_Split](@filterBy,'''')

	UPDATE @BLOCKS SET blk=replace(blk,'___','###')
	WHERE (POS % 2) = 0

	SET @filterBy = ''
	SELECT @filterBy=@filterBy+blk FROM @BLOCKS
	SET @filterBy=REPLACE(LTRIM(@filterBy),'___',' ')
	DELETE FROM @BLOCKS

	-- parse and determine filter by
	set @filterBy=replace(replace(replace(replace(replace(replace(replace(upper(@filterBy),' like ','|_like_|'),' eq ','|=|'),' lt ','|<|'),' gt ','|>|'),' ge ','|>=|'),' le ','|<=|'),' ne ','|<>|')
	insert into @BLOCKS
	select [POS],[item] from [civic].[udf_Split](@filterBy,' ')

	DECLARE @COMBINEVARS TABLE(pos int,varname nvarchar(512),operation nvarchar(512),varvalue nvarchar(512))
	DECLARE @OPERATIONS TABLE(pos int,operation nvarchar(512))
	
	--SELECT * FROM @BLOCKS

	WHILE(EXISTS(SELECT * FROM @BLOCKS))
	BEGIN
		DECLARE @PAIR NVARCHAR(512)
		SELECT TOP 1 @POS=POS,@PAIR=BLK FROM @BLOCKS

		IF CHARINDEX('|',@PAIR)>0 
		BEGIN
			DECLARE @TEMP TABLE(
				POS int
				,ITEM nvarchar(256)
			)
			INSERT INTO @TEMP
			select POS,[item] from [civic].[udf_Split](REPLACE(@PAIR,'|_LIKE_|','| LIKE |'),'|')

			DECLARE @VARNAME NVARCHAR(512)
			select TOP 1 @VARNAME=[item] from @TEMP WE

			DECLARE @OPERATION NVARCHAR(50) 
			select @OPERATION=[item] from @TEMP WHERE POS=2 ORDER BY [POS]

			DECLARE @VARVALUE NVARCHAR(512) 
			select @VARVALUE=[item] from @TEMP WHERE POS=3 ORDER BY [POS]
	
			IF NOT EXISTS(SELECT * FROM @COMBINEVARS WHERE varname=@VARNAME)
				BEGIN
					INSERT INTO @COMBINEVARS
					SELECT @POS,@VARNAME,@OPERATION,@VARVALUE
				END
			ELSE 
				BEGIN
					INSERT INTO @COMBINEVARS
					SELECT @POS,@VARNAME,@OPERATION,@VARVALUE
					
					DELETE FROM @BLOCKS 
					WHERE POS = @POS			
				END
		END
		ELSE 
		BEGIN
			INSERT INTO @OPERATIONS
			SELECT @POS,@PAIR
		END

		DELETE FROM @TEMP
		DELETE FROM @BLOCKS WHERE @POS=POS
	END
	
	-- trim leading and trailing single quotes
	WHILE EXISTS(SELECT * FROM @COMBINEVARS WHERE PATINDEX('%''',varvalue)>0) OR EXISTS(SELECT * FROM @COMBINEVARS WHERE PATINDEX('''%',varvalue)>0)
	BEGIN
		UPDATE @COMBINEVARS SET varvalue=SUBSTRING(varvalue,1,LEN(varvalue)-1)
		WHERE PATINDEX('%''',varvalue)>0

		UPDATE @COMBINEVARS SET varvalue=SUBSTRING(varvalue,2,LEN(varvalue)-1)
		WHERE PATINDEX('''%',varvalue)>0
	END

	-- build where clause
	SET @WHERE = ''
	IF EXISTS(SELECT * FROM @OPERATIONS)
	BEGIN
		WHILE EXISTS(SELECT * FROM @OPERATIONS)
		BEGIN
			SELECT @POS=POS,@OPERATION=operation FROM @OPERATIONS

			SELECT @WHERE=@WHERE+[VARNAME]+[OPERATION]+'@_val'+CONVERT(NVARCHAR(50),[POS]) FROM @COMBINEVARS
			WHERE POS<@POS
	
			SET @WHERE = @WHERE + ' ' + @OPERATION + ' '

			SELECT @WHERE=@WHERE+[VARNAME]+[OPERATION]+'@_val'+CONVERT(NVARCHAR(50),[POS]) FROM @COMBINEVARS
			WHERE POS>@POS

			DELETE FROM @OPERATIONS WHERE @POS=POS
		END
	END
	ELSE 
	BEGIN
		SELECT TOP 1 @WHERE=[VARNAME]+[OPERATION]+'@_val'+CONVERT(NVARCHAR(50),[POS]) FROM @COMBINEVARS
	END

	-- unescape string spaces
	UPDATE @COMBINEVARS SET varvalue=replace(varvalue,'###',' ')

	-- build dynamic order clause
	IF @orderBy is not null and @orderBy<>''
	BEGIN
		SET @order=''
		DECLARE @orderbylist table (orderby nvarchar(512),[desc] bit) 
		INSERT INTO @orderbylist
		SELECT [item],0 FROM [civic].[udf_Split](@orderBy,',')
		UPDATE @orderbylist SET ORDERBY=SUBSTRING(ORDERBY,1,LEN(ORDERBY)-5),[DESC]=1 WHERE PATINDEX('%_DESC',ORDERBY)>0
		UPDATE @orderbylist SET ORDERBY=SUBSTRING(ORDERBY,1,LEN(ORDERBY)-4) WHERE PATINDEX('%_ASC',ORDERBY)>0
		SELECT @order = @order + ',' + orderby + CASE WHEN [DESC]=1 THEN ' DESC' ELSE '' END FROM @orderbylist
		SET @order=LTRIM(@order)

		IF @order<>''
		BEGIN
			SET @order=CHAR(13) + CHAR(10) + '    ORDER BY '+ LTRIM(RTRIM(SUBSTRING(@order,2,LEN(@order))))
		END
	END
	ELSE
	BEGIN
		SET @SQL = @select 
		SET @order = ' ORDER BY getdate()'
	END

	-- build complete dyn sql statement
	IF @WHERE<>'' 
	BEGIN
		IF @WHEREEXT IS NOT NULL
		BEGIN
			SET @WHERE = ' AND (' + @where + ')'
		END
		ELSE 
		BEGIN
			SET @WHERE = ' WHERE ' + @WHERE
		END
	END
		 
	SET @SQLCOUNT = @select + @where 
	SET @SQL = @SQLCOUNT + @order +' OFFSET @_skip ROWS FETCH NEXT @_count ROWS ONLY'

	DECLARE @PARAMS NVARCHAR(MAX)
	SET @PARAMS = N'@_skip int,@_count int,@_orderBy nvarchar(512),'+civic.udf_CreateDynamicVals('val',20)
	DECLARE @val1 nvarchar(512)
			, @val2 nvarchar(512)
			, @val3 nvarchar(512)
			, @val4 nvarchar(512)
			, @val5 nvarchar(512)
			, @val6 nvarchar(512)
			, @val7 nvarchar(512)
			, @val8 nvarchar(512)
			, @val9 nvarchar(512)
			, @val10 nvarchar(512)
			, @val11 nvarchar(512)
			, @val12 nvarchar(512)
			, @val13 nvarchar(512)
			, @val14 nvarchar(512)
			, @val15 nvarchar(512)
			, @val16 nvarchar(512)
			, @val17 nvarchar(512)
			, @val18 nvarchar(512)
			, @val19 nvarchar(512)
			, @val20 nvarchar(512)
	
	SELECT @val1 = varvalue FROM @COMBINEVARS WHERE POS = 1
	SELECT @val2  = varvalue FROM @COMBINEVARS WHERE POS = 2 
	SELECT @val3  = varvalue FROM @COMBINEVARS WHERE POS = 3 
	SELECT @val4  = varvalue FROM @COMBINEVARS WHERE POS = 4 
	SELECT @val5  = varvalue FROM @COMBINEVARS WHERE POS = 5 
	SELECT @val6  = varvalue FROM @COMBINEVARS WHERE POS = 6 
	SELECT @val7  = varvalue FROM @COMBINEVARS WHERE POS = 7 
	SELECT @val8  = varvalue FROM @COMBINEVARS WHERE POS = 8 
	SELECT @val9  = varvalue FROM @COMBINEVARS WHERE POS = 9 
	SELECT @val10 = varvalue FROM @COMBINEVARS WHERE POS = 10
	SELECT @val11 = varvalue FROM @COMBINEVARS WHERE POS = 11
	SELECT @val12 = varvalue FROM @COMBINEVARS WHERE POS = 12
	SELECT @val13 = varvalue FROM @COMBINEVARS WHERE POS = 13
	SELECT @val14 = varvalue FROM @COMBINEVARS WHERE POS = 14
	SELECT @val15 = varvalue FROM @COMBINEVARS WHERE POS = 15
	SELECT @val16 = varvalue FROM @COMBINEVARS WHERE POS = 16
	SELECT @val17 = varvalue FROM @COMBINEVARS WHERE POS = 17
	SELECT @val18 = varvalue FROM @COMBINEVARS WHERE POS = 18
	SELECT @val19 = varvalue FROM @COMBINEVARS WHERE POS = 19
	SELECT @val20 = varvalue FROM @COMBINEVARS WHERE POS = 20

	exec dbo.sp_executesql   @sql
							,@PARAMS=@params
							,@_skip=@skip
							,@_count=@count
							,@_orderBy=@orderBy
							,@_val1=@val1
							,@_val2=@val2
							,@_val3=@val3
							,@_val4=@val4
							,@_val5=@val5
							,@_val6=@val6
							,@_val7=@val7
							,@_val8=@val8
							,@_val9=@val9
							,@_val10=@val10
							,@_val11=@val11
							,@_val12=@val12
							,@_val13=@val13
							,@_val14=@val14
							,@_val15=@val15
							,@_val16=@val16
							,@_val17=@val17
							,@_val18=@val18
							,@_val19=@val19
							,@_val20=@val20

	IF @retcount=1
	BEGIN
		SET @SQL = LTRIM(RTRIM(@sqlcount))
		SET @SQL = LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(@SQLCOUNT,'	',' '),CHAR(10),' '),CHAR(13),' ')))
		SET @SQL = 'SELECT COUNT(*) ' + SUBSTRING(@SQL,@POS,LEN(@SQL)-@POS+1)
	
		DECLARE @counttable table ([count] int)
		insert into @counttable([count])
		exec dbo.sp_executesql @sql
						,@PARAMS=@params
						,@_skip=@skip
						,@_count=@count
						,@_orderBy=@orderBy
						,@_val1=@val1
						,@_val2=@val2
						,@_val3=@val3
						,@_val4=@val4
						,@_val5=@val5
						,@_val6=@val6
						,@_val7=@val7
						,@_val8=@val8
						,@_val9=@val9
						,@_val10=@val10
						,@_val11=@val11
						,@_val12=@val12
						,@_val13=@val13
						,@_val14=@val14
						,@_val15=@val15
						,@_val16=@val16
						,@_val17=@val17
						,@_val18=@val18
						,@_val19=@val19
						,@_val20=@val20

		SELECT @COUNT = [COUNT] FROM @counttable
	END
END
GO

CREATE DEFAULT [civic].[udf_GetDate]
AS GETUTCDATE()
GO


CREATE DEFAULT [civic].[udf_Unknown]
AS 'UNK'
GO

CREATE DEFAULT [civic].[udf_Yes]
AS 1
GO

CREATE DEFAULT [civic].[udf_No]
AS 0
GO

CREATE DEFAULT [civic].[udf_Zero]
AS 0
GO
-- t4-defaults begin
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity2].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity3].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[InstallationEnvironment].[Modified]';
-- t4-defaults end
GO

CREATE PROCEDURE [dbo].[usp_Entity1Add]
-- t4-params begin
	  @name [nvarchar](max) out
	, @environmentID [int]
	, @dte [datetime]
	, @dte2 [datetime]
	, @dble1 [decimal](20,4)
	, @dec1 [decimal](20,4)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity1](
-- t4-columns begin
		 [Name]
		,[EnvironmentID]
		,[Dte]
		,[Dte2]
		,[Dble1]
		,[Dec1]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @name
		,@environmentID
		,@dte
		,@dte2
		,@dble1
		,@dec1
-- t4-values end
	)


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Modify]
	  @name [nvarchar](max)
	, @environmentID [int]
	, @dte [datetime]
	, @dte2 [datetime]
	, @dble1 [decimal](20,4)
	, @dec1 [decimal](20,4)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e1] SET 
		-- t4-columns begin
		 [Name] = @name
		,[EnvironmentID] = @environmentID
		,[Dte] = @dte
		,[Dte2] = @dte2
		,[Dble1] = @dble1
		,[Dec1] = @dec1
		-- t4-columns end
	FROM [dbo].[Entity1] [e1]
	WHERE	
		-- t4-where begin
	    [e1].[Name] = @name
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Remove]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity1]
	WHERE	
		-- t4-where begin
	    [Name] = @name
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY1]
AS

	SELECT	
		-- t4-columns begin
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
	
		-- t4-columns end
	FROM [dbo].[Entity1] [e1]
GO

	CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @someID [int] out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
	, @ouid [varchar](32)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @oid [int]
	SELECT @oid = [OID]
	FROM [civic].[OrgUnit]
	WHERE [OUID] = @ouid

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
		,[OID]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
		,@oid
-- t4-values end
	)

SET @someID = SCOPE_IDENTITY()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @someID [int]
	, @ff [nvarchar](max)
	, @otherDate [datetime]
	, @ouid [varchar](32)
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @oid [int]
		SELECT @oid = [OID]
		FROM [civic].[OrgUnit]
		WHERE [OUID] = @ouid
	
	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		,[OID] = @oid
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Remove]
	  @someID [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity2]
	WHERE	
		-- t4-where begin
	    [SomeID] = @someID
	AND [ff] = @ff
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY2]
AS

	SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		,[e2].[OID]
		,[ou].[OUID]
	
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	LEFT JOIN [civic].[OrgUnit] as [ou] ON [e2].[OID] = [ou].[OID]
GO

	CREATE PROCEDURE [dbo].[usp_Entity3Add]
-- t4-params begin
	  @someUID [nvarchar](max)
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](
-- t4-columns begin
		 [Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 [civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someUID = SCOPE_IDENTITY()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]
	  @someUID [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin
		 [Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Remove]
	  @someUID [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity3]
	WHERE	
		-- t4-where begin
	    [SomeUID] = @someUID
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY3]
AS

	SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[SomeID]
		,[e3].[Modified]
		,[e3].[OtherDate]
	
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
GO

	CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @name [nvarchar](max)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Environment](
-- t4-columns begin
		 [Name]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @name
-- t4-values end
	)


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e] SET 
		-- t4-columns begin
		 [Name] = @name
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Environment]
	WHERE	
		-- t4-where begin
	    [ID] = @id
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENVIRONMENT]
AS

	SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
	
		-- t4-columns end
	FROM [dbo].[Environment] [e]
GO

	CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentAdd]
-- t4-params begin
	  @environmentCode [varchar](20) out
	, @name [nvarchar](100)
	, @description [nvarchar](max)
	, @isVisible [varchar](1)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[InstallationEnvironment](
-- t4-columns begin
		 [EnvironmentCode]
		,[Name]
		,[Description]
		,[IsVisible]
		,[Modified]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @environmentCode
		,@name
		,@description
		,@isVisible
		,[civic].udf_getSysDate()
-- t4-values end
	)


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentModify]
	  @environmentCode [varchar](20)
	, @name [nvarchar](100)
	, @description [nvarchar](max)
	, @isVisible [varchar](1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [ie] SET 
		-- t4-columns begin
		 [EnvironmentCode] = @environmentCode
		,[Name] = @name
		,[Description] = @description
		,[IsVisible] = @isVisible
		,[Modified] = [civic].udf_getSysDate()
		-- t4-columns end
	FROM [dbo].[InstallationEnvironment] [ie]
	WHERE	
		-- t4-where begin
	    [ie].[EnvironmentCode] = @environmentCode
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentRemove]
	  @environmentCode [varchar](20)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[InstallationEnvironment]
	WHERE	
		-- t4-where begin
	    [EnvironmentCode] = @environmentCode
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_INSTALLATIONENVIRONMENT]
AS

	SELECT	
		-- t4-columns begin
		 [ie].[EnvironmentCode]
		,[ie].[Name]
		,[ie].[Description]
		,[ie].[IsVisible]
		,[ie].[Modified]
	
		-- t4-columns end
	FROM [dbo].[InstallationEnvironment] [ie]
GO

	
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Dev');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('QA');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Load');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Stage');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Prod');

GO

