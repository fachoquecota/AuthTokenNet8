using Validaciones.Models;

namespace Validaciones.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(LoginModel loginModel);
    }
}
