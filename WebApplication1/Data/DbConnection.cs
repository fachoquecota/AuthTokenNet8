using Microsoft.Extensions.Configuration;

namespace Validaciones.Data
{
    public class DbConnection
    {
        private readonly string _connectionString;

        // Constructor: Inyectar IConfiguration
        public DbConnection(IConfiguration configuration)
        {
            // Accede directamente a la clave ConnectionStrings:CadenaSQL
            _connectionString = configuration["ConnectionStrings:CadenaSQL"];
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}