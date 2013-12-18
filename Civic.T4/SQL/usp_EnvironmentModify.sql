IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentModify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @Id [int]
	, @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE e SET 
		-- t4-columns begin
		 [Name] = @name
		-- t4-columns end
	FROM [dbo].[Environment] e
	WHERE	
		-- t4-where begin
	    e.[Id] = @Id
		-- t4-where end
END
GO
