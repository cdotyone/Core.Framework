cd %2
echo %2
del  %2*.nupkg
SET PATH=%4.nuget;%PATH%
AssemblyVersionFinder.exe -s -n 3 -d %4\references > tmpFile
set /p version=<tmpFile
set nugetconfig=%4.nuget\nuget.config
echo %nugetconfig%
IF %3==Nuget (
	FOR %%X in ("%1nuget\*.nuspec") DO (
		ECHO nuget.exe pack %%X -NoPackageAnalysis -Version %version%
		nuget.exe pack %%X -NoPackageAnalysis -Version %version%
	)

	FOR %%X in ("%2*.nupkg") DO (
		ECHO nuget.exe push %%X -Config %nugetconfig% -Source https://nuget.civic360.com
		nuget.exe push %%X 2Gt35eXSKnbkenkiBiecY21WhRtcO6uv -Config %nugetconfig% -Source https://nuget.civic360.com
	)
)