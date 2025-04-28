using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Validaciones.Interfaces;

namespace Validaciones.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISprProducts _sprProducts;
        public ProductsController(ISprProducts sprProducts)
        {
            _sprProducts = sprProducts;
        }
        [HttpGet]
        [Route("GetProducts")]
        public dynamic GetProducts()
        {
            var result = _sprProducts.ListProducts();
            if (result is null) 
            {
                return BadRequest(new { message = "Credenciales incorrectas"});
            }
            return new
            {
                message = result
            };

        }
    }
}
