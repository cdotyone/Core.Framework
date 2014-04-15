IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Modify]
	  @name [nvarchar](max)
	, @environmentId [int]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE e1 SET 
		-- t4-columns begin
		 [Name] = @name
		,[EnvironmentId] = @environmentId
		-- t4-columns end
	FROM [dbo].[Entity1] e1
	WHERE	
		-- t4-where begin
	    e1.[Name] = @name
		-- t4-where end
END
GO
