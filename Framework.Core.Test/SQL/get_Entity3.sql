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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
