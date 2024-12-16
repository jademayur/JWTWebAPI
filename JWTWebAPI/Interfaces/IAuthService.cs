using JWTWebAPI.Models;
using JWTWebAPI.Models.RequestModels;

namespace JWTWebAPI.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);

        string Login(LoginRequest loginRequest);
    }
}
