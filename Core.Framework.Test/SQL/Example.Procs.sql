﻿SET ANSI_NULLS ON
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
	FROM [common].[OrgUnit]
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
		,[common].udf_getSysDate()
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
	FROM [common].[OrgUnit]
	WHERE [OUID] = @ouid

	UPDATE [e2] SET 
		-- t4-columns begin
		 [ff] = @ff
		,[Modified] = [common].udf_getSysDate()
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
CREATE PROCEDURE [dbo].[usp_EnvironmentAdd]
-- t4-params begin
	  @name [nvarchar](max)
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


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_EnvironmentModify]
	  @name [nvarchar](max)
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentAdd]
-- t4-params begin
	  @environmentCode [varchar](20) out
	, @name [nvarchar](100)
	, @description [nvarchar](max)
	, @isVisible [varchar](1)
-- t4-params end
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[InstallationEnvironment](
-- t4-columns begin
		 [EnvironmentCode]
		,[Name]
		,[Description]
		,[IsVisible]
		,[Modified]
-- t4-columns end
	) VALUES (

-- t4-values begin
		 @environmentCode
		,@name
		,@description
		,@isVisible
		,[common].udf_getSysDate()
-- t4-values end
	)


END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentModify]
	  @environmentCode [varchar](20)
	, @name [nvarchar](100)
	, @description [nvarchar](max)
	, @isVisible [varchar](1)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [ie] SET 
		-- t4-columns begin
		 [EnvironmentCode] = @environmentCode
		,[Name] = @name
		,[Description] = @description
		,[IsVisible] = @isVisible
		,[Modified] = [common].udf_getSysDate()
		-- t4-columns end
	FROM [dbo].[InstallationEnvironment] [ie]
	WHERE	
		-- t4-where begin
	    [ie].[EnvironmentCode] = @environmentCode
		-- t4-where end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentRemove]
	  @environmentCode [varchar](20)
AS
BEGIN
	SET NOCOUNT ON

	DELETE FROM [dbo].[InstallationEnvironment]
	WHERE	
		-- t4-where begin
	    [EnvironmentCode] = @environmentCode
		-- t4-where end
END
GO
