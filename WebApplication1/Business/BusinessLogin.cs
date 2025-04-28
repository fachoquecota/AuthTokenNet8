using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Business
{
    public class BusinessLogin : IBusinessLogin
    {
        private readonly ISprBusiness _sprBusiness;
        public BusinessLogin(ISprBusiness sprBusiness) 
        { 
            _sprBusiness = sprBusiness;
        }
        public DBBoolResult LoginValidation(LoginModel loginModel) 
        { 
            var resultSP = _sprBusiness.LoginValidation(loginModel);
            bool isAuthenticated = resultSP.FirstOrDefault()?.result == true;

            return new DBBoolResult { 
                result = isAuthenticated,
                value = isAuthenticated ? "Autenticación aprobada!" : "Usuario o contraseña incorrecta"
            };
        }
    }
}
