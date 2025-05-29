namespace SFLAPI.Services
{
    // IAuthService.cs
    public interface IAuthService
    {
        Task RegisterAsync(string name, string password, string role);
        Task<bool> ValidateAsync(string name, string password, string role);
        Task<string> GenerateTokenAsync(string name, string role);
    }


}
