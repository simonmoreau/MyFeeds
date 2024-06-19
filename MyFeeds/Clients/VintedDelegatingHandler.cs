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
        private readonly VintedClient _authenticationClient;
        private readonly ILogger<VintedDelegatingHandler> _logger;
        private readonly string cookie = "NjBRYSthR0FhZDViYlBNSFRjUGx0RTVvaXB5REM2eng0UFl5ejRobCtuZ0ZHdWljeW1ha3o5QWlscFQzVlg0YU50ZzBPanBxUTZ4TnN6RUR6Mk12UDRUY3BYdDRZSjk2eTBzR0Vjc2RJaXpFMW9DdGdQTXE4Qm1ycUpLTlh4VTUzRk94NC9iZTJ4aU5CVUZJbTJCTGZyaFZxQjJVRitlOGdpY1lwcVkxK2p3RG94UWpFbVJwR1I3L3BMRzg0T1BhYXgzY0l1a1FVbmxDREVpd0J3VDBlZ1lkUGM3V3dnMVg2K2g2YVEyYVA2Nk9YWDRvMU9peWJlMmtYb254TkVLaDRwKzRsS3RCQkt3OVhLazV4ZmlXejBydXJXQldWbFEvaTlrWldTeFZHaUVzL1Y4SmtLWkVtZ0RvcXRTR1hXdGFGb2pmeUN6dWdhcms5dTM2NVVBSzlPWnJsYVl0clBZYjN4NGdwZWYxV0JQWWk4U29GTFZqUURib2QvZlJRdmF6STRxdlVkMmpjMk12MkJ2dmFSNFM0Y3I0a1A3OVIrcGs2VStZTFBzSm54bE9Lak9CQXMxSmdvaVl0b2NlYU9RY2JMcExPbitFcEZzKzhKN3lNL1czTzhnZmVBY3k1TWNSTVU0aTFHTEdyTDgyN1ZQMjQxV3J6MFc1cGpEVVVyNEo5STJnR1BscUhIcEtzYzF1NHZYalEwNVBOM0FXMWE2d3N2RGJLeThCb0RGTVJBb0NHREdZTlJIbHZDZ1JzTTRFYTV5dXpMZm1WNTV3SHo0bndKOE1YLzBCMS9COWM0MERvd1JJZGp5QS95aEVFenYweVZ5aFBUaW8yZzdrMGNCMlhFekF2c1A0ckNWZk15UGdWZUVWT1pkS05CNVo0TGFvaXg0QUxMUlRXVXpCY3NnQkNSOC83VnlsTDFlVzF6SzIrbVpxdUZVN3hZMG01Mm5WSzViZ2NFMnZJZ3JtQTlPdXFNNFFjS0hzZkE2bVZWbTcwd3hTQmlySkNmQjFIaG9QWGxDdXRtV1E2cnV2bjRoVkpNYnF2YS9TcUl3M1Vnem9IQ0pwWFVIbGxoQkY0Z2NFTDJiR2pQL2FpbFN3TWFEaG5IMEZuOHlaRU04VGR2MHY3Vk9VazlQd2NFWVN2eXNicGxCeFBZV3piUEZoSG43RFR2UUExQitYWDFBWkdRTiswWlFjTk93eFpGQk55OHFqV2g0V3FtWlVjWTBQZU1oR3pCQThLNjJjZGpSQnZTWTg1WmZ2SmhBL3pRbXNqaXkyRFg0WVl0TWdEMnM5RUd2dFFwTExwRVFueG5DMlZxQnphNTI5ZXZVdk1xRCtvblJxRnVrd01MQStXcFpwdHNCV3pEaEkzb0grRDJoMnRMaGpMQ3NvQkMrdGJHWENXTXhzdCt5TFhEVktYMEd2Qk1mMzlrbUYwR2padXRIRGgzOVlDa01mdUlUVTBKdzVjM1pIdWthVVZNYnBKQW9PZkV6bjlndE5UNG01SHVBMlVVYm5rRHFkMnp6elc0NG9CODFJdklLZ2dGdllKRy9mVW0vNjAvbVdDL2lONWwvT3ZUZTVNZ2N0UjhHWEFRbFQxZm9TTUlYSWRoVkRTOUpQTFVEVVhYWkpEYm8wQWhxaSs2Q01MaUV5Qlp3cVlHalR5cGhuUytIa0FCWm5kY205OUlsZEIvVDRQdVFkQnRNUjlJY1NzL3l2Y1RtTzJxRXN5QVllODFyVFVOS3h2OEJmVldNdlpUNGZ0V05ZUUw3WWU3a3VlY2FiSnQ0dkhoamFJMC9WTS9SbGdNNzRiTWZKMnBYeTBXaEcwNC9SbzJJVUh0d0EwS1c5c21DMWVjL0Z1cTUwTytRQWdta3EyRlhjeGZxM1hlSDE2M1pVeHU4VVhXTWVXZkNleUZXNktFTWFRL3d6WlQ0YVNSY1hBWENUYmNUcUpTQUl6MUt6QXF5YUhJZHRyR1EzRlBHY3hWUkxNMC82eUtaMzlXR2NpY0tJZ2hHdGZkVDY2RUhnVHpzRk1EMzlYVm5qQkQwRUpVcmppdi9RWCtWeU9DdnZkWGw2ZUVBWWdMcmFONFJEenR6ZUxOazRic3kvTFRrVTFVa015alNmSnR6WW1qV2FRWFhUNHhacUtkcjAyeWZwbGFsUW55ZVZzWDU3ZHFtK2p6TU1nMjgwTGtFMnE4WTVPYTJkVGR4Z3JFRFBWWVdIT2RtRVphVVF5ZFJZTmlmbFRFSmQ3dEVqVWxTVGtIbG11Wnh4NVh1bDFBOTJ4YkNjODVjd1VGSnB5L3Z6aHBmZ3lDQkNPdXBkODBaMUhpalhBaXM3dTZ5c0VLakRkd255N1lPY0RrbVRtL2xrWFBvSzM2aGsvQU44blhkcTAzQ2RXb1NMUEtXQlIvQWEraG1LY0tCQkFTb1BiMjh6RCtyRWxPczZHcndlbnFBYXVNQ1hOUmJidndGejU4UWFDa0d1NE1HS1NEeXpyYjJieWxQWGpYUEgxNTZ3NFJOdlRCSGJpanU5d0NYbURQUE41eU5XbjRRPS0tRmhqWFZYMXdpVDJqMmxIM01ESG1wQT09--105460142e21a164346b06ffa577750496c42a8f";


        public VintedDelegatingHandler(VintedClient authenticationClient, ILogger<VintedDelegatingHandler> logger)
            : base()
        {
            _authenticationClient = authenticationClient;
            _logger = logger;
        }

        public VintedDelegatingHandler(HttpMessageHandler innerHandler,
            VintedClient authenticationClient, ILogger<VintedDelegatingHandler> logger)
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
                string accessToken = cookie;
                if (string.IsNullOrEmpty(accessToken))
                {
                    throw new Exception($"Access token is missing for the request {request.RequestUri}");
                }

                request.Headers.Add("Cookie", $"_vinted_fr_session={cookie};");

                httpResponseMessage = await base.SendAsync(request, cancellationToken);
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
