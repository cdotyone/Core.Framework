



/*

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetDate]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_GetDate]
GO

CREATE DEFAULT [civic].[udf_GetDate]
AS GETUTCDATE()
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Unknown]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Unknown]
GO

CREATE DEFAULT [civic].[udf_Unknown]
AS 'UNK'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Yes]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Yes]
GO

CREATE DEFAULT [civic].[udf_Yes]
AS 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_No]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_No]
GO

CREATE DEFAULT [civic].[udf_No]
AS 0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Zero]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Zero]
GO

CREATE DEFAULT [civic].[udf_Zero]
AS 0
GO
-- t4-defaults begin
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity2].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity3].[Modified]';
-- t4-defaults end
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Get]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

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
	WHERE	
		-- t4-where begin
	    [e1].[Name] = @name
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
		-- t4-columns end
    FROM [dbo].[Entity1] [e1]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Modify]
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Remove]
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Get]
	  @someID [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity2] [e2]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @someID [int] out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @someID [int]
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Remove]
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Get]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity3] [e3]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Add]
-- t4-params begin
	  @someUID [nvarchar](max) out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someUID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Remove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Remove]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity3]
	WHERE	
		-- t4-where begin
	    [SomeUID] = @someUID
	AND [ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGet]
	  @id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
    FROM [dbo].[Environment] [e]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentAdd]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @id [int] out
	, @name [nvarchar](max)
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

SET @id = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentModify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @id [int]
	, @name [nvarchar](max)
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentRemove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
	  @id [int]
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetDate]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_GetDate]
GO

CREATE DEFAULT [civic].[udf_GetDate]
AS GETUTCDATE()
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Unknown]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Unknown]
GO

CREATE DEFAULT [civic].[udf_Unknown]
AS 'UNK'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Yes]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Yes]
GO

CREATE DEFAULT [civic].[udf_Yes]
AS 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_No]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_No]
GO

CREATE DEFAULT [civic].[udf_No]
AS 0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Zero]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Zero]
GO

CREATE DEFAULT [civic].[udf_Zero]
AS 0
GO
-- t4-defaults begin
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity2].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity3].[Modified]';
-- t4-defaults end
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Get]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

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
	WHERE	
		-- t4-where begin
	    [e1].[Name] = @name
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
		-- t4-columns end
    FROM [dbo].[Entity1] [e1]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Modify]
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Remove]
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

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Get]
	  @someID [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity2] [e2]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @someID [int] out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @someID [int]
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Remove]
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

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Get]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity3] [e3]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Add]
-- t4-params begin
	  @someUID [nvarchar](max) out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someUID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Remove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Remove]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity3]
	WHERE	
		-- t4-where begin
	    [SomeUID] = @someUID
	AND [ff] = @ff
		-- t4-where end
END
GO

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGet]
	  @id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
    FROM [dbo].[Environment] [e]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentAdd]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @id [int] out
	, @name [nvarchar](max)
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

SET @id = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentModify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @id [int]
	, @name [nvarchar](max)
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentRemove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
	  @id [int]
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

	INSERT INTO [dbo].[Environments]([Name]) VALUES ('Dev');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('QA');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Load');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Stage');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Prod');

GO


IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetSysDate]'))
DROP FUNCTION [civic].[udf_GetSysDate]
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Get]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

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
	WHERE	
		-- t4-where begin
	    [e1].[Name] = @name
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
		-- t4-columns end
    FROM [dbo].[Entity1] [e1]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Modify]
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Remove]
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

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Get]
	  @someID [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity2] [e2]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @someID [int] out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @someID [int]
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Remove]
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

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Get]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3GetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[ff]
		,[e3].[Modified]
		,[e3].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity3] [e3]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Add]
-- t4-params begin
	  @someUID [nvarchar](max) out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someUID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
	AND [e3].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Remove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Remove]
	  @someUID [nvarchar](max)
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity3]
	WHERE	
		-- t4-where begin
	    [SomeUID] = @someUID
	AND [ff] = @ff
		-- t4-where end
END
GO

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGet]
	  @id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
    FROM [dbo].[Environment] [e]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentAdd]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @id [int] out
	, @name [nvarchar](max)
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

SET @id = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentModify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @id [int]
	, @name [nvarchar](max)
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
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentRemove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
	  @id [int]
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

	
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static Entity1Entity GetEntity1( String name, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var entity1Returned = new Entity1Entity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Get"))
    		{
    			command.AddInParameter("@name", name);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEntity1(entity1Returned, dataReader))
                        {
    					entity1Returned.Name = name;
    					                    }
                        else entity1Returned = null;
                    });
    		}
    
    		return entity1Returned;
    	}
    
    	internal static List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity1Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1GetFiltered"))
    		{
                command.AddInParameter("@skip", skip);			
                command.AddInParameter("@retcount", retCount);
    			if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    			command.AddInParameter("@orderBy", orderBy);
        		command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
                command.ExecuteReader(dataReader =>
                    {
    					var item = new Entity1Entity();
    					while(populateEntity1(item, dataReader))
    					{
    						list.Add(item);
    						item = new Entity1Entity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static void AddEntity1(Entity1Entity entity1, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Add"))
    		{
    			buildEntity1CommandParameters( entity1, command, true );
    			command.ExecuteNonQuery();
    		}
    	}
    
    	internal static List<Entity1Entity> ModifyEntity1(Entity1Entity entity1, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity1Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Modify"))
    		{
    			buildEntity1CommandParameters( entity1, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEntity1( String name, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity1Remove"))
    		{
    			command.AddInParameter("@name", name);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEntity1CommandParameters( Entity1Entity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@name", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","entity1","name",entity.Name));
    		else command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","entity1","name",entity.Name));
    		command.AddInParameter("@environmentid", entity.EnvironmentID);
    		command.AddInParameter("@dte", entity.Dte.ToDB());
    		command.AddInParameter("@dte2", entity.Dte2.ToDB());
    		command.AddInParameter("@dble1", entity.Dble1);
    		command.AddInParameter("@dec1", entity.Dec1);
    
    	}
    	
    	private static bool populateEntity1(Entity1Entity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    							
    		entity.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;						
    		entity.EnvironmentID = dataReader["EnvironmentID"] != null && !(dataReader["EnvironmentID"] is DBNull) ? Int32.Parse(dataReader["EnvironmentID"].ToString()) : 0;					
    		if(!(dataReader["Dte"] is DBNull)) entity.Dte = DateTime.Parse(dataReader["Dte"].ToString()).FromDB();					
    		if(!(dataReader["Dte2"] is DBNull)) entity.Dte2 = DateTime.Parse(dataReader["Dte2"].ToString()).FromDB();						
    		entity.Dble1 = double.Parse(dataReader["Dble1"] != null && !(dataReader["Dble1"] is DBNull) && dataReader["Dble1"] != null ? dataReader["Dble1"].ToString() : "0");						
    		entity.Dec1 = double.Parse(dataReader["Dec1"] != null && !(dataReader["Dec1"] is DBNull) && dataReader["Dec1"] != null ? dataReader["Dec1"].ToString() : "0");		
    			return true;
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static Entity2Entity GetEntity2( Int32 someID,  String ff, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var entity2Returned = new Entity2Entity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Get"))
    		{
    			command.AddInParameter("@someID", someID);
    			command.AddInParameter("@ff", ff);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEntity2(entity2Returned, dataReader))
                        {
    					entity2Returned.SomeID = someID;
    					entity2Returned.ff = ff;
    					                    }
                        else entity2Returned = null;
                    });
    		}
    
    		return entity2Returned;
    	}
    
    	internal static List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity2Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2GetFiltered"))
    		{
                command.AddInParameter("@skip", skip);			
                command.AddInParameter("@retcount", retCount);
    			if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    			command.AddInParameter("@orderBy", orderBy);
        		command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
                command.ExecuteReader(dataReader =>
                    {
    					var item = new Entity2Entity();
    					while(populateEntity2(item, dataReader))
    					{
    						list.Add(item);
    						item = new Entity2Entity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static void AddEntity2(Entity2Entity entity2, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Add"))
    		{
    			buildEntity2CommandParameters( entity2, command, true );
    			command.ExecuteNonQuery();
    			entity2.SomeID = Int32.Parse(
    			command.GetOutParameter("@someid").Value.ToString());
    		}
    	}
    
    	internal static List<Entity2Entity> ModifyEntity2(Entity2Entity entity2, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity2Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Modify"))
    		{
    			buildEntity2CommandParameters( entity2, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEntity2( Int32 someID, String ff, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity2Remove"))
    		{
    			command.AddInParameter("@someID", someID);
    			command.AddInParameter("@ff", ff);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEntity2CommandParameters( Entity2Entity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@someid", ParameterDirection.InputOutput,  entity.SomeID);
    		else command.AddInParameter("@someid", entity.SomeID);
       		if(addRecord) command.AddParameter("@ff", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","entity2","ff",entity.ff));
    		else command.AddInParameter("@ff", T4Config.CheckUpperCase("dbo","entity2","ff",entity.ff));
    		command.AddInParameter("@otherdate", entity.OtherDate.ToDB());
    
    	}
    	
    	private static bool populateEntity2(Entity2Entity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    								
    		entity.SomeID = dataReader["SomeID"] != null && !(dataReader["SomeID"] is DBNull) ? Int32.Parse(dataReader["SomeID"].ToString()) : 0;					
    		entity.ff = dataReader["ff"] != null && !string.IsNullOrEmpty(dataReader["ff"].ToString()) ? dataReader["ff"].ToString() : string.Empty;					
    		if(!(dataReader["Modified"] is DBNull)) entity.Modified = DateTime.Parse(dataReader["Modified"].ToString()).FromDB();					
    		if(!(dataReader["OtherDate"] is DBNull)) entity.OtherDate = DateTime.Parse(dataReader["OtherDate"].ToString()).FromDB();		
    			return true;
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static Entity3Entity GetEntity3( String someUID,  String ff, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var entity3Returned = new Entity3Entity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Get"))
    		{
    			command.AddInParameter("@someUID", someUID);
    			command.AddInParameter("@ff", ff);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEntity3(entity3Returned, dataReader))
                        {
    					entity3Returned.SomeUID = someUID;
    					entity3Returned.ff = ff;
    					                    }
                        else entity3Returned = null;
                    });
    		}
    
    		return entity3Returned;
    	}
    
    	internal static List<Entity3Entity> GetPagedEntity3(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity3Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3GetFiltered"))
    		{
                command.AddInParameter("@skip", skip);			
                command.AddInParameter("@retcount", retCount);
    			if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    			command.AddInParameter("@orderBy", orderBy);
        		command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
                command.ExecuteReader(dataReader =>
                    {
    					var item = new Entity3Entity();
    					while(populateEntity3(item, dataReader))
    					{
    						list.Add(item);
    						item = new Entity3Entity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static void AddEntity3(Entity3Entity entity3, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Add"))
    		{
    			buildEntity3CommandParameters( entity3, command, true );
    			command.ExecuteNonQuery();
    		}
    	}
    
    	internal static List<Entity3Entity> ModifyEntity3(Entity3Entity entity3, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<Entity3Entity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Modify"))
    		{
    			buildEntity3CommandParameters( entity3, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEntity3( String someUID, String ff, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_Entity3Remove"))
    		{
    			command.AddInParameter("@someUID", someUID);
    			command.AddInParameter("@ff", ff);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEntity3CommandParameters( Entity3Entity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@someuid", ParameterDirection.InputOutput,  entity.SomeUID.ToUpper());
    		else command.AddInParameter("@someuid", entity.SomeUID.ToUpper());
       		if(addRecord) command.AddParameter("@ff", ParameterDirection.InputOutput,  T4Config.CheckUpperCase("dbo","entity3","ff",entity.ff));
    		else command.AddInParameter("@ff", T4Config.CheckUpperCase("dbo","entity3","ff",entity.ff));
    		command.AddInParameter("@otherdate", entity.OtherDate.ToDB());
    
    	}
    	
    	private static bool populateEntity3(Entity3Entity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    							
    		entity.SomeUID = dataReader["SomeUID"] != null && !string.IsNullOrEmpty(dataReader["SomeUID"].ToString()) ? dataReader["SomeUID"].ToString() : string.Empty;					
    		entity.ff = dataReader["ff"] != null && !string.IsNullOrEmpty(dataReader["ff"].ToString()) ? dataReader["ff"].ToString() : string.Empty;					
    		if(!(dataReader["Modified"] is DBNull)) entity.Modified = DateTime.Parse(dataReader["Modified"].ToString()).FromDB();					
    		if(!(dataReader["OtherDate"] is DBNull)) entity.OtherDate = DateTime.Parse(dataReader["OtherDate"].ToString()).FromDB();		
    			return true;
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Civic.Core.Data;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Configuration;
using Civic.Framework.WebApi.Test.Entities;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;
namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    
    	internal static EnvironmentEntity GetEnvironment( Int32 id, IDBConnection database)
    	{
            Debug.Assert(database!=null);
    
    		var environmentReturned = new EnvironmentEntity();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGet"))
    		{
    			command.AddInParameter("@id", id);
    			
                command.ExecuteReader(dataReader =>
                    {
                        if (populateEnvironment(environmentReturned, dataReader))
                        {
    					environmentReturned.ID = id;
    					                    }
                        else environmentReturned = null;
                    });
    		}
    
    		return environmentReturned;
    	}
    
    	internal static List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<EnvironmentEntity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentGetFiltered"))
    		{
                command.AddInParameter("@skip", skip);			
                command.AddInParameter("@retcount", retCount);
    			if(!string.IsNullOrEmpty(filterBy)) command.AddInParameter("@filterBy", filterBy);
    			command.AddInParameter("@orderBy", orderBy);
        		command.AddParameter("@count", ParameterDirection.InputOutput, count);
    			
                command.ExecuteReader(dataReader =>
                    {
    					var item = new EnvironmentEntity();
    					while(populateEnvironment(item, dataReader))
    					{
    						list.Add(item);
    						item = new EnvironmentEntity();
    					} 
                    });
    
    			if (retCount) count = int.Parse(command.GetOutParameter("@count").Value.ToString());
    		}
    
    		return list;
    	}
    
    	internal static int AddEnvironment(EnvironmentEntity environment, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentAdd"))
    		{
    			buildEnvironmentCommandParameters( environment, command, true );
    			command.ExecuteNonQuery();
    			 
    			return environment.ID = Int32.Parse(
    			command.GetOutParameter("@id").Value.ToString());
    		}
    	}
    
    	internal static List<EnvironmentEntity> ModifyEnvironment(EnvironmentEntity environment, IDBConnection database)
    	{ 
            Debug.Assert(database!=null);
    
    		var list = new List<EnvironmentEntity>();
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentModify"))
    		{
    			buildEnvironmentCommandParameters( environment, command, false );
    			command.ExecuteNonQuery();
    		}
    
    		return list;
    	}
    
    	internal static void RemoveEnvironment( Int32 id, IDBConnection database )
    	{
            Debug.Assert(database!=null);
    
    		using (var command = database.CreateStoredProcCommand("dbo","usp_EnvironmentRemove"))
    		{
    			command.AddInParameter("@id", id);
    			command.ExecuteNonQuery();
    		}
    	}
    
    	private static void buildEnvironmentCommandParameters( EnvironmentEntity entity, IDBCommand command, bool addRecord )
    	{ 
            Debug.Assert(command!=null);
       		if(addRecord) command.AddParameter("@id", ParameterDirection.InputOutput,  entity.ID);
    		else command.AddInParameter("@id", entity.ID);
    		command.AddInParameter("@name", T4Config.CheckUpperCase("dbo","environment","name",entity.Name, false));
    
    	}
    	
    	private static bool populateEnvironment(EnvironmentEntity entity, IDataReader dataReader)
    	{
    		if (dataReader==null || !dataReader.Read()) return false;
    								
    		entity.ID = dataReader["ID"] != null && !(dataReader["ID"] is DBNull) ? Int32.Parse(dataReader["ID"].ToString()) : 0;					
    		entity.Name = dataReader["Name"] != null && !string.IsNullOrEmpty(dataReader["Name"].ToString()) ? dataReader["Name"].ToString() : string.Empty;		
    			return true;
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Civic.Core.Data;
using Civic.Framework.WebApi.Test.Entities;

namespace Civic.Framework.WebApi.Test.Data
{
    internal partial class ExampleData
    {
    }
}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Core.Data;
using Civic.Framework.WebApi;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="entity1")]
    public partial class Entity1 : IEntity
    {
    	[DataMember(Name="name")]
        public string Name { get; set; }
    
    	[DataMember(Name="environmentID")]
        public int EnvironmentID { get; set; }
    
    	[DataMember(Name="dte")]
        public System.DateTime Dte { get; set; }
    
    	[DataMember(Name="dte2")]
        public Nullable<System.DateTime> Dte2 { get; set; }
    
    	[DataMember(Name="dble1")]
        public double Dble1 { get; set; }
    
    	[DataMember(Name="dec1")]
        public double Dec1 { get; set; }
    
    
        public Entity1 Copy()
        {
            var copy = new Entity1
                {
    			Name = Name
    			,EnvironmentID = EnvironmentID
    			,Dte = Dte
    			,Dte2 = Dte2
    			,Dble1 = Dble1
    			,Dec1 = Dec1
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    					return null;
    		}
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEntity1(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEntity1(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEntity1(Name );
        }
    
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Core.Data;
using Civic.Framework.WebApi;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="entity2")]
    public partial class Entity2 : IEntity
    {
    	[DataMember(Name="someID")]
        public int SomeID { get; set; }
    
    	[DataMember(Name="ff")]
        public string ff { get; set; }
    
    	[DataMember(Name="modified")]
        public System.DateTime Modified { get; set; }
    
    	[DataMember(Name="otherDate")]
        public Nullable<System.DateTime> OtherDate { get; set; }
    
    
        public Entity2 Copy()
        {
            var copy = new Entity2
                {
    			SomeID = SomeID
    			,ff = ff
    			,Modified = Modified
    			,OtherDate = OtherDate
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    	return this.SomeID.ToString();
    }
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEntity2(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEntity2(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEntity2(SomeID , ff );
        }
    
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Core.Data;
using Civic.Framework.WebApi;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="entity3")]
    public partial class Entity3 : IEntity
    {
    	[DataMember(Name="someUID")]
        public string SomeUID { get; set; }
    
    	[DataMember(Name="ff")]
        public string ff { get; set; }
    
    	[DataMember(Name="modified")]
        public System.DateTime Modified { get; set; }
    
    	[DataMember(Name="otherDate")]
        public Nullable<System.DateTime> OtherDate { get; set; }
    
    
        public Entity3 Copy()
        {
            var copy = new Entity3
                {
    			SomeUID = SomeUID
    			,ff = ff
    			,Modified = Modified
    			,OtherDate = OtherDate
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    	return this.SomeUID.ToString();
    }
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEntity3(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEntity3(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEntity3(SomeUID , ff );
        }
    
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Civic.Core.Data;
using Civic.Framework.WebApi;

namespace Civic.Framework.WebApi.Test.Entities
{
    [DataContract(Name="environment")]
    public partial class Environment : IEntity
    {
    	[DataMember(Name="id")]
        public int ID { get; set; }
    
    	[DataMember(Name="name")]
        public string Name { get; set; }
    
    
        public Environment Copy()
        {
            var copy = new Environment
                {
    			ID = ID
    			,Name = Name
                };
            return copy;
        }
    
    	public string IdentityID 
        { 
    		get {
    	return this.ID.ToString();
    }
    	}
    
        public void Add(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.AddEnvironment(this);
        }
    
        public void Modify(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.ModifyEnvironment(this);
        }
    
        public void Remove(IDBConnection connection)
        {
    		var service = new Services.ExampleService();
    		service.Connection = connection;
    		service.RemoveEnvironment(ID );
        }
    
    }
}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Entity1")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEntity1Controller : ApiController 
    {
    	private static readonly ExampleService _service = new ExampleService();
    
    	[Route("")]
    	public QueryMetadata<Entity1Entity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","entity1");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _service.GetPagedEntity1(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<Entity1Entity>(result, resultLimit);
    	}
    
    	[Route("{name}")]
    	public QueryMetadata<Entity1Entity> Get( String name )
    	{
    		var result = new List<Entity1Entity> { _service.GetEntity1( name) };
    		return new QueryMetadata<Entity1Entity>(result, 1);
    	}
    
    	[Route("")]
    	public String Post([FromBody]Entity1Entity value)
    	{
    		_service.AddEntity1(value);
    		return value.Name;
    	}
    
    	[Route("{name}")]
    	public void Put(String name, [FromBody]Entity1Entity value)
    	{
    		value.Name = name;
    		_service.ModifyEntity1(value);
    	}
    
    	[Route("{name}")]
    	public void Delete( String name )
    	{
    		_service.RemoveEntity1( name );
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Entity2")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEntity2Controller : ApiController 
    {
    	private static readonly ExampleService _service = new ExampleService();
    
    	[Route("")]
    	public QueryMetadata<Entity2Entity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","entity2");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _service.GetPagedEntity2(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<Entity2Entity>(result, resultLimit);
    	}
    
    	[Route("{someID}/{ff}")]
    	public QueryMetadata<Entity2Entity> Get( Int32 someID, String ff )
    	{
    		var result = new List<Entity2Entity> { _service.GetEntity2( someID, ff) };
    		return new QueryMetadata<Entity2Entity>(result, 1);
    	}
    
    	[Route("")]
    	public String Post([FromBody]Entity2Entity value)
    	{
    		_service.AddEntity2(value);
    		return value.ff;
    	}
    
    	[Route("{someID}/{ff}")]
    	public void Put(Int32 someID, String ff, [FromBody]Entity2Entity value)
    	{
    		value.SomeID = someID;
    		value.ff = ff;
    		_service.ModifyEntity2(value);
    	}
    
    	[Route("{someID}/{ff}")]
    	public void Delete( Int32 someID, String ff )
    	{
    		_service.RemoveEntity2( someID, ff );
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Entity3")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEntity3Controller : ApiController 
    {
    	private static readonly ExampleService _service = new ExampleService();
    
    	[Route("")]
    	public QueryMetadata<Entity3Entity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","entity3");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _service.GetPagedEntity3(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<Entity3Entity>(result, resultLimit);
    	}
    
    	[Route("{someUID}/{ff}")]
    	public QueryMetadata<Entity3Entity> Get( String someUID, String ff )
    	{
    		var result = new List<Entity3Entity> { _service.GetEntity3( someUID, ff) };
    		return new QueryMetadata<Entity3Entity>(result, 1);
    	}
    
    	[Route("")]
    	public String Post([FromBody]Entity3Entity value)
    	{
    		_service.AddEntity3(value);
    		return value.ff;
    	}
    
    	[Route("{someUID}/{ff}")]
    	public void Put(String someUID, String ff, [FromBody]Entity3Entity value)
    	{
    		value.SomeUID = someUID;
    		value.ff = ff;
    		_service.ModifyEntity3(value);
    	}
    
    	[Route("{someUID}/{ff}")]
    	public void Delete( String someUID, String ff )
    	{
    		_service.RemoveEntity3( someUID, ff );
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Web.Http;
using Civic.Framework.WebApi.Test.Services;
using Civic.Framework.WebApi.Test.Entities;
using Civic.Framework.WebApi;
using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Controllers
{
    [RoutePrefix("api/example/1.0/Environment")]
    [System.CodeDom.Compiler.GeneratedCode("STE-EF",".NET 3.5")]
    public partial class ExampleEnvironmentController : ApiController 
    {
    	private static readonly ExampleService _service = new ExampleService();
    
    	[Route("")]
    	public QueryMetadata<EnvironmentEntity> Get()
    	{
    		ODataV3QueryOptions options = this.GetOptions();
    		var maxrows = Civic.Framework.WebApi.Configuration.T4Config.GetMaxRows("dbo","environment");
    		var resultLimit = options.Top < maxrows && options.Top > 0 ? options.Top : maxrows;
    		string orderby = options.ProcessOrderByOptions();
    		var result = _service.GetPagedEnvironment(options.Skip, ref resultLimit, options.InlineCount, options.Filter, orderby);
    		return new QueryMetadata<EnvironmentEntity>(result, resultLimit);
    	}
    
    	[Route("{id}")]
    	public QueryMetadata<EnvironmentEntity> Get( Int32 id )
    	{
    		var result = new List<EnvironmentEntity> { _service.GetEnvironment( id) };
    		return new QueryMetadata<EnvironmentEntity>(result, 1);
    	}
    
    	[Route("")]
    	public Int32 Post([FromBody]EnvironmentEntity value)
    	{
    		_service.AddEnvironment(value);
    		return value.ID;
    	}
    
    	[Route("{id}")]
    	public void Put(Int32 id, [FromBody]EnvironmentEntity value)
    	{
    		value.ID = id;
    		_service.ModifyEnvironment(value);
    	}
    
    	[Route("{id}")]
    	public void Delete( Int32 id )
    	{
    		_service.RemoveEnvironment( id );
    	}
    }
}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Audit;
using Civic.Core.Configuration;
using Civic.Core.Data;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;


namespace Civic.Framework.WebApi.Test.Services
{
    
    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class ExampleService : IExample, IEntityService
    {
    	
    	private IDBConnection _connection;
    
    	public IDBConnection Connection
        {
    		get {
    			if(_connection==null) return DatabaseFactory.CreateDatabase("Example");
    	        return _connection;
    		}
    		set {
    			_connection = value;
    		}
        }
    
    	public INamedElement Configuration { get; set; }
    
        public string ModuleName { get { return "example"; } }
            
    	public List<string> EntitiesProvided { 
    		get {
    			return new List<string> { "entity1","entity2","entity3","environment" }; 
    		}
        }
    
    	public IEntity Create(string name)
    	{
    		switch(name) {
    					case "entity1":
    			case "Entity1":
    				return new Civic.Framework.WebApi.Test.Entities.Entity1();
    					case "entity2":
    			case "Entity2":
    				return new Civic.Framework.WebApi.Test.Entities.Entity2();
    					case "entity3":
    			case "Entity3":
    				return new Civic.Framework.WebApi.Test.Entities.Entity3();
    					case "environment":
    			case "Environment":
    				return new Civic.Framework.WebApi.Test.Entities.Environment();
    				};
    
    		return null;
        }
    }
    
}


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public Entity1Entity GetEntity1( String name) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity1ByName")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity1")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEntity1(name, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity1")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEntity1(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEntity1(Entity1Entity entity1) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity1")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity1.Name.ToString()+"", entity1);
    			 		Data.ExampleData.AddEntity1(entity1, db);
					AuditManager.MarkSuccessFul("dbo", logid);

    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEntity1(Entity1Entity entity1) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity1")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity1( entity1.Name, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.Name.ToString()+"", before, entity1);
    					Data.ExampleData.ModifyEntity1(entity1, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEntity1( String name ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity1")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "entity1")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity1( name, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.Name.ToString()+"", before);
    					Data.ExampleData.RemoveEntity1( name, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public Entity2Entity GetEntity2( Int32 someID, String ff) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity2Byff")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity2")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEntity2(someID, ff, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity2")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity2")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEntity2(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEntity2(Entity2Entity entity2) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity2")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "entity2")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity2.SomeID.ToString()+entity2.ff.ToString()+"", entity2);
    			 		Data.ExampleData.AddEntity2(entity2, db);
					AuditManager.MarkSuccessFul("dbo", logid);

    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEntity2(Entity2Entity entity2) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity2")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "entity2")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity2( entity2.SomeID, entity2.ff, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeID.ToString()+before.ff.ToString()+"", before, entity2);
    					Data.ExampleData.ModifyEntity2(entity2, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEntity2( Int32 someID, String ff ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity2")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "entity2")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity2( someID, ff, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeID.ToString()+before.ff.ToString()+"", before);
    					Data.ExampleData.RemoveEntity2( someID, ff, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public Entity3Entity GetEntity3( String someUID, String ff) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEntity3Byff")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity3")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEntity3(someUID, ff, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<Entity3Entity> GetPagedEntity3(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEntity3")) {
    
                if (!AuthorizationHelper.CanView("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEntity3(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEntity3(Entity3Entity entity3) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEntity3")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", entity3.SomeUID.ToString()+entity3.ff.ToString()+"", entity3);
    			 		Data.ExampleData.AddEntity3(entity3, db);
					AuditManager.MarkSuccessFul("dbo", logid);

    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEntity3(Entity3Entity entity3) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEntity3")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity3( entity3.SomeUID, entity3.ff, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeUID.ToString()+before.ff.ToString()+"", before, entity3);
    					Data.ExampleData.ModifyEntity3(entity3, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEntity3( String someUID, String ff ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEntity3")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "entity3")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEntity3( someUID, ff, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.SomeUID.ToString()+before.ff.ToString()+"", before);
    					Data.ExampleData.RemoveEntity3( someUID, ff, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.ServiceModel.Activation;
using System.Collections.Generic;
using Civic.Core.Security;
using Civic.Core.Audit;
using Civic.Core.Logging;
using Civic.Framework.WebApi;
using Civic.Framework.WebApi.Test.Entities;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Services
{
    
    public partial class ExampleService
    	{
    	public EnvironmentEntity GetEnvironment( Int32 id) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetEnvironmentByID")) {
    
                if (!AuthorizationHelper.CanView("dbo", "environment")) throw new NotImplementedException();
    
    			try {		
                    using (var database = Connection) {
    					return Data.ExampleData.GetEnvironment(id, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "GetPagedEnvironment")) {
    
                if (!AuthorizationHelper.CanView("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using (var database = Connection) {
    					return Data.ExampleData.GetPagedEnvironment(skip, ref count, retCount, filterBy, orderBy, database);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    
    		return null;
    	}
    	
    	public void AddEnvironment(EnvironmentEntity environment) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "AddEnvironment")) {
    
                if (!AuthorizationHelper.CanAdd("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    	                var logid = AuditManager.LogAdd(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", environment.ID.ToString()+"", environment);
    			 		Data.ExampleData.AddEnvironment(environment, db);
					AuditManager.MarkSuccessFul("dbo", logid);

    
    				}
    			} 
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void ModifyEnvironment(EnvironmentEntity environment) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "ModifyEnvironment")) {
    
                if (!AuthorizationHelper.CanModify("dbo", "environment")) throw new NotImplementedException();
    
    			try {
                    using(var db = Connection) {
    					var before = Data.ExampleData.GetEnvironment( environment.ID, db);
    					var logid = AuditManager.LogModify(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.ID.ToString()+"", before, environment);
    					Data.ExampleData.ModifyEnvironment(environment, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    
    	public void RemoveEnvironment( Int32 id ) 
    	{
            using(Logger.CreateTrace(LoggingBoundaries.ServiceBoundary, typeof(ExampleService), "RemoveEnvironment")) {
    
                if (!AuthorizationHelper.CanRemove("dbo", "environment")) throw new NotImplementedException();
    
    			try {
    				using(var db = Connection) {
    					var before = Data.ExampleData.GetEnvironment( id, db);
    					var logid = AuditManager.LogRemove(IdentityManager.Username, IdentityManager.ClientMachine, "dbo", "dbo", before.ID.ToString()+"", before);
    					Data.ExampleData.RemoveEnvironment( id, db);
    					AuditManager.MarkSuccessFul("dbo", logid);
    				}
    			}
    			catch (Exception ex)
    			{
    				if (Logger.HandleException(LoggingBoundaries.ServiceBoundary, ex)) throw;
    			}
    
    		}
    	}
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;


namespace Civic.Framework.WebApi.Test.Services
{
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExample : IExampleEntity1,IExampleEntity2,IExampleEntity3,IExampleEnvironment
    {
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using Entity1Entity = Civic.Framework.WebApi.Test.Entities.Entity1;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEntity1
    {
    	[OperationContract]
    	List<Entity1Entity> GetPagedEntity1(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	Entity1Entity GetEntity1( String name);
    
    	[OperationContract]
    	void AddEntity1(Entity1Entity entity1);
    
    	[OperationContract]
    	void ModifyEntity1(Entity1Entity entity1);
    
    	[OperationContract]
    	void RemoveEntity1( String name );
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using Entity2Entity = Civic.Framework.WebApi.Test.Entities.Entity2;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEntity2
    {
    	[OperationContract]
    	List<Entity2Entity> GetPagedEntity2(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	Entity2Entity GetEntity2( Int32 someID, String ff);
    
    	[OperationContract]
    	void AddEntity2(Entity2Entity entity2);
    
    	[OperationContract]
    	void ModifyEntity2(Entity2Entity entity2);
    
    	[OperationContract]
    	void RemoveEntity2( Int32 someID, String ff );
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using Entity3Entity = Civic.Framework.WebApi.Test.Entities.Entity3;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEntity3
    {
    	[OperationContract]
    	List<Entity3Entity> GetPagedEntity3(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	Entity3Entity GetEntity3( String someUID, String ff);
    
    	[OperationContract]
    	void AddEntity3(Entity3Entity entity3);
    
    	[OperationContract]
    	void ModifyEntity3(Entity3Entity entity3);
    
    	[OperationContract]
    	void RemoveEntity3( String someUID, String ff );
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591 // this is for supress no xml comments in public members warnings 

using System;
using System.Collections.Generic;
using System.ServiceModel;
using Civic.Framework.WebApi.Test.Entities;

using EnvironmentEntity = Civic.Framework.WebApi.Test.Entities.Environment;

namespace Civic.Framework.WebApi.Test.Services
{
    
    [ServiceContract(Namespace = "http://example.civic360.com/")]
    public interface IExampleEnvironment
    {
    	[OperationContract]
    	List<EnvironmentEntity> GetPagedEnvironment(int skip, ref int count, bool retCount, string filterBy, string orderBy);
    
    	[OperationContract]
    	EnvironmentEntity GetEnvironment( Int32 id);
    
    	[OperationContract]
    	void AddEnvironment(EnvironmentEntity environment);
    
    	[OperationContract]
    	void ModifyEnvironment(EnvironmentEntity environment);
    
    	[OperationContract]
    	void RemoveEnvironment( Int32 id );
    }
}


