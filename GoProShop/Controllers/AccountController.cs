using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.ViewModels;

namespace GoProShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
       
        public AccountController(
            IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public ActionResult Login() => PartialView();

        public async Task<ActionResult> Edit()
        {
            var userToEdit = Mapper.Map<UserEditVM>(await _userService.GetUserByName(User.Identity.Name));
            return View(userToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginVM model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (!ModelState.IsValid)
                return PartialView("Login", model);

            var claim = await _userService.Authenticate(Mapper.Map<UserLoginDTO>(model));

            if (claim != null)
            {
                _userService.SignIn(claim);
                var result = new { success = true };
                return Json(result);
            }
            ModelState.AddModelError("", "Wrong login or password");
            return PartialView("Login", model);
        }

        public ActionResult Register() => PartialView();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterVM user)
        {  
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            if (!ModelState.IsValid)
                return PartialView("Register", user);
            
            await _userService.Create(Mapper.Map<UserDTO>(user));
            var result = new { success = true };
            return Json(result);
        }

        public ActionResult Logout()
        {
            _userService.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}