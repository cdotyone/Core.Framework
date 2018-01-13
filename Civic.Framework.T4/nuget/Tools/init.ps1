#param($rootPath, $toolsPath, $package, $project)

$message = "Root Path " 
$message += $PSScriptRoot
Write-Host $message

$message = "ToolsPath " 
$message += $toolsPath
Write-Host $message

$message = "Package " 
$message += $package
Write-Host $message

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($toolsPath)[1]

$rootPath += '0.0.0.0'

$message = "Copying Templates From $rootPath To" 
$messeg += $base
Write-Host $message

Copy-Item $rootPath $base -recurse -force