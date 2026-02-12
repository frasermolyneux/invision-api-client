# MX.InvisionCommunity.Api.Client
[![Build and Test](https://github.com/frasermolyneux/invision-api-client/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/build-and-test.yml)
[![Code Quality](https://github.com/frasermolyneux/invision-api-client/actions/workflows/codequality.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/codequality.yml)
[![Copilot Setup Steps](https://github.com/frasermolyneux/invision-api-client/actions/workflows/copilot-setup-steps.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/copilot-setup-steps.yml)
[![Dependabot Automerge](https://github.com/frasermolyneux/invision-api-client/actions/workflows/dependabot-automerge.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/dependabot-automerge.yml)
[![PR Verify](https://github.com/frasermolyneux/invision-api-client/actions/workflows/pr-verify.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/pr-verify.yml)
[![Release Publish NuGet](https://github.com/frasermolyneux/invision-api-client/actions/workflows/release-publish-nuget.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/release-publish-nuget.yml)
[![Release Version and Tag](https://github.com/frasermolyneux/invision-api-client/actions/workflows/release-version-and-tag.yml/badge.svg)](https://github.com/frasermolyneux/invision-api-client/actions/workflows/release-version-and-tag.yml)

## Documentation
- [Development Workflows](docs/development-workflows.md) - Local commands, CI/CD, and release automation
- [Architecture Overview](docs/architecture-overview.md) - Client composition, REST endpoints, and telemetry patterns

## Overview
REST client library for the Invision Community API packaged as `MX.InvisionCommunity.Api.Client` for .NET 9 and 10. Requests are issued via RestSharp with Basic auth using the API key and serialized through Newtonsoft.Json, with Application Insights dependency telemetry for every call. The DI helper `AddInvisionApiClient` wires typed clients for core, downloads, and forums endpoints plus aggregated access through `IInvisionApiClient`. Nerdbank.GitVersioning in `version.json` drives package versions published through the release workflows.

## Contributing
Please read the [contributing](CONTRIBUTING.md) guidance; this is a learning and development project.

## Security
Please read the [security](SECURITY.md) guidance; I am always open to security feedback through email or opening an issue.
