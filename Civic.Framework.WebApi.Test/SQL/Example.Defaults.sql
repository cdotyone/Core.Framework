
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_GetDate]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_GetDate]
GO

CREATE DEFAULT [civic].[udf_GetDate]
AS GETUTCDATE()
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Unknown]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Unknown]
GO

CREATE DEFAULT [civic].[udf_Unknown]
AS 'UNK'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Yes]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Yes]
GO

CREATE DEFAULT [civic].[udf_Yes]
AS 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_No]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_No]
GO

CREATE DEFAULT [civic].[udf_No]
AS 0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[civic].[udf_Zero]') AND OBJECTPROPERTY(object_id, N'IsDefault') = 1)
DROP DEFAULT [civic].[udf_Zero]
GO

CREATE DEFAULT [civic].[udf_Zero]
AS 0
GO
-- t4-defaults begin
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity2].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[Entity3].[Modified]';
EXECUTE sp_bindefault N'civic.udf_GetDate', N'[dbo].[InstallationEnvironment].[Modified]';
-- t4-defaults end
GO
