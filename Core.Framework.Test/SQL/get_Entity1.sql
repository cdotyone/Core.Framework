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
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
		-- t4-columns end
	FROM [dbo].[Entity1] [e1]
	WHERE	
		-- t4-where begin
	    [e1].[Name] = @name
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1GetFiltered]
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
		 [e1].[Name]
		,[e1].[EnvironmentID]
		,[e1].[Dte]
		,[e1].[Dte2]
		,[e1].[Dble1]
		,[e1].[Dec1]
		-- t4-columns end
    FROM [dbo].[Entity1] [e1]'

	EXEC [common].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
