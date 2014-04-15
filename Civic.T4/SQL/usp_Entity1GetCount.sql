IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetCount]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1GetCount]
     @count int out
AS
BEGIN
	SET NOCOUNT ON

	SELECT @count = COUNT(*)
	FROM [dbo].[Entity1] e1
	
END
GO
