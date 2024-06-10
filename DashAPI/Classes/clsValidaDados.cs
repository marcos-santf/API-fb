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
    public class clsValidaDados
    {
        public static DataTable ValidaDados(string email, string senha)
        {
            DataTable DataTable = new DataTable();

            string query = @"
            SELECT u.*, e.*, ec.*, ep.*
            FROM usuario u
            INNER JOIN empresa e ON u.id_usuario = e.id_usuario
            LEFT JOIN empresa_campanhas ec ON e.id_empresa = ec.id_empresa
            LEFT JOIN empresa_pixels ep ON e.id_empresa = ep.id_empresa
            WHERE u.email_usuario = @email AND u.senha_usuario = @senha";

            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@senha", senha);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(DataTable);
                        }

                        // Retorne o dataSet após o preenchimento bem-sucedido
                        return DataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção, se necessário
                throw new Exception("Ocorreu um erro ao recuperar os dados: " + ex.Message);
            }
        }

        public static int AlteraDados(string email, int tipo)
        {
            int tentativas = 0;
            if (tipo == 0)
            {
                tentativas = ObterTentativasLogin(email);
                if (tentativas < 4)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                        {
                            connection.Open();

                            string query = "UPDATE usuario SET ind_tentativas_login = @tentativa, dt_alteracao = @data WHERE email_usuario = @email";
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@tentativa", tentativas + 1);
                                cmd.Parameters.AddWithValue("@email", email);
                                cmd.Parameters.AddWithValue("@data", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }

                            tentativas = 3 - tentativas; // Reduzindo uma tentativa
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Ocorreu um erro ao atualizar os dados: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                        {
                            connection.Open();

                            string query = "UPDATE usuario SET fg_bloqueado = 1, dt_alteracao = @data WHERE email_usuario = @email";
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                cmd.Parameters.AddWithValue("@email", email);
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
            return tentativas;
        }


        public static int ObterTentativasLogin(string email)
        {
            int tentativas = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT ind_tentativas_login FROM usuario WHERE email_usuario = @email";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            tentativas = Convert.ToInt32(result);
                        else
                            tentativas = 5;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter o número de tentativas de login: " + ex.Message);
            }
            return tentativas;
        }

        public static void ClientMessage(Control control, string strMessage)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            s.Append("\n<SCRIPT LANGUAGE='JavaScript'>\n");
            s.Append("	alert('" + strMessage.Replace("'", "") + "');\n");
            s.Append("</SCRIPT>");
            control.Page.ClientScript.RegisterClientScriptBlock(typeof(clsValidaDados), "ShowMessage", s.ToString());
        }
    }
}