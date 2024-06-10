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
    public class clsUsuario
    {
        public static DataSet RetornaDadosUsuario(int CodigoUsuario, string dtInicio, string dtFim, string Exame ,int tipoPesquisa)
        {
            DataSet dataSet = new DataSet();

            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HOSPITALConnectionString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("pr_retorna_dados_usuario", connection))
                    {
                        try
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cd_usuario", CodigoUsuario);
                            cmd.Parameters.AddWithValue("@dt_inicio", dtInicio);
                            cmd.Parameters.AddWithValue("@dt_fim", dtFim);
                            cmd.Parameters.AddWithValue("@ds_tipo_exame", Exame);
                            cmd.Parameters.AddWithValue("@ind_tipo", tipoPesquisa);
                            
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(dataSet);
                            }

                            return dataSet;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RetornaIncluiAgenda(int CodigoUsuario, string DtAgendamento, string TipoExame, string Exame, string Hora)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HOSPITALConnectionString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("pr_inclui_agenda_exame", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cd_usuario", CodigoUsuario);
                        cmd.Parameters.AddWithValue("@dt_exame", DtAgendamento);
                        cmd.Parameters.AddWithValue("@ds_tipo_exame", TipoExame);
                        cmd.Parameters.AddWithValue("@ds_exame_agendado", Exame);
                        cmd.Parameters.AddWithValue("@ds_hora", Hora);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluiAgenda(int CodigoAgenda)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["HOSPITALConnectionString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("pr_exclui_agenda_exame", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cd_agenda", CodigoAgenda);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}