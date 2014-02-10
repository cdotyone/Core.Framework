﻿
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_CreateDynamicVals]'))
DROP FUNCTION [dbo].[udf_CreateDynamicVals]
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Split]'))
DROP FUNCTION [dbo].[udf_Split]
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ProcessFilter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ProcessFilter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udf_Split] (
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


CREATE FUNCTION [dbo].[udf_CreateDynamicVals] (
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
CREATE PROCEDURE [dbo].[usp_ProcessFilter]
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

	-- parse and determine filter by
	set @filterBy=replace(replace(replace(replace(replace(replace(upper(@filterBy),' eq ','|=|'),' lt ','|<|'),' gt ','|>|'),' ge ','|>=|'),' le ','|<=|'),' ne ','|<>|')
	
	DECLARE @BLOCKS TABLE(pos int,blk nvarchar(512))
	insert into @BLOCKS
	select [POS],[item] from [dbo].[udf_Split](@filterBy,' ')

	DECLARE @COMBINEVARS TABLE(pos int,varname nvarchar(512),operation nvarchar(512),varvalue nvarchar(512))
	DECLARE @OPERATIONS TABLE(pos int,operation nvarchar(512))
	
	WHILE(EXISTS(SELECT * FROM @BLOCKS))
	BEGIN
		DECLARE @PAIR NVARCHAR(512)
		SELECT @POS=POS,@PAIR=BLK FROM @BLOCKS

		IF CHARINDEX('|',@PAIR)>0 
		BEGIN
			DECLARE @TEMP TABLE(
				POS int
				,ITEM nvarchar(256)
			)
			INSERT INTO @TEMP
			select POS,[item] from [dbo].[udf_Split](@PAIR,'|')
			
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

	-- build dynamic order clause
	IF @orderBy is not null and @orderBy<>''
	BEGIN
		SET @order=''
		DECLARE @orderbylist table (orderby nvarchar(512),[desc] bit) 
		INSERT INTO @orderbylist
		SELECT [item],0 FROM [dbo].[udf_Split](@orderBy,' ')
		UPDATE @orderbylist SET ORDERBY=SUBSTRING(ORDERBY,1,LEN(ORDERBY)-5),[DESC]=1 WHERE PATINDEX('%_DESC',ORDERBY)>0
		UPDATE @orderbylist SET ORDERBY=SUBSTRING(ORDERBY,1,LEN(ORDERBY)-4) WHERE PATINDEX('%_ASC',ORDERBY)>0
		SELECT @order = @order + ' AND ' + orderby + CASE WHEN [DESC]=1 THEN ' DESC' ELSE '' END FROM @orderbylist
		IF @order<>''
		BEGIN
			SET @order=' ORDER BY '+ LTRIM(RTRIM(SUBSTRING(@order,5,LEN(@order)-3)))
		END
	END
	ELSE
	BEGIN
		SET @SQL = @select 
		SET @order = 'ORDER BY getdate()'
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
	SET @PARAMS = N'@_skip int,@_count int,@_orderBy nvarchar(512),'+dbo.udf_CreateDynamicVals('val',20)
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
		SET @POS = PATINDEX('% FROM %',@SQL)
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
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/18/2013 16:21:12
-- Generated from EDMX file: D:\devel\CIVIC\T4\Civic.T4\Models\Example.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Example];
GO
-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

USE [Example];
GO
-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Environments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Environments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Environments'
CREATE TABLE [dbo].[Environments] (
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Environments'
ALTER TABLE [dbo].[Environments]
ADD CONSTRAINT [PK_Environments]

    PRIMARY KEY ([Id]ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetDate]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_GetDate]
GO

CREATE DEFAULT [dbo].[udf_GetDate]
AS GETUTCDATE()
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Unknown]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Unknown]
GO

CREATE DEFAULT [dbo].[udf_Unknown]
AS 'UNK'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Yes]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Yes]
GO

CREATE DEFAULT [dbo].[udf_Yes]
AS 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_No]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_No]
GO

CREATE DEFAULT [dbo].[udf_No]
AS 0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Zero]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Zero]
GO

CREATE DEFAULT [dbo].[udf_Zero]
AS 0
GO
-- t4-defaults begin
-- t4-defaults end
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
	  @Id [int] out
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

SET @ID = SCOPE_IDENTITY()
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
	  @Id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @Id
		-- t4-where end
END
GO

	IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetCount]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetCount]
     @count int out
AS
BEGIN
	SET NOCOUNT ON

	SELECT @count = COUNT(*)
	FROM [dbo].[Environment] e
	
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
		 e.[Id]
		,e.[Name]
		-- t4-columns end
    FROM [dbo].[Environment] e'

	EXEC [dbo].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO

	
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetPaged]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetPaged]
GO

