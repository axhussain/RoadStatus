# RoadStatus
Simple console app client for TfL's public API, returning the status of the given road.

## Build instructions
Follow the following steps to clone this repo and build it as a self-contained app).

1. `git clone https://github.com/axhussain/RoadStatus.git`
2. `cd RoadStatus` (the solution root)
3. `dotnet restore`
4. `cd RoadStatus` (the project root)
5. Edit appsettings.json with your TfL API App ID and App Key (these can be obtained from https://api-portal.tfl.gov.uk/)
6. `dotnet publish -r win-x86 -c Release` (this will build the self-contained app)
7. `cd .\bin\release\netcoreapp2.1\win-x86\publish\` (the path of RoadStatus.exe)
8. You can now run the executable, e.g. `RoadStatus.exe A2`

## Assumptions
The above assumes that the app is being built on a Windows x86 box.

## Unit tests
This repo contains some tests using the MS Test suite. To run the tests:
1. cd back to the solution root folder.
2. `dotnet build`
3. `dotnet test`

Alternatively you may open the solution in Visual Studio 2017 and run the tests from the Test Explorer.
