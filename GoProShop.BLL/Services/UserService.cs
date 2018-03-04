using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GoProShop.BLL.DTO;
using GoProShop.BLL.Services.Interfaces;
using GoProShop.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using GoProShop.DAL.Interfaces;
using GoProShop.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GoProShop.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;
        public readonly IAuthenticationManager _authenticationManager;

        public UserService(IUnitOfWork uow, IAuthenticationManager authenticationManager)
            : base(uow)
        {
            _authenticationManager = authenticationManager;
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(uow.Context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(uow.Context));
        }

        public async Task<bool> IsUserExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task Create(UserDTO user)
        {
            var userToAdd = Mapper.Map<ApplicationUser>(user);
            await _userManager.CreateAsync(userToAdd, user.Password);
            await _uow.Commit();
        }

        public async Task<ClaimsIdentity> Authenticate(UserLoginDTO userDto)
        {
            ClaimsIdentity claim = null;
            var user = await _userManager.FindAsync(userDto.Name, userDto.Password);

            if (user != null)
                claim =
                    await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<UserDTO> GetUserByName(string name)
        {
            return Mapper.Map<UserDTO>(await _userManager.FindByNameAsync(name));
        }

        public void SignIn(ClaimsIdentity claim)
        {
            var properties = new AuthenticationProperties { IsPersistent = true };

            _authenticationManager.SignOut();
            _authenticationManager.SignIn(properties, claim);
        }

        public void SignOut()
        {
            _authenticationManager.SignOut();
        }
    }
}
