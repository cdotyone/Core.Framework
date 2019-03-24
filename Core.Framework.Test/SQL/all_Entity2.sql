CREATE PROCEDURE [dbo].[usp_Entity2Add]
-- t4-params begin
	  @someID [int] out
	, @ff [nvarchar](max) out
	, @otherDate [datetime]
	, @ouid [varchar](32)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @oid [int]
	SELECT @oid = [OID]
	FROM [civic].[OrgUnit]
	WHERE [OUID] = @ouid

	INSERT INTO [dbo].[Entity2](
-- t4-columns begin
		 [ff]
		,[Modified]
		,[OtherDate]
		,[OID]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @ff
		,[civic].udf_getSysDate()
		,@otherDate
		,@oid
-- t4-values end
	)

SET @someID = SCOPE_IDENTITY()
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Modify]
	  @someID [int]
	, @ff [nvarchar](max)
	, @otherDate [datetime]
	, @ouid [varchar](32)
AS
BEGIN
	SET NOCOUNT ON

		DECLARE @oid [int]
		SELECT @oid = [OID]
		FROM [civic].[OrgUnit]
		WHERE [OUID] = @ouid
	
	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [civic].udf_getSysDate()
		,[OtherDate] = @otherDate
		,[OID] = @oid
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin
	    [e2].[SomeID] = @someID
	AND [e2].[ff] = @ff
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Remove]
	  @someID [int]
	, @ff [nvarchar](max)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[Entity2]
	WHERE	
		-- t4-where begin
	    [SomeID] = @someID
	AND [ff] = @ff
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_ENTITY2]
AS

	SELECT	
		-- t4-columns begin
		 [e2].[SomeID]
		,[e2].[ff]
		,[e2].[Modified]
		,[e2].[OtherDate]
		,[e2].[OID]
		,[ou].[OUID]
	
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	LEFT JOIN [civic].[OrgUnit] as [ou] ON [e2].[OID] = [ou].[OID]
GO
