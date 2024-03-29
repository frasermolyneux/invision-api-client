﻿using System.Net;

using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using RestSharp;

using XtremeIdiots.InvisionCommunity.Interfaces;
using XtremeIdiots.InvisionCommunity.Models;

namespace XtremeIdiots.InvisionCommunity.Api
{
    public class CoreApi : BaseApi, ICoreApi
    {
        public CoreApi(ILogger<CoreApi> logger, IOptions<InvisionApiClientOptions> options, TelemetryClient telemetryClient) : base(logger, options, telemetryClient)
        {
        }

        public async Task<CoreHello?> GetCoreHello()
        {
            var request = CreateRequest("api/core/hello", Method.Get);

            var response = await ExecuteAsync(request);

            if (response.Content != null)
                return JsonConvert.DeserializeObject<CoreHello>(response.Content);
            else
                throw new Exception($"Response of {request.Method} to '{request.Resource}' has no content");
        }

        public async Task<Member?> GetMember(string id)
        {
            var request = CreateRequest($"api/core/members/{id}", Method.Get);

            var response = await ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            if (response.Content != null)
                return JsonConvert.DeserializeObject<Member>(response.Content);
            else
                throw new Exception($"Response of {request.Method} to '{request.Resource}' has no content");
        }
    }
}
