IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Get]
	  @id [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin
		 [e2].[Id]
		,[e2].[ff]
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[Id] = @id
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2GetFiltered]
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
		 [e2].[Id]
		,[e2].[ff]
		-- t4-columns end
    FROM [dbo].[Entity2] [e2]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @id [int] out
	, @ff [nvarchar](max) out
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
-- t4-values end
	)

SET @ID = SCOPE_IDENTITY()
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @id [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[Id] = @id
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Remove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Remove]
	  @id [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity2]
	WHERE	
		-- t4-where begin
	    [Id] = @id
	AND [ff] = @ff
		-- t4-where end
END
GO
