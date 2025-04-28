using Validaciones.Models;
using System.Data;
using System.Data.SqlClient;
using Validaciones.Interfaces;

namespace Validaciones.Data.StoreProcedures
{
    public class StoredProcedureUsers : ISprBusiness
    {
        private readonly DbConnection _dbConnection;

        // Constructor: Inyectar DbConnection como dependencia
        public StoredProcedureUsers(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<DBBoolResult> LoginValidation(LoginModel loginModel)
        {
            var oList = new List<DBBoolResult>();

            try
            {
                // Usar la cadena de conexión proporcionada por DbConnection
                using (var conexion = new SqlConnection(_dbConnection.GetConnectionString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_UserValidator", conexion);
                    cmd.Parameters.AddWithValue("@VCH_Username", loginModel.Username);
                    cmd.Parameters.AddWithValue("@VCH_Password", loginModel.Password);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            oList.Add(new DBBoolResult()
                            {
                                result = Convert.ToBoolean(dr["Result"])
                            });
                        }
                    }
                }
                return oList;
            }
            catch (Exception ex)
            {
                // Manejar errores y devolver un resultado con el mensaje de error
                oList.Add(new DBBoolResult()
                {
                    result = false,
                    value = ex.Message
                });
                return oList;
            }
        }
    }
}