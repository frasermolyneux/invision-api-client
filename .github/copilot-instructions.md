# Copilot Instructions

This repository publishes a typed Invision Community API client as three packages: abstractions, implementation, and consumer testing helpers.

## Runtime and layout

- SDK: `10.0.301` from `global.json`.
- Package projects target `net9.0` and `net10.0`; test projects target `net9.0`.
- Solution: `src/MX.InvisionCommunity.slnx`.
- Packages: `MX.InvisionCommunity.Api.Abstractions`, `MX.InvisionCommunity.Api.Client`, and `MX.InvisionCommunity.Api.Client.Testing`.

## Repository rules

- Keep endpoint interfaces and DTOs in the abstractions package aligned with feature clients and the aggregate `IInvisionApiClient`.
- Centralize request creation, Basic authentication, telemetry, error handling, and response handling in the client infrastructure.
- Use `ApiPathPrefix` for deployments below a base path; do not embed deployment-specific paths in endpoint clients.
- Preserve documented 404 behavior and rethrow transport exceptions after telemetry records them.
- Public fakes and DTO factories are consumer contracts; update them alongside relevant public API additions.
- Never expose API keys or credentials.
- Package IDs, target frameworks, package READMEs, generated package metadata, and NBGV configuration in `version.json` are release boundaries.

## Validation

```pwsh
dotnet build src/MX.InvisionCommunity.slnx
dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName!~IntegrationTests"
dotnet test src/MX.InvisionCommunity.slnx --filter "FullyQualifiedName~MyTestClass.MyTestMethod"
dotnet format src/MX.InvisionCommunity.slnx --verify-no-changes
```

See `docs/architecture-overview.md` for composition, authentication, and endpoint behavior.
