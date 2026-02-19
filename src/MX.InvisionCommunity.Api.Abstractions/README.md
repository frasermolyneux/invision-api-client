# MX.InvisionCommunity.Api.Abstractions

Abstraction layer providing interfaces and models for the Invision Community API client. This package contains no implementation details and is suitable for use in consumer projects that only need the contract definitions.

## Interfaces

- `IInvisionApiClient` — Main entry point aggregating feature-specific APIs
- `ICoreApi` — Core API operations (hello, member lookup)
- `IDownloadsApi` — Downloads file retrieval
- `IForumsApi` — Forum topic creation and updates

## Models

All DTOs follow the `*Dto` naming convention and use `Newtonsoft.Json` attributes for serialization.
