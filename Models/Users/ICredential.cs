namespace LTKGMaster.Models.Users
{
    public interface ICredential
    {
        string Email { get; set; }
        string Password { get; set; }
        string? PasswordHash { get; set; }
        bool RememberMe { get; set; }
    }
}