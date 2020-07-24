
CREATE DEFAULT [common].[udf_GetDate]
AS GETUTCDATE()
GO


CREATE DEFAULT [common].[udf_Unknown]
AS 'UNK'
GO

CREATE DEFAULT [common].[udf_Yes]
AS 1
GO

CREATE DEFAULT [common].[udf_No]
AS 0
GO

CREATE DEFAULT [common].[udf_Zero]
AS 0
GO
-- t4-defaults begin
EXECUTE sp_bindefault N'[common].udf_GetDate', N'[dbo].[Entity2].[Modified]';
EXECUTE sp_bindefault N'[common].udf_GetDate', N'[dbo].[Entity3].[Modified]';
EXECUTE sp_bindefault N'[common].udf_GetDate', N'[dbo].[InstallationEnvironment].[Modified]';
-- t4-defaults end
GO
