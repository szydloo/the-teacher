$Projects = @("TheTeacher.Tests", "TheTeacher.Tests.EndToEnd")
foreach($Project in $Projects)
{
    dotnet test $Project/$Project.csproj
}