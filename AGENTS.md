# invision-api-client

Multi-target .NET REST client for the Invision Community API. It publishes abstractions, client implementation, and consumer testing-helper packages.

## Locations

- Solution: `src/MX.InvisionCommunity.slnx`
- Packages: `src/MX.InvisionCommunity.Api.Abstractions`, `src/MX.InvisionCommunity.Api.Client`, `src/MX.InvisionCommunity.Api.Client.Testing`
- Tests: matching `*.Tests` projects under `src/`
- Architecture and workflows: `docs/`

## Commands

```pwsh
dotnet build src/MX.InvisionCommunity.slnx
dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName!~IntegrationTests"
dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName~MyTestClass.MyTestMethod"
dotnet format src/MX.InvisionCommunity.slnx --verify-no-changes
```

## Constraints

- Preserve interfaces and DTOs in the abstractions package and keep endpoint implementations aligned with them.
- Keep request creation, authentication, telemetry, and error handling centralized in the client infrastructure.
- Extend the published testing package whenever a public endpoint or DTO contract changes.
- Never expose API keys or credentials.
- Keep package identities, target frameworks, package READMEs, and `version.json` behavior unchanged unless explicitly requested.
- Build generates packages; do not publish them during validation.

## Documentation

- [Architecture overview](docs/architecture-overview.md)
- [Development workflows](docs/development-workflows.md)
