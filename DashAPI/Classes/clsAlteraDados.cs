using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Web.Services.Description;

namespace DashAPI.Classes
{
    public class clsAlteraDados
    {
        public static void DadosEmpresa(string TokenFacebook, string appID, int CodigoUsuario, int CodigoEmpresa)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE empresa SET app_id = @app_ID, access_token = @tokenFB, dt_token = @data, dt_alteracao = @data WHERE id_usuario = @id_usuario AND id_empresa = @id_empresa";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@tokenFB", TokenFacebook);
                        cmd.Parameters.AddWithValue("@app_ID", appID);
                        cmd.Parameters.AddWithValue("@id_usuario", CodigoUsuario);
                        cmd.Parameters.AddWithValue("@id_empresa", CodigoEmpresa);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os dados: " + ex.Message);
            }
        }
    }
}