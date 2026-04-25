# Copilot Instructions

> Shared conventions: see [`.github-copilot/.github/instructions/dotnet-nuget-library.instructions.md`](../../.github-copilot/.github/instructions/dotnet-nuget-library.instructions.md) for general .NET NuGet library standards, and [`.github-copilot/.github/instructions/dotnet-api-client-libraries.instructions.md`](../../.github-copilot/.github/instructions/dotnet-api-client-libraries.instructions.md) for the typed API client patterns layered on top (three-package pattern, fluent DI builder, `ApiResult<T>` envelope, authentication options, testing-package conventions).

## Architecture
- .NET 9/10 solution in `src/MX.InvisionCommunity.sln` providing a REST client library for the Invision Community API.
- Three packages: `MX.InvisionCommunity.Api.Abstractions`, `MX.InvisionCommunity.Api.Client`, `MX.InvisionCommunity.Api.Client.Testing`.
- Built on the `MX.Api.Client` / `MX.Api.Abstractions` shared framework (see `api-client-abstractions` repo) — Polly retries, pluggable authentication, `IRestClientService` abstraction.

## Project Layout
- `MX.InvisionCommunity.Api.Abstractions/` — Interfaces (`ICoreApi`, `IDownloadsApi`, `IForumsApi`, `IInvisionApiClient`) and DTO models (all suffixed with `Dto`: `MemberDto`, `CoreHelloDto`, `DownloadFileDto`, etc.).
- `MX.InvisionCommunity.Api.Client/` — Implementation. Feature clients (`CoreApi`, `DownloadsApi`, `ForumsApi`) extend `BaseApi<InvisionApiClientOptions>`. `InvisionApiClient` aggregates them. `ServiceCollectionExtensions.AddInvisionApiClient()` uses `AddTypedApiClient<>()` for DI registration with fluent `InvisionApiClientOptionsBuilder`.
- `MX.InvisionCommunity.Api.Client.Testing/` — `FakeInvisionApiClient`, `FakeCoreApi`, `FakeDownloadsApi`, `FakeForumsApi` with thread-safe configurable responses, error simulation, call tracking, reset. `InvisionDtoFactory` provides static factory methods for all DTOs.
- `MX.InvisionCommunity.Api.Client.Testing.Tests/` — xUnit + coverlet unit tests for all fakes, factories, and DI registration.

## Repo-specific Options
- `InvisionApiClientOptions` extends `ApiClientOptionsBase` from `MX.Api.Client` with optional `ApiPathPrefix`.
- `InvisionApiClientOptionsBuilder` extends `ApiClientOptionsBuilder<TOptions, TBuilder>` for fluent configuration.

## API Surface
- Feature clients: `CoreApi` (`GetCoreHello`, `GetMember`), `DownloadsApi` (`GetDownloadFile`), `ForumsApi` (`PostTopic`, `UpdateTopic`).
- All deserialize with Newtonsoft.Json.

## Extending
- Add new endpoints by deriving from `BaseApi<InvisionApiClientOptions>`, adding an interface under `MX.InvisionCommunity.Api.Abstractions/Interfaces/`, registering via `AddTypedApiClient<>()` in `ServiceCollectionExtensions`, and updating `InvisionApiClient` to surface the client. Add corresponding fake and factory methods in the testing package.
