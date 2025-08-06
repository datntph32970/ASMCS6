namespace AppView.Services
{
    public interface IJwtService
    {
        string? GetToken();
        void SetToken(string token);
        void ClearToken();
        bool IsAuthenticated();
    }
}