#param($rootPath, $toolsPath, $package, $project)

$message = "Root Path " + $rootPath
Write-Host $message

$message = "ToolsPath " + $toolsPath
Write-Host $message

$message = "Package " + $package
Write-Host $message

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($toolsPath)[1]

$rootPath += '0.0.0.0'

$message = "Copying Templates From $rootPath To" + $base
Write-Host $message

Copy-Item $rootPath $base -recurse -force