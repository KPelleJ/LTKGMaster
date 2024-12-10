using Microsoft.AspNetCore.Mvc;

namespace LTKGMaster.Models.Users
{
    /// <summary>
    /// Defines methods to be used for user login actions
    /// </summary>
    public interface ILogin
    {
        bool UserLogin(Credential c);
    }
}
