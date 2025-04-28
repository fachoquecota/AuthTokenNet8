using Validaciones.Models;
using System.Data;
using System.Data.SqlClient;
using Validaciones.Interfaces;
namespace Validaciones.Data.StoreProcedures
{
    public class StoredProcedureUsers : ISprBusiness
    {
        public List<DBBoolResult> LoginValidation(LoginModel loginModel)
        {
            var oList = new List<DBBoolResult>();

            try
            {
                var cn = new DbConnection();
                using (var conexion = new SqlConnection(cn.getConnectionString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SP_UserValidator", conexion);
                    cmd.Parameters.AddWithValue("@VCH_Username", loginModel.Username);
                    cmd.Parameters.AddWithValue("@VCH_Password", loginModel.Password);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader()) 
                    {
                        if (dr.Read()) {
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
                //ErrorResult.ErrorMessage = ex.Message;
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
