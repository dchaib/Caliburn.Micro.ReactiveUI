function Get-ScriptDirectory
{
   $Invocation = (Get-Variable MyInvocation -Scope 1).Value
   Split-Path $Invocation.MyCommand.Path
}

$nuget = Join-Path (Get-ScriptDirectory) ..\src\.nuget\NuGet.exe
$nuspec = Join-Path (Get-ScriptDirectory) .\package\caliburn.micro.reactiveui.nuspec

. $nuget pack $nuspec