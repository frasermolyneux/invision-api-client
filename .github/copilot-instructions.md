# Copilot Instructions

- Purpose: REST client library for the Invision Community API, packaged as `MX.InvisionCommunity.Api.Client` for .NET 9/10.
- Layout: code lives under `src/MX.InvisionCommunity.Api.Client/` with feature clients in `Api/`, interfaces in `Interfaces/`, models in `Models/`, DI helpers in `ServiceCollectionExtensions.cs`, and shared plumbing in `BaseApi.cs`.
- Options: `InvisionApiClientOptions` requires `BaseUrl` and `ApiKey`, with optional `ApiPathPrefix`; missing values throw during construction.
- HTTP behavior: `BaseApi` builds a RestSharp `RestClient`, injects Basic auth using the API key, and emits Application Insights dependency telemetry (`InvisionRestApi` with target `BaseUrl`). Non-success responses log errors; 404 handling is explicit where used.
- Feature clients: `CoreApi` (`GetCoreHello`, `GetMember` returning `null` on 404), `DownloadsApi` (`GetDownloadFile`), `ForumsApi` (`PostTopic` returning `TopicId`/`FirstPostId`, `UpdateTopic`). All deserialize with Newtonsoft.Json and throw when responses lack content.
- Aggregation: `InvisionApiClient` exposes the feature clients via `IInvisionApiClient`. `ServiceCollectionExtensions.AddInvisionApiClient` registers options and all clients as singletons for DI consumption.
- Build/test: `dotnet build src/MX.InvisionCommunity.sln`; `dotnet test src` (no tests yet, retained for workflow parity). Packages are produced on build (`GeneratePackageOnBuild`).
- Versioning & release: Nerdbank.GitVersioning (`version.json`) sets package versions; release automation runs `release-version-and-tag.yml` then `release-publish-nuget.yml` to push the NuGet package.
- CI/CD workflows: `build-and-test.yml`, `pr-verify.yml`, `codequality.yml`, `dependabot-automerge.yml`, `release-version-and-tag.yml`, `release-publish-nuget.yml`.
- Telemetry: callers must supply a configured `TelemetryClient` (e.g., via DI); exceptions from RestSharp are tracked and re-thrown.
- Extending: add new endpoints by deriving from `BaseApi`, adding an interface under `Interfaces/`, registering in `ServiceCollectionExtensions`, and updating `InvisionApiClient` to surface the client.
