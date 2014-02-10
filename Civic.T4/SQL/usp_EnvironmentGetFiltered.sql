﻿IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentGetFiltered]
     @skip int
    ,@count int out
	,@orderBy nvarchar(512)
	,@filterBy nvarchar(512)
	,@retcount bit = 0
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @select nvarchar(max)
    SET @select = 'SELECT	
		-- t4-columns begin
		 e.[Id]
		,e.[Name]
		-- t4-columns end
    FROM [dbo].[Environment] e'

	EXEC [dbo].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO