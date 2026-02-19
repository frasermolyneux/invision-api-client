# Copilot Instructions

## Architecture
- .NET 9/10 solution in `src/MX.InvisionCommunity.sln` providing a REST client library for the Invision Community API.
- Three NuGet packages are published: `MX.InvisionCommunity.Api.Abstractions` (interfaces/models), `MX.InvisionCommunity.Api.Client` (typed HTTP client), and `MX.InvisionCommunity.Api.Client.Testing` (in-memory fakes and DTO factories for consumer test projects).
- Built on the `MX.Api.Client` / `MX.Api.Abstractions` shared framework providing `BaseApi<TOptions>`, `ApiResult<T>` response envelopes, retry policies (Polly exponential backoff), pluggable authentication, and `IRestClientService` abstraction.

## Project Layout
- `MX.InvisionCommunity.Api.Abstractions/` — Interfaces (`ICoreApi`, `IDownloadsApi`, `IForumsApi`, `IInvisionApiClient`) and DTO models (all suffixed with `Dto`: `MemberDto`, `CoreHelloDto`, `DownloadFileDto`, etc.). Internal setters on DTOs; `InternalsVisibleTo` grants access to client, testing, and test projects.
- `MX.InvisionCommunity.Api.Client/` — Implementation. Feature clients (`CoreApi`, `DownloadsApi`, `ForumsApi`) extend `BaseApi<InvisionApiClientOptions>`. `InvisionApiClient` aggregates them. `ServiceCollectionExtensions.AddInvisionApiClient()` uses `AddTypedApiClient<>()` for DI registration with fluent `InvisionApiClientOptionsBuilder`.
- `MX.InvisionCommunity.Api.Client.Testing/` — `FakeInvisionApiClient`, `FakeCoreApi`, `FakeDownloadsApi`, `FakeForumsApi` (thread-safe fakes with configurable responses, error simulation, call tracking, reset). `InvisionDtoFactory` provides static factory methods for all DTOs. `AddFakeInvisionApiClient()` DI extension for integration tests.
- `MX.InvisionCommunity.Api.Client.Testing.Tests/` — xUnit + coverlet unit tests for all fakes, factories, and DI registration.

## Options & Configuration
- `InvisionApiClientOptions` extends `ApiClientOptionsBase` from `MX.Api.Client` with optional `ApiPathPrefix`.
- `InvisionApiClientOptionsBuilder` extends `ApiClientOptionsBuilder<TOptions, TBuilder>` for fluent configuration.
- DI registration: `services.AddInvisionApiClient(builder => builder.WithBaseUrl("...").WithApiKeyAuthentication("..."))`.

## API Methods & Response Pattern
- All methods return `ApiResult<T>` (from `MX.Api.Abstractions`) wrapping `HttpStatusCode`, `ApiResponse<T>` (with `Data`, `Errors`), and helpers (`IsSuccess`, `IsNotFound`).
- All async methods accept `CancellationToken cancellationToken = default`.
- Methods never throw; client-side errors return `ApiResult` with `HttpStatusCode.InternalServerError` and `ApiError("CLIENT_ERROR", "...")`.
- Feature clients: `CoreApi` (`GetCoreHello`, `GetMember`), `DownloadsApi` (`GetDownloadFile`), `ForumsApi` (`PostTopic`, `UpdateTopic`). All deserialize with Newtonsoft.Json.

## Build/Test
- `dotnet build src/MX.InvisionCommunity.sln`
- `dotnet test src/MX.InvisionCommunity.sln` — xUnit unit tests for testing package. Test framework: xUnit + coverlet + native assertions.
- Packages are produced on build (`GeneratePackageOnBuild`).

## Versioning & Release
- Nerdbank.GitVersioning (`version.json`) sets package versions.
- Release automation: `release-version-and-tag.yml` → `release-publish-nuget.yml` to push NuGet packages.

## Extending
- Add new endpoints by deriving from `BaseApi<InvisionApiClientOptions>`, adding an interface under `MX.InvisionCommunity.Api.Abstractions/Interfaces/`, registering via `AddTypedApiClient<>()` in `ServiceCollectionExtensions`, and updating `InvisionApiClient` to surface the client. Add corresponding fake and factory methods in the testing package.
