$Projects = @("TheTeacher.Api","TheTeacher.Infrastructure","TheTeacher.Core","TheTeacher.Tests", "TheTeacher.Tests.EndToEnd")
foreach( $Project in $Projects)
{
    dotnet restore $Project/$Project.csproj
}