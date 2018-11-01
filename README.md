# TemplateTester

## Purpose

This repository is for the following purposes:

* Used to arrive at a stable dotnet template
* dotnet template can be used to quickly generate PoC prototypes
* Ensure structure is in place for SPA publishing and testing

## Generating Code Coverage

Run the following command in PowerShell from the root directory to generate a coverage report. Inspired by [this article](https://medium.com/agilix/collecting-test-coverage-using-coverlet-and-sonarqube-for-a-net-core-project-ef4a507d4b28)

```PowerShell
dotnet test .\TemplateTesterTests\TemplateTesterTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Generating an HTML Report

First ensure the [ReportGenerator](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) tool is
installed. Alternative techniques may be found [here](https://danielpalme.github.io/ReportGenerator/usage.html).

```PowerShell
dotnet tool install --global dotnet-reportgenerator-globaltool --version 4.0.2
```

Then run the following to generate a fresh HTML from the coverage file you generated from `Generating Code Coverage`

```PowerShell
reportgenerator "-reports:.\TemplateTesterTests\coverage.opencover.xml" "-targetdir:.\TemplateTesterTests\coveragereport"
```