IF EXISTS(SELECT * from [dbo].[udf_Split](convert(nvarchar(50),SERVERPROPERTY('productversion')),'.') where Pos=1 and Item>10)
BEGIN
DECLARE @SQL NVARCHAR(4000)
SET @SQL ='
CREATE PROCEDURE [dbo].[usp_EnvironmentGetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''ID_DESC'' THEN e.[Id] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ID_ASC'' THEN e.[Id] ELSE '''' END
			, CASE WHEN @orderBy = ''NAME_DESC'' THEN e.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e.[Name] ELSE '''' END
		-- t4-order end
	OFFSET @skip ROW
	FETCH NEXT @count ROW ONLY
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_EnvironmentGetCount] @count=@count out
	END
END
'
EXEC sp_executesql @SQL
END
GO

IF EXISTS(SELECT * from [dbo].[udf_Split](convert(nvarchar(50),SERVERPROPERTY('productversion')),'.') where Pos=1 and Item<11)
BEGIN
DECLARE @SQL NVARCHAR(4000)
SET @SQL = '
CREATE PROCEDURE [dbo].[usp_EnvironmentGetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @LIST TABLE(
		POS INT IDENTITY(1,1)
    ,[Id] [int] IDENTITY(1,1)
    ,[Name] [nvarchar](max) 
	)

	INSERT INTO @LIST(
		 [Id]
		,[Name]
	)

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''ID_DESC'' THEN e.[Id] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ID_ASC'' THEN e.[Id] ELSE '''' END
			, CASE WHEN @orderBy = ''NAME_DESC'' THEN e.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e.[Name] ELSE '''' END
		-- t4-order end

	DELETE FROM @LIST
	WHERE POS<=@skip OR POS>@skip+@count
	
	SELECT * FROM @LIST
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_EnvironmentGetCount] @count=@count out
	END
END
'
EXEC sp_executesql @SQL

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
	  @Id [int]
	, @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE e SET 
		-- t4-columns begin
		 [Name] = @name
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @Id
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
	  @Id [int]
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Environment]
	WHERE	
		-- t4-where begin
	    [Id] = @Id
		-- t4-where end
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
	  @Id [int] out
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

SET @ID = SCOPE_IDENTITY()
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
	  @Id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @Id
		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetCount]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetCount]
     @count int out
AS
BEGIN
	SET NOCOUNT ON

	SELECT @count = COUNT(*)
	FROM [dbo].[Environment] e
	
END
GO


IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetPaged]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetPaged]
GO

IF EXISTS(SELECT * from [dbo].[udf_Split](convert(nvarchar(50),SERVERPROPERTY('productversion')),'.') where Pos=1 and Item>10)
BEGIN
DECLARE @SQL NVARCHAR(4000)
SET @SQL ='
CREATE PROCEDURE [dbo].[usp_EnvironmentGetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''ID_DESC'' THEN e.[Id] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ID_ASC'' THEN e.[Id] ELSE '''' END
			, CASE WHEN @orderBy = ''NAME_DESC'' THEN e.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e.[Name] ELSE '''' END
		-- t4-order end
	OFFSET @skip ROW
	FETCH NEXT @count ROW ONLY
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_EnvironmentGetCount] @count=@count out
	END
END
'
EXEC sp_executesql @SQL
END
GO

IF EXISTS(SELECT * from [dbo].[udf_Split](convert(nvarchar(50),SERVERPROPERTY('productversion')),'.') where Pos=1 and Item<11)
BEGIN
DECLARE @SQL NVARCHAR(4000)
SET @SQL = '
CREATE PROCEDURE [dbo].[usp_EnvironmentGetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @LIST TABLE(
		POS INT IDENTITY(1,1)
    ,[Id] [int] IDENTITY(1,1)
    ,[Name] [nvarchar](max) 
	)

	INSERT INTO @LIST(
		 [Id]
		,[Name]
	)

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''ID_DESC'' THEN e.[Id] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ID_ASC'' THEN e.[Id] ELSE '''' END
			, CASE WHEN @orderBy = ''NAME_DESC'' THEN e.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e.[Name] ELSE '''' END
		-- t4-order end

	DELETE FROM @LIST
	WHERE POS<=@skip OR POS>@skip+@count
	
	SELECT * FROM @LIST
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_EnvironmentGetCount] @count=@count out
	END
END
'
EXEC sp_executesql @SQL

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
		 e.[Id]
		,e.[Name]
		-- t4-columns end
    FROM [dbo].[Environment] e'

	EXEC [dbo].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
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
	  @Id [int]
	, @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE e SET 
		-- t4-columns begin
		 [Name] = @name
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @Id
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
	  @Id [int]
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Environment]
	WHERE	
		-- t4-where begin
	    [Id] = @Id
		-- t4-where end
END
GO

INSERT INTO [dbo].[Environments]([Name]) VALUES ('Dev');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('QA');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Load');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Stage');
INSERT INTO [dbo].[Environments]([Name]) VALUES ('Prod');

GO

