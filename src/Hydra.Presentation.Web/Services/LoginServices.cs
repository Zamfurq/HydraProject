using Hydra.Business.Interfaces;
using Hydra.DataAccess.Models;
using Hydra.Presentation.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Hydra.Presentation.Web.Services
{
    public class LoginServices
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;

        public LoginServices(IAdminRepository adminRepository, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _configuration = configuration;
        }

        public AuthenticationTicket LoginUser(UserLoginViewModel vm)
        {
            User theUser = _adminRepository.GetUser(vm.Username);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(vm.Password, theUser.Password);
            if (!isCorrectPassword)
            {
                throw new Exception("Username or Password is not correct!");
            }

            ClaimsPrincipal principal = GetPrincipal(theUser.Username, theUser.Roles.FirstOrDefault().Name);

            AuthenticationTicket authenticationTicket = GetAuthenticationTicket(principal);

            return authenticationTicket;

        }
        private ClaimsPrincipal GetPrincipal(string username, string role)
        {
            var claims = new List<Claim> {
                new Claim("username", username),
                new Claim(ClaimTypes.Role, role)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
        private AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal principal)
        {
            AuthenticationProperties authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            };

            AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties,
                                                                                CookieAuthenticationDefaults.AuthenticationScheme);

            return authenticationTicket;
        }

        public void RegisterUser(UserRegisterViewModel vm)
        {
            var user = new User
            {
                Username = vm.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                Email = vm.Email
            };
            user.Roles.Add(_adminRepository.GetRole(vm.Role));
            _adminRepository.RegisterUser(user);
        }

        public List<SelectListItem> GetRoles()
        {
            List<Role> roles = _adminRepository.GetRoles();
            List<SelectListItem> result = new List<SelectListItem>();

            foreach (Role role in roles)
            {
                result.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                });
            }

            return result;
        }
    }
}
