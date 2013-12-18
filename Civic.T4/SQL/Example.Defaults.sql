
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetDate]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_GetDate]
GO

CREATE DEFAULT [dbo].[udf_GetDate]
AS GETUTCDATE()
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Unknown]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Unknown]
GO

CREATE DEFAULT [dbo].[udf_Unknown]
AS 'UNK'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Yes]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Yes]
GO

CREATE DEFAULT [dbo].[udf_Yes]
AS 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_No]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_No]
GO

CREATE DEFAULT [dbo].[udf_No]
AS 0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_Zero]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [dbo].[udf_Zero]
GO

CREATE DEFAULT [dbo].[udf_Zero]
AS 0
GO
-- t4-defaults begin
-- t4-defaults end
GO
