git checkout main
git pull
git checkout -b %1
dotnet new console -n %1
dotnet sln add %1/%1.csproj
cd tests
dotnet new xunit -n %1tests
dotnet sln ../advent-of-code-2020.sln add %1tests/%1tests.csproj
cd %1tests
dotnet add reference ../../%1/%1.csproj
dotnet add package Shouldly
dotnet restore
cd ../..
git add .
git commit -m "Add %1"
