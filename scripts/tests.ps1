$Projects = @("TheTeacher.Tests", "TheTeacher.Tests.EndToEnd")
foreach($Project in $Projects)
{
    dotnet test tests/$Project/$Project.csproj
}