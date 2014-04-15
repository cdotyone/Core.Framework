
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetPaged]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetPaged]
GO

IF EXISTS(SELECT * from [dbo].[udf_Split](convert(nvarchar(50),SERVERPROPERTY('productversion')),'.') where Pos=1 and Item>10)
BEGIN
DECLARE @SQL NVARCHAR(4000)
SET @SQL ='
CREATE PROCEDURE [dbo].[usp_Entity1GetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e1.[Name]
		,e1.[EnvironmentId]
		-- t4-columns end
	FROM [dbo].[Entity1] e1
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''NAME_DESC'' THEN e1.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e1.[Name] ELSE '''' END
			, CASE WHEN @orderBy = ''ENVIRONMENTID_DESC'' THEN e1.[EnvironmentId] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ENVIRONMENTID_ASC'' THEN e1.[EnvironmentId] ELSE '''' END
		-- t4-order end
	OFFSET @skip ROW
	FETCH NEXT @count ROW ONLY
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_Entity1GetCount] @count=@count out
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
CREATE PROCEDURE [dbo].[usp_Entity1GetPaged]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @LIST TABLE(
		POS INT IDENTITY(1,1)
    ,[Name] [nvarchar](max) 
    ,[EnvironmentId] [int] 
	)

	INSERT INTO @LIST(
		 [Name]
		,[EnvironmentId]
	)

	SELECT	
		-- t4-columns begin
		 e1.[Name]
		,e1.[EnvironmentId]
		-- t4-columns end
	FROM [dbo].[Entity1] e1
	ORDER BY 
		-- t4-order begin
			  CASE WHEN @orderBy = ''NAME_DESC'' THEN e1.[Name] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''NAME_ASC'' THEN e1.[Name] ELSE '''' END
			, CASE WHEN @orderBy = ''ENVIRONMENTID_DESC'' THEN e1.[EnvironmentId] ELSE '''' END DESC
			, CASE WHEN @orderBy = ''ENVIRONMENTID_ASC'' THEN e1.[EnvironmentId] ELSE '''' END
		-- t4-order end

	DELETE FROM @LIST
	WHERE POS<=@skip OR POS>@skip+@count
	
	SELECT * FROM @LIST
	
	IF @retcount=1
	BEGIN
		EXEC [dbo].[usp_Entity1GetCount] @count=@count out
	END
END
'
EXEC sp_executesql @SQL

END
GO
