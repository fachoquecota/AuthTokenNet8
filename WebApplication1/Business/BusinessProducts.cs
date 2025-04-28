using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Business
{
    public class BusinessProducts : IProducts
    {
        private readonly ISprProducts _sprProducts;
        public BusinessProducts(ISprProducts sprProducts)
        {
            _sprProducts = sprProducts;
        }

        public DataModel ListProducts()
        {
            var resultSP = _sprProducts.ListProducts();
            return resultSP;
        }
    }
}
