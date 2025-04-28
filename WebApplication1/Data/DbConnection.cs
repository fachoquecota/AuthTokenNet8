namespace Validaciones.Data
{
    
    public class DbConnection
    {
        private string _connectionString = string.Empty;
        public DbConnection() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _connectionString = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public string getConnectionString()
        {
            return _connectionString; 
        }
    }
}
