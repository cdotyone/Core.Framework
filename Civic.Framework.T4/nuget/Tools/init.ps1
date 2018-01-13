$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($PSScriptRoot)[1]

Write-Host "Base " $base

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*(\\)', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$rootPath = $regex.split($PSScriptRoot)[1]

Write-Host "Root " $rootPath

$rootPath += '0.0.0.0'

$message = "Copying Templates From " + $base + " To " + $rootPath
Write-Host $message

Copy-Item $base $rootPath -recurse -force
