using MX.Api.Client.Configuration;

namespace MX.InvisionCommunity.Api.Client
{
    public class InvisionApiClientOptions : ApiClientOptionsBase
    {
        public string? ApiPathPrefix { get; set; }

        public override void Validate()
        {
            base.Validate();
        }
    }
}