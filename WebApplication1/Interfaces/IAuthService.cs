using Validaciones.Models;

namespace Validaciones.Interfaces
{
    public interface IAuthService
    {
        (bool Success, string Message, string Token) Authenticate(LoginModel loginModel);
    }
}
