# Development Workflows

## Local build and test
- Restore/build: `dotnet build src/MX.InvisionCommunity.slnx`
- Validate non-integration tests: `dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName!~IntegrationTests"`
- Optional targeted test: `dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName~MyTestClass.MyTestMethod"`
- Format validation: `dotnet format src/MX.InvisionCommunity.slnx --verify-no-changes`
- Packages emit on build via `GeneratePackageOnBuild` in the package projects.

## .NET 10 focused validation
- Build package projects for .NET 10:
  - `dotnet build src/MX.InvisionCommunity.Api.Abstractions/MX.InvisionCommunity.Api.Abstractions.csproj -f net10.0`
  - `dotnet build src/MX.InvisionCommunity.Api.Client/MX.InvisionCommunity.Api.Client.csproj -f net10.0`
  - `dotnet build src/MX.InvisionCommunity.Api.Client.Testing/MX.InvisionCommunity.Api.Client.Testing.csproj -f net10.0`
- Build package projects in Release to generate NuGet packages (includes net10.0 assets for multi-targeted packages):
  - `dotnet build src/MX.InvisionCommunity.Api.Abstractions/MX.InvisionCommunity.Api.Abstractions.csproj -c Release`
  - `dotnet build src/MX.InvisionCommunity.Api.Client/MX.InvisionCommunity.Api.Client.csproj -c Release`
  - `dotnet build src/MX.InvisionCommunity.Api.Client.Testing/MX.InvisionCommunity.Api.Client.Testing.csproj -c Release`
- Publish .NET 10 client assets:
  - `dotnet publish src/MX.InvisionCommunity.Api.Client/MX.InvisionCommunity.Api.Client.csproj -f net10.0 -c Release`

## Versioning and releases
- Versioning uses Nerdbank.GitVersioning (`version.json`), tagging releases as `v<semver>`.
- Release automation runs via `release-version-and-tag.yml` then `release-publish-nuget.yml` to push the NuGet package `MX.InvisionCommunity.Api.Client`.

## CI/CD workflows
- `build-and-test.yml` builds and runs the repository test projects on changes.
- `pr-verify.yml` validates pull requests.
- `codequality.yml` runs static analysis.
- `dependabot-automerge.yml` manages bot updates.
- `release-version-and-tag.yml` and `release-publish-nuget.yml` manage version bumps and NuGet publishing.
