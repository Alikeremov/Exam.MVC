using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pigga.Mvc.Exam.Areas.Manage.ViewModels;
using Pigga.Mvc.Exam.Models;
using Pigga.Mvc.Exam.Utilites.Enums;

namespace Pigga.Mvc.Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if(!ModelState.IsValid) return View(registerVm);
            AppUser user = new AppUser
            {
                Name = registerVm.Name,
                SurName = registerVm.SurName,
                Email = registerVm.Email,
                UserName = registerVm.UserName,
            };
            var result= await _userManager.CreateAsync(user,registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            await _userManager.AddToRoleAsync(user,UserRoles.Admin.ToString());
            await _signInManager.SignInAsync(user, false);
            
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if(!ModelState.IsValid) return View(loginVm);
            AppUser user = await _userManager.FindByEmailAsync(loginVm.UserNameOrEmail);
            if (user==null)
            {
                user= await _userManager.FindByNameAsync(loginVm.UserNameOrEmail);
                if (user==null)
                {
                    ModelState.AddModelError(string.Empty, "Username , password or email was wrong");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.IsRemember, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username , password or email was wrong");
                return View();
            }
            if(result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "You are Locked please try some minute late");
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole {Name= item.ToString()});
                }
            }
            return RedirectToAction("Index", "Home", new { area = "" });

        }
    }
}
