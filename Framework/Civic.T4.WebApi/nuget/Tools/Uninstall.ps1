param($installPath, $toolsPath, $package, $project)

$project.Save()


$Dirs = "Controllers,Data,Models,Entities,Services,SQL,T4,Security"
$Dirs.split(',') | ForEach { 
	$Dir = $_
	$project.ProjectItems | ForEach {
		If( $_.Name -eq $Dir) { 
			If($_.ProjectItems.Count -eq 0) { $_.Delete() } 
			If($_.ProjectItems.Count -ne 0) { 
				$Dir = $_
				$Dir.ProjectItems | ForEach {
					If(($_.Name -eq "Example.txt4") -or ($_.Name -eq "Example.tt") -or ($_.Name -eq "t4generate.ttinclude") -or ($_.Name -eq "t4settings.ttinclude")) {
						$_.Delete() 
					}
				}
				If($Dir.ProjectItems.Count -eq 0) { $Dir.Delete() } 				
			} 
		} 
	} 
}

$project.Save()

