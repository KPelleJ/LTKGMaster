using Microsoft.AspNetCore.Mvc;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public interface ILogin
    {
        Task<IActionResult> UserLogin(Credential c);
    }
}
