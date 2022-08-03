using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Simple_E_Commerce.App.Models;
using Simple_E_Commerce.Data.Models;
using Simple_E_Commerce.Data.Repositories;
using System.Security.Claims;

namespace Simple_E_Commerce.App.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(accountRegisterViewModel);
            }

            if (_userRepository.IsExistUserByUserName(accountRegisterViewModel.UserName.ToLower()))
            {
                ModelState.AddModelError("UserName", "This UserName has been registered ...!");
                return View(accountRegisterViewModel);
            }

            if (_userRepository.IsExistUserByEmail(accountRegisterViewModel.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "This Email has been registered ...!");
                return View(accountRegisterViewModel);
            }

            _userRepository.AddUser(new User()
            {
                UserName = accountRegisterViewModel.UserName.ToLower(),
                Password = accountRegisterViewModel.Password,
                Email = accountRegisterViewModel.Email.ToLower(),
                IsManager = false,
                RegisterDate = DateTime.Now
            });
            return View("RegisterSuccess",accountRegisterViewModel);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel accountLoginViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(accountLoginViewModel);
            }

            var user = _userRepository.GetUserForLogin(accountLoginViewModel.Email.ToLower(), accountLoginViewModel.Password);

            if(user == null)
            {
                ModelState.AddModelError("Email", "Email or Password is not correct...!");
                return View(accountLoginViewModel);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                // new Claim ("OtherProperty", user.OtherProperty)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = accountLoginViewModel.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }
    }
}
