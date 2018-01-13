#param($rootPath, $toolsPath, $package, $project)

Write-Host "Root Path "+$rootPath
Write-Host "ToolsPath "+$toolsPath
Write-Host "Package "+$package

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($toolsPath)[1]

$rootPath += '0.0.0.0'

Write-Host "Copying Templates From $rootPath To" + $base

Copy-Item $rootPath $base -recurse -force