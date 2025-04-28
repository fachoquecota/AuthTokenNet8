using Validaciones.Models;

namespace Validaciones.Interfaces
{
    public interface IBusinessLogin
    {
        DBBoolResult LoginValidation(LoginModel loginModel);
    }
}
