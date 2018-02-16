#!/bin/bash
set -eu -o pipefail

dotnet restore ./LinuxTests.csproj
dotnet test ./LinuxTests.csproj
