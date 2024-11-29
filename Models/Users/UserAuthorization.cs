using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LTKGMaster.Models.Users
{
    //Author Kasper
    public class UserAuthorization
    {
        
        private readonly IHttpContextAccessor _contextAccessor;

        public UserAuthorization(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> AuthorizeAsync(IUser user)
        {
            //Creating security context
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email,user.CredMail),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("SignUpDate",user.SignUpDate.ToString()),
                    new Claim("Rating",user.Rating.ToString()),
                    new Claim("City", user.City)
                };

            var identity = new ClaimsIdentity(claims, CookieConstants.CookieName);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = user.Credential.RememberMe
            };

            await _contextAccessor.HttpContext.SignInAsync(CookieConstants.CookieName, principal, authProperties);
            
            return new RedirectToPageResult("/Index");
        }
        
    }
}
