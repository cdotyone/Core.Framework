#param($rootPath, $toolsPath, $package, $project)

Write-Output Root Path $rootPath
Write-Output ToolsPath $toolsPath
Write-Output Package $package

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($toolsPath)[1]

$rootPath += '0.0.0.0'

Write-Output Copying Templates From $rootPath To $base

Copy-Item $rootPath $base -recurse -force