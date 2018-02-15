#!/bin/bash
set -eu -o pipefail

dotnet restore /tests/linux/LinuxTests.csproj
dotnet test /tests/linux/LinuxTests.csproj
