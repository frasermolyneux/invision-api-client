# MX.InvisionCommunity.Api.Client

Invision Community API client packaged as `MX.InvisionCommunity.Api.Client` on NuGet.

## Build & test
- `dotnet build src/MX.InvisionCommunity.sln`
- `dotnet test src` (tests not yet present; keeps workflow compatibility)

## Releases
- Versioning uses Nerdbank.GitVersioning (`version.json`) with tags `v<semver>`.
- CI/CD aligns with `cod-demo-reader`: feature/bugfix/hotfix pushes run Build and Test; PRs run PR Verify; main pushes run Release - Version and Tag, followed by Release - Publish NuGet.

## Package info
- Package Id: `MX.InvisionCommunity.Api.Client`
- Repository: https://github.com/frasermolyneux/invision-api-client
