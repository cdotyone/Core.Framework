#param($rootPath, $toolsPath, $package, $project)

$regex = new-object System.Text.RegularExpressions.Regex ('(.*\\).*', [System.Text.RegularExpressions.RegexOptions]::MultiLine)
$base = $regex.split($rootPath)[1] + 'T4'

$rootPath += '\T4'

Write-Output Copying Templates From $rootPath To $base

Copy-Item $rootPath $base -recurse -force