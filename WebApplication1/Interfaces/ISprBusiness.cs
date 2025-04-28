using Validaciones.Models;

namespace Validaciones.Interfaces
{
    public interface ISprBusiness
    {
        List<DBBoolResult> LoginValidation(LoginModel loginModel);
    }
}
