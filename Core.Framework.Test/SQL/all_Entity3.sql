CREATE PROCEDURE [dbo].[usp_Entity3Add]
-- t4-params begin
	  @someUID [nvarchar](max)
	, @otherDate [datetime]
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](
-- t4-columns begin
		 [Modified]
		,[OtherDate]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 [common].udf_getSysDate()
		,@otherDate
-- t4-values end
	)

SET @someUID = SCOPE_IDENTITY()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]
	  @someUID [nvarchar](max)
	, @otherDate [datetime]
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin
		 [Modified] = [common].udf_getSysDate()
		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin
	    [e3].[SomeUID] = @someUID
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Remove]
	  @someUID [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity3]
	WHERE	
		-- t4-where begin
	    [SomeUID] = @someUID
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY3]
AS

	SELECT	
		-- t4-columns begin
		 [e3].[SomeUID]
		,[e3].[SomeID]
		,[e3].[Modified]
		,[e3].[OtherDate]
	
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
GO
