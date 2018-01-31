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

namespace GoProShop.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork uow)
            : base(uow)
        {
        }

        public async Task<bool> IsUserExist(string userName)
        {
            var user = await _uow.UserManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task Create(UserDTO user)
        {
            var userToAdd = Mapper.Map<ApplicationUser>(user);
            await _uow.UserManager.CreateAsync(userToAdd, user.Password);
            await _uow.Commit();
        }

        public async Task<ClaimsIdentity> Authenticate(UserLoginDTO userDto)
        {
            ClaimsIdentity claim = null;
            var user = await _uow.UserManager.FindAsync(userDto.Name, userDto.Password);

            if (user != null)
                claim =
                    await _uow.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<UserDTO> GetUserByName(string name)
        {
            return Mapper.Map<UserDTO>(await _uow.UserManager.FindByNameAsync(name));
        }

        public void SignIn(ClaimsIdentity claim)
        {
            var properties = new AuthenticationProperties { IsPersistent = true };

            _uow.AuthenticationManager.SignOut();
            _uow.AuthenticationManager.SignIn(properties, claim);
        }

        public void SignOut()
        {
            _uow.AuthenticationManager.SignOut();
        }
    }
}
