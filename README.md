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
