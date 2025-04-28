using System.Data;
using System.Data.SqlClient;
using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Data.StoreProcedures
{
    public class StoredProcedureProducts : ISprProducts
    {
        private readonly DbConnection _dbConnection;

        // Constructor: Inyectar DbConnection como dependencia
        public StoredProcedureProducts(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public DataModel ListProducts()
        {
            var oList = new List<Products>();
            var response = new DataModel();

            try
            {
                // Usar la cadena de conexión proporcionada por DbConnection
                using (var conexion = new SqlConnection(_dbConnection.GetConnectionString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_ListAllProducts", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oList.Add(new Products()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"]),
                                Quantity = Convert.ToInt32(dr["Quantity"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"]),
                                CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                            });
                        }
                    }
                }

                // Respuesta exitosa
                response.Data = oList;
                response.Message = "Productos listados exitosamente";
                return response;
            }
            catch (Exception ex)
            {
                // Respuesta en caso de error
                response.Data = oList;
                response.Message = $"Error: {ex.Message}";
                return response;
            }
        }
    }
}