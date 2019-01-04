
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VW_ENTITY1]') AND OBJECTPROPERTY(object_id, N'IsView') = 1)
DROP VIEW [dbo].[VW_ENTITY1]
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VW_ENTITY2]') AND OBJECTPROPERTY(object_id, N'IsView') = 1)
DROP VIEW [dbo].[VW_ENTITY2]
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
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VW_ENTITY3]') AND OBJECTPROPERTY(object_id, N'IsView') = 1)
DROP VIEW [dbo].[VW_ENTITY3]
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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VW_ENVIRONMENT]') AND OBJECTPROPERTY(object_id, N'IsView') = 1)
DROP VIEW [dbo].[VW_ENVIRONMENT]
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


