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
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
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
		 [e].[ID]
		,[e].[Name]
		-- t4-columns end
    FROM [dbo].[Environment] [e]'

	EXEC [civic].[usp_ProcessFilter]
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

SET @id = SCOPE_IDENTITY()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @id [int]
	, @name [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e] SET 
		-- t4-columns begin
		 [Name] = @name
		-- t4-columns end
	FROM [dbo].[Environment] [e]
	WHERE	
		-- t4-where begin
	    [e].[ID] = @id
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentRemove]
	  @id [int]
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Environment]
	WHERE	
		-- t4-where begin
	    [ID] = @id
		-- t4-where end
END
GO
