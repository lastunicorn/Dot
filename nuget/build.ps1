
$rootDirectory = ".."


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Clean up existing files."
Write-Output "----------------------------------------------------------------------------------------------------"

if (Test-Path "lib")
{
    Remove-Item "lib" -Recurse -Force;
}

if (Test-Path "*.nupkg")
{
    Remove-Item "*.nupkg" -Recurse -Force;
}

if (Test-Path "changelog.txt")
{
    Remove-Item "changelog.txt" -Recurse -Force;
}

if (Test-Path "readme.txt")
{
    Remove-Item "readme.txt" -Recurse -Force;
}


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Retrieve all files."
Write-Output "----------------------------------------------------------------------------------------------------"

New-Item -ItemType Directory -Force -Path "lib\netcoreapp3.1"

$projectDirs = @(
"$rootDirectory\sources\Dot.AdventureGame",
"$rootDirectory\sources\Dot.Application",
"$rootDirectory\sources\Dot.AudioSupport",
"$rootDirectory\sources\Dot.Bootstrapping",
"$rootDirectory\sources\Dot.Bootstrapping.MicrosoftDependencyInjection",
"$rootDirectory\sources\Dot.Bootstrapping.Ninject",
"$rootDirectory\sources\Dot.ConsoleHelpers",
"$rootDirectory\sources\Dot.Domain",
"$rootDirectory\sources\Dot.GameStorage.Binary",
"$rootDirectory\sources\Dot.Presentation",
"$rootDirectory\sources\Dot.WindowsNative"
)

for ($i = 0; $i -lt $projectDirs.length; $i++)
{
	$projectDir = $projectDirs[$i]
	
	Write-Output "---> Retrieve assemblies from project: $projectDir"
	
	Copy-Item -Path "$projectDir\bin\Release\netcoreapp3.1\*.dll" -Destination "lib\netcoreapp3.1" -Recurse -Container
	Copy-Item -Path "$projectDir\bin\Release\netcoreapp3.1\*.xml" -Destination "lib\netcoreapp3.1" -Recurse -Container
	Copy-Item -Path "$projectDir\bin\Release\netcoreapp3.1\*.json" -Destination "lib\netcoreapp3.1" -Recurse -Container
}

Write-Output "---> Retrieve changelog file"
Copy-Item -Path "$rootDirectory\doc\changelog.txt" -Destination "." -Recurse -Container

Write-Output "---> Retrieve readme file"
Copy-Item -Path "$rootDirectory\doc\readme.txt" -Destination "." -Recurse -Container

Write-Output "---> Retrieve license file"
Copy-Item -Path "$rootDirectory\license.txt" -Destination "." -Recurse -Container


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Create package"
Write-Output "----------------------------------------------------------------------------------------------------"

nuget pack


Write-Output "----------------------------------------------------------------------------------------------------"
Write-Output "Clean up files."
Write-Output "----------------------------------------------------------------------------------------------------"

if (Test-Path "lib")
{
    Remove-Item "lib" -Recurse -Force;
}

if (Test-Path "changelog.txt")
{
    Remove-Item "changelog.txt" -Recurse -Force;
}

if (Test-Path "readme.txt")
{
    Remove-Item "readme.txt" -Recurse -Force;
}

if (Test-Path "license.txt")
{
    Remove-Item "license.txt" -Recurse -Force;
}