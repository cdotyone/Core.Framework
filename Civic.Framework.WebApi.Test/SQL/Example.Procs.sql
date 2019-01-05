

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetSysDate]'))
DROP FUNCTION [civic].[udf_GetSysDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [civic].[udf_GetSysDate]()
RETURNS datetime
AS
BEGIN
	RETURN getdate()
END
GO


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

	
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity2Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity2Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity2Get]

	  @someID [int]

	, @ff [nvarchar](max)

AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin

		 [e2].[SomeID]

		,[e2].[ff]

		,[e2].[Modified]

		,[e2].[OtherDate]

		,[e2].[OID]
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin

	    [e2].[SomeID] = @someID

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

		 [e2].[SomeID]

		,[e2].[ff]

		,[e2].[Modified]

		,[e2].[OtherDate]

		,[e2].[OID]
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

	  @someID [int] out

	, @ff [nvarchar](max) out

	, @otherDate [datetime]

	, @oID [int]

-- t4-params end

AS
BEGIN
	SET NOCOUNT ON

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

		,@oID

-- t4-values end

	)

SET @someID = SCOPE_IDENTITY()

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

	  @someID [int]

	, @ff [nvarchar](max)

	, @otherDate [datetime]

	, @oID [int]

AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e2] SET 
		-- t4-columns begin

		 [ff] = @ff

		,[Modified] = [civic].udf_getSysDate()

		,[OtherDate] = @otherDate

		,[OID] = @oID
		-- t4-columns end
	FROM [dbo].[Entity2] [e2]
	WHERE	
		-- t4-where begin

	    [e2].[SomeID] = @someID

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

	
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Get]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Get]

	  @someUID [nvarchar](max)

AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin

		 [e3].[SomeUID]

		,[e3].[SomeID]

		,[e3].[Modified]

		,[e3].[OtherDate]
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin

	    [e3].[SomeUID] = @someUID

		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3GetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3GetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3GetFiltered]
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

		 [e3].[SomeUID]

		,[e3].[SomeID]

		,[e3].[Modified]

		,[e3].[OtherDate]
		-- t4-columns end
    FROM [dbo].[Entity3] [e3]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Add]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Add]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Add]

-- t4-params begin

	  @someUID [nvarchar](max)

	, @someID [bigint] out

	, @otherDate [datetime]

-- t4-params end

AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Entity3](

-- t4-columns begin

		 [SomeID]

		,[Modified]

		,[OtherDate]

-- t4-columns end
	) VALUES (


-- t4-values begin

		 @someID

		,[civic].udf_getSysDate()

		,@otherDate

-- t4-values end

	)

SET @someUID = SCOPE_IDENTITY()

END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Modify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Modify]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Entity3Modify]

	  @someUID [nvarchar](max)

	, @someID [bigint]

	, @otherDate [datetime]

AS
BEGIN
	SET NOCOUNT ON

	UPDATE [e3] SET 
		-- t4-columns begin

		 [SomeID] = @someID

		,[Modified] = [civic].udf_getSysDate()

		,[OtherDate] = @otherDate
		-- t4-columns end
	FROM [dbo].[Entity3] [e3]
	WHERE	
		-- t4-where begin

	    [e3].[SomeUID] = @someUID

		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Entity3Remove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_Entity3Remove]
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

	
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentGet]
GO
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentGetFiltered]') AND type in (N'P', N'PC'))
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentAdd]
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentModify]
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

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_EnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_EnvironmentRemove]
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

	
IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InstallationEnvironmentGet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InstallationEnvironmentGet]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentGet]

	  @environmentCode [varchar](20)

AS
BEGIN
	SET NOCOUNT ON

	SELECT	
		-- t4-columns begin

		 [ie].[EnvironmentCode]

		,[ie].[Name]

		,[ie].[Description]

		,[ie].[IsVisible]

		,[ie].[Modified]
		-- t4-columns end
	FROM [dbo].[InstallationEnvironment] [ie]
	WHERE	
		-- t4-where begin

	    [ie].[EnvironmentCode] = @environmentCode

		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InstallationEnvironmentGetFiltered]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InstallationEnvironmentGetFiltered]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InstallationEnvironmentGetFiltered]
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

		 [ie].[EnvironmentCode]

		,[ie].[Name]

		,[ie].[Description]

		,[ie].[IsVisible]

		,[ie].[Modified]
		-- t4-columns end
    FROM [dbo].[InstallationEnvironment] [ie]'

	EXEC [civic].[usp_ProcessFilter]
		     @skip = @skip
			,@select = @select
			,@count = @count out
			,@orderBy = @orderBy
			,@filterBy = @filterBy
			,@retcount = @retcount 
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InstallationEnvironmentAdd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InstallationEnvironmentAdd]
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

		,[civic].udf_getSysDate()

-- t4-values end

	)



END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InstallationEnvironmentModify]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InstallationEnvironmentModify]
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

		,[Modified] = [civic].udf_getSysDate()
		-- t4-columns end
	FROM [dbo].[InstallationEnvironment] [ie]
	WHERE	
		-- t4-where begin

	    [ie].[EnvironmentCode] = @environmentCode

		-- t4-where end
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InstallationEnvironmentRemove]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InstallationEnvironmentRemove]
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

	

