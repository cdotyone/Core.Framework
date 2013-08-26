param($installPath, $toolsPath, $package, $project)

$project.Save()

$baseTT = $toolsPath -replace "Tools\\net40","Content\T4\t4generate.ttinclude" 
$toolsPath = $toolsPath -replace "Tools\\net40","T4"
$index = $toolsPath.IndexOf("\packages\")
$toolsPath = $toolsPath.Substring($index,$toolsPath.Length-$index)
$projectPath = [system.io.Path]::GetDirectoryName($project.FullName)
$modelsPath = $projectPath + "\T4\t4generate.ttinclude"

$Content = (Get-Content $baseTT | out-string ) 
$Content = $Content -replace "{packageDir}", $toolsPath

$Dirs = "Controllers,Data,Entities,Services,SQL,Security"
$Dirs.split(',') | ForEach { 
	$hasDir = 0
	$Dir = $_
	$project.ProjectItems | ForEach { if($_.Name -eq $Dir) { $hasDir = 1 } }
	if(-not $hasDir) {
		Write-Host "Creating -> $_" 
		$project.ProjectItems.AddFolder($_) 
	} 
}

$project.Save()

Set-Content $modelsPath "$Content"