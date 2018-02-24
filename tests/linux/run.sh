#!/bin/bash
set -eu -o pipefail

dotnet add package Postgres2Go -s https://api.bintray.com/nuget/skyrise/Postgres2Go
dotnet restore ./LinuxTests.csproj
dotnet test ./LinuxTests.csproj
