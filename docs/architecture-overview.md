# Architecture Overview

MX.InvisionCommunity.Api.Client is a multi-targeted .NET 9/10 REST client for the Invision Community API. It relies on RestSharp for HTTP calls, Newtonsoft.Json for payload handling, and Application Insights for dependency telemetry.

## Composition
- `InvisionApiClientOptions` requires `BaseUrl` and `ApiKey` (and optional `ApiPathPrefix`) to create a `RestClient` with Basic auth for every request.
- `BaseApi` centralizes request creation, error handling, and dependency telemetry (`InvisionRestApi` operation with target `BaseUrl`). Non-success responses log errors; 404s are surfaced to the caller when handled explicitly.
- Feature clients derive from `BaseApi`:
  - `CoreApi` exposes `GetCoreHello()` and `GetMember(string id)`, returning `null` on 404 for members.
  - `DownloadsApi` exposes `GetDownloadFile(int fileId)`.
  - `ForumsApi` posts and updates topics (`PostTopic` returns `TopicId` and `FirstPostId` parsed from the response).
- `InvisionApiClient` aggregates the feature clients behind `IInvisionApiClient`.
- `ServiceCollectionExtensions.AddInvisionApiClient` wires options and registers the feature clients plus `IInvisionApiClient` as singletons for DI use.

## Usage notes
- Ensure `BaseUrl` excludes the API path; use `ApiPathPrefix` when the API sits under a sub-path.
- The API key is injected as Basic auth (`Authorization: Basic <base64(apiKey)>`).
- Dependency telemetry is emitted per request; exceptions from RestSharp are tracked and re-thrown for caller handling.
- Models under `Models/` map the current subset of API payloads (members, downloads, forum posts); extend them when adding endpoints.
