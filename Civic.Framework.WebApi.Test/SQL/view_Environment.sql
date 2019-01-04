
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
