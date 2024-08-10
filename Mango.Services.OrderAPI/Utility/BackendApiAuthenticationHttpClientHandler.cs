using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Mango.Services.OrderAPI.Utility
{
    public class BackendApiAuthenticationHttpClientHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor _acccessor;
        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor acccessor)
        {
            _acccessor = acccessor;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var token = await _acccessor.HttpContext.GetTokenAsync("access_token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
