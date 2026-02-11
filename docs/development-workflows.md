# Development Workflows

## Local build and test
- Restore/build: `dotnet build src/MX.InvisionCommunity.sln`
- Tests: `dotnet test src` (currently no tests; kept for workflow parity)
- Packages emit on build via `GeneratePackageOnBuild` in the client csproj.

## Versioning and releases
- Versioning uses Nerdbank.GitVersioning (`version.json`), tagging releases as `v<semver>`.
- Release automation runs via `release-version-and-tag.yml` then `release-publish-nuget.yml` to push the NuGet package `MX.InvisionCommunity.Api.Client`.

## CI/CD workflows
- `build-and-test.yml` builds (and executes empty tests) on changes.
- `pr-verify.yml` validates pull requests.
- `codequality.yml` runs static analysis.
- `dependabot-automerge.yml` manages bot updates.
- `release-version-and-tag.yml` and `release-publish-nuget.yml` manage version bumps and NuGet publishing.
