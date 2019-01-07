IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VW_INSTALLATIONENVIRONMENT]') AND OBJECTPROPERTY(object_id, N'IsView') = 1)
DROP VIEW [dbo].[VW_INSTALLATIONENVIRONMENT]
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
