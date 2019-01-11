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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY1]
AS

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
GO
