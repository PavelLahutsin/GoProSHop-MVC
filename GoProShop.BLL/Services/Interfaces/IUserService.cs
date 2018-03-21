using System.Security.Claims;
using System.Threading.Tasks;
using GoProShop.BLL.DTO;

namespace GoProShop.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDTO user);

        Task<ClaimsIdentity> Authenticate(UserLoginDTO user);

        Task<UserDTO> GetUserByName(string name);

        void SignIn(ClaimsIdentity claim);

        void SignOut();

        Task<bool> IsUserExist(string userName);
    }    
}
