IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGet]
	  @id [int]
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @id
		-- t4-where end
END
GO
