
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
