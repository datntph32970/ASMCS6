using Microsoft.JSInterop;

namespace AppView.Services
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "auth_token";

        public JwtAuthorizationMessageHandler(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
                // Handle error silently
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
