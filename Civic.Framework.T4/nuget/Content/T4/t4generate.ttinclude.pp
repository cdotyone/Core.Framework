<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\TemplateFilemanager.CS.ttinclude" #>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\Core.ttinclude"#>
/*
<#
var fileManager = TemplateFileManager.Create(this);
fileManager.IsAutoIndentEnabled = true;
fileManager.CanOverwriteExistingFile = true;
#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\DBProcs.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\DBTables.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\DB.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\Data.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\Entities.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\WebAPI.ttinclude"#>
<#@ include file="$(userdata)\.nuget\packages\Civic.Framework.T4.Model\$version$\T4\WCF.ttinclude"#>
<#
EmbedEdmx(Host.TemplateFile.ToString(), fileManager);
fileManager.Process();
#>