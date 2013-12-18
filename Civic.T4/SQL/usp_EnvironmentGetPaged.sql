
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
