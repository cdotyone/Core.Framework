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
