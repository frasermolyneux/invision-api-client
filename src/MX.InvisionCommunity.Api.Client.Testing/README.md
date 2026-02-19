# MX.InvisionCommunity.Api.Client.Testing

Test helpers for consumer apps using the Invision Community API client. Provides in-memory fakes, DTO factory methods, and DI extensions for integration tests.

## Usage

### DI Registration (Integration Tests)

```csharp
services.AddFakeInvisionApiClient(fake =>
{
    fake.CoreApi.AddMemberResponse("123", InvisionDtoFactory.CreateMember(id: 123, name: "Test User"));
});
```

### Direct Usage (Unit Tests)

```csharp
var fake = new FakeInvisionApiClient();
fake.CoreApi.AddMemberResponse("123", InvisionDtoFactory.CreateMember(id: 123));

var result = await fake.Core.GetMember("123");
Assert.True(result.IsSuccess);
```
