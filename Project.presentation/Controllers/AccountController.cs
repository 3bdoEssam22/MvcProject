using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.DataAccess.Models.IdentityModel;
using Project.presentation.Utilities;
using Project.presentation.ViewModels;

namespace Project.presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register

        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new ApplicationUser
            {
                UserName = viewModel.UserName,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            };
            var Result = _userManager.CreateAsync(user, viewModel.Password).Result;
            if (Result.Succeeded) return RedirectToAction("Login", "Account");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }
        }
        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel ViewModel)
        {
            if (!ModelState.IsValid) return View(ViewModel);

            var User = _userManager.FindByEmailAsync(ViewModel.Email).Result;

            if (User is not null)
            {
                bool Flag = _userManager.CheckPasswordAsync(User, ViewModel.Password).Result;
                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, ViewModel.Password, ViewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is not Allowed");

                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Locked Out");

                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
                ModelState.AddModelError(String.Empty, "Invalid Login");

            return View(ViewModel);
            //_userManager.CheckPasswordAsync
        }

        #endregion

        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    // BaseUrl/Account/ResetPassword?email=abdulrahman@gmail.com
                    var ResetPasswordLink = Url.Action("ResetPawword", "Account", new { email = viewModel.Email, Token }, Request.Scheme);
                    var email = new Email
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink //TODO
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);




        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["Email"] = email;
            TempData["Token"] = Token;

            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string email = TempData["Email"] as string ?? string.Empty;
            string token = TempData["Token"] as string ?? string.Empty;

            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
                var Result = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                {
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(nameof(Login), viewModel);

        }

        #endregion

    }
}
