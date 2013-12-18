IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetCount]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetCount]
     @count int out
AS
BEGIN
	SET NOCOUNT ON

	SELECT @count = COUNT(*)
	FROM [dbo].[Environment] e
	
END
GO
