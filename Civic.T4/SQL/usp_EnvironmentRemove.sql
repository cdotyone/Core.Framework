IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentRemove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
	  @Id [int]
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Environment]
	WHERE	
		-- t4-where begin
	    [Id] = @Id
		-- t4-where end
END
GO
