IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentAdd]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @id [int] out
	, @name [nvarchar](max)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Environment](
-- t4-columns begin
		 [Name]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @name
-- t4-values end
	)

SET @ID = SCOPE_IDENTITY()
END
GO
