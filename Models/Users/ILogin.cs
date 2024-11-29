using Microsoft.AspNetCore.Mvc;

namespace LTKGMaster.Models.Users
{
    public interface ILogin
    {
        Task<IActionResult> UserLogin(Credential c);
    }
}
