
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Get]') AND type in (N'P', N'PC'))
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1GetFiltered]
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

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Add]

-- t4-params begin

	  @name [nvarchar](max) out

	, @environmentID [int]

	, @dte [datetime]

	, @dte2 [datetime]

	, @dble1 [decimal](20,4)

	, @dec1 [decimal](20,4)

-- t4-params end

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity1](

-- t4-columns begin

		 [Name]

		,[EnvironmentID]

		,[Dte]

		,[Dte2]

		,[Dble1]

		,[Dec1]

-- t4-columns end
	) VALUES (


-- t4-values begin

		 @name

		,@environmentID

		,@dte

		,@dte2

		,@dble1

		,@dec1

-- t4-values end

	)



END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Modify]

	  @name [nvarchar](max)

	, @environmentID [int]

	, @dte [datetime]

	, @dte2 [datetime]

	, @dble1 [decimal](20,4)

	, @dec1 [decimal](20,4)

AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e1] SET 
		-- t4-columns begin

		 [Name] = @name

		,[EnvironmentID] = @environmentID

		,[Dte] = @dte

		,[Dte2] = @dte2

		,[Dble1] = @dble1

		,[Dec1] = @dec1
		-- t4-columns end
	FROM [dbo].[Entity1] [e1]
	WHERE	
		-- t4-where begin

	    [e1].[Name] = @name

		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity1Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity1Remove]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity1Remove]

	  @name [nvarchar](max)

AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity1]
	WHERE	
		-- t4-where begin

	    [Name] = @name

		-- t4-where end
END
GO
