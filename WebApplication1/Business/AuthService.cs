using System.Diagnostics.CodeAnalysis;
using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Business
{
    public class AuthService : IAuthService
    {
        private readonly IBusinessLogin _businessLogin;
        private readonly ITokenService _tokenService;
        
        public AuthService(IBusinessLogin businessLogin, ITokenService tokenService)
        {
            _businessLogin = businessLogin;
            _tokenService = tokenService;
        }
        public (bool Success, string Message, string Token) Authenticate(LoginModel loginModel) 
        {
            if (loginModel == null) 
            {
                return (false, "Invalid login model", null);
            }
            var validationResult = _businessLogin.LoginValidation(loginModel);
            if (!validationResult.result) 
            { 
                return (false, validationResult.value, null);
            }

            var token = _tokenService.GenerateToken(loginModel);
            return (true, validationResult.value, token);
        }
    }
}
