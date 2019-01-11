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
