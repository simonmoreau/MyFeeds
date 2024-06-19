using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace MyFeeds.Clients
{
    public class VintedDelegatingHandler : DelegatingHandler
    {
        private readonly VintedAuthenticationClient _authenticationClient;
        private readonly ILogger<VintedDelegatingHandler> _logger;

        public VintedDelegatingHandler(VintedAuthenticationClient authenticationClient, ILogger<VintedDelegatingHandler> logger)
            : base()
        {
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        public VintedDelegatingHandler(HttpMessageHandler innerHandler,
            VintedAuthenticationClient authenticationClient, ILogger<VintedDelegatingHandler> logger)
            : base(innerHandler)
        {
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                string accessToken = await _authenticationClient.GetSessionCookie();
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
                }

                request.Headers.Add("Cookie", $"_vinted_fr_session={accessToken};");

                httpResponseMessage = await base.SendAsync(request, cancellationToken);

                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _authenticationClient.ClearCachedCookie();

                    accessToken = await _authenticationClient.GetSessionCookie();
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        throw new Exception($"Access token is missing for the request {request.RequestUri}");
                    }

                    request.Headers.Add("Cookie", $"_vinted_fr_session={accessToken};");

                    httpResponseMessage = await base.SendAsync(request, cancellationToken);

                }

                httpResponseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
                throw;
            }
            return httpResponseMessage;
        }
    }
}
