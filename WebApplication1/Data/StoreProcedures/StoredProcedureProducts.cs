using System.Data;
using System.Data.SqlClient;
using Validaciones.Interfaces;
using Validaciones.Models;

namespace Validaciones.Data.StoreProcedures
{
    public class StoredProcedureProducts : ISprProducts
    {
        public DataModel ListProducts() 
        {
            var oList = new List<Products>();
            var response = new DataModel();

            try
            {
                var cn = new DbConnection();
                using (var conexion = new SqlConnection(cn.getConnectionString()))
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
                response.Data = oList;
                response.Message = "Productos listados exitosamente";
                return response;
            }
            catch (Exception ex)
            {
                response.Data = oList;
                response.Message = $"Error: {ex.Message}";
                return response;
            }
        }
    }
}
