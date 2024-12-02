using Microsoft.AspNetCore.Mvc;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public interface ILogin
    {
        bool UserLogin(Credential c);
    }
}
