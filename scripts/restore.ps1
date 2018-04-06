$Projects = @("TheTeacher.Api","TheTeacher.Infrastructure","TheTeacher.Core","TheTeacher.Tests", "TheTeacher.Tests.EndToEnd")
foreach($Project in $Projects)
{
    if($Project.Contains("Api") -or $Project.Contains("Infrastructure") -or $Project.Contains("Core")) 
    {
        dotnet restore ./src/$Project/$Project.csproj
    }
    else 
    {
        dotnet restore ./tests/$Project/$Project.csproj
    }
}