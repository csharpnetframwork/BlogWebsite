using BlogWebsite.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    public class Acount : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> userSignin;

        public Acount(UserManager<IdentityUser>userManager,SignInManager<IdentityUser>userSignin)
        {
            this.userManager = userManager;
            this.userSignin = userSignin;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel registerViewModel) 
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };
            var identityResult =await userManager.CreateAsync(identityUser,registerViewModel.Password);
            if(identityResult.Succeeded)
            {
                var roleIdentityResults = await userManager.AddToRoleAsync(identityUser, "User");
                if(roleIdentityResults.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var re = await userSignin.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            if (re != null && re.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await userSignin.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
