SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentGet]
	  @environmentCode [varchar](20)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [ie].[EnvironmentCode]
		,[ie].[Name]
		,[ie].[Description]
		,[ie].[IsVisible]
		,[ie].[Modified]
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
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentGetFiltered]
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
		 [ie].[EnvironmentCode]
		,[ie].[Name]
		,[ie].[Description]
		,[ie].[IsVisible]
		,[ie].[Modified]
		-- t4-columns end
    FROM [dbo].[InstallationEnvironment] [ie]'

	EXEC [common].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
