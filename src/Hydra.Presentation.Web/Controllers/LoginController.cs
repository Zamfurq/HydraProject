using Hydra.Presentation.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Hydra.Presentation.Web.Services;

namespace Hydra.Presentation.Web.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginServices _service;

        public LoginController(LoginServices service)
        {
            _service = service;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(UserLoginViewModel vm)
        {
            try
            {
                var authTicket = _service.LoginUser(vm);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        authTicket.Principal, authTicket.Properties);

                return RedirectToAction("Index", "Home");

            }
            catch (Exception e)
            {
                return View(vm);
            }
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View(new UserRegisterViewModel
            {
                Roles = _service.GetRoles()
            });
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegisterViewModel vm)
        {
            vm.Roles = _service.GetRoles();
            if (!ModelState.IsValid)
            {
                return View("Register", vm);
            }
            _service.RegisterUser(vm);
            return RedirectToAction("Login");
        }
    }
}
