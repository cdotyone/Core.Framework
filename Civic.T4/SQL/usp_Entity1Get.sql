﻿IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Get]
	  @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 e1.[Name]
		,e1.[EnvironmentId]
		-- t4-columns end
	FROM [dbo].[Entity1] e1
	WHERE	
		-- t4-where begin
	    e1.[Name] = @name
		-- t4-where end
END
GO