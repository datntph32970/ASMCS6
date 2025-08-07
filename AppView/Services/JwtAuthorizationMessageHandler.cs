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
                Console.WriteLine($"JwtAuthorizationMessageHandler: Token for {request.RequestUri}: {(string.IsNullOrEmpty(token) ? "null" : token.Substring(0, Math.Min(20, token.Length)) + "...")}");

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    Console.WriteLine($"JwtAuthorizationMessageHandler: Authorization header set for {request.RequestUri}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JwtAuthorizationMessageHandler error: {ex.Message}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
