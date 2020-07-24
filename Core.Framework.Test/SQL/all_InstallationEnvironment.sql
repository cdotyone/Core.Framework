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
		,[common].udf_getSysDate()
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
		,[Modified] = [common].udf_getSysDate()
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
