using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using DashAPI.Classes;
//using Microsoft.IdentityModel.Tokens;

namespace DashAPI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (email.Value != "" && password.Value != "")
            {
                string Param1 = string.Empty;
                Param1 = clsCriptografia.Encrypt(password.Value, "Eita#$%Nois##", true);

                ValidaDadosBanco(email.Value, Param1);

            }
            else if (Request.QueryString["Param1"] != null && Request.QueryString["Param2"] != null && Request.QueryString["Param3"] == "0")
            {
                string Param1 = string.Empty;
                Param1 = clsCriptografia.Encrypt(Request.QueryString["Param2"], "Eita#$%Nois##", true);

                ValidaDadosBanco(Request.QueryString["Param1"], Param1);
            }
        }

        private void ValidaDadosBanco(string email, string senha)
        {
            string Param1 = string.Empty;
            string Param2 = string.Empty;
            string Param3 = string.Empty;
            string Param4 = string.Empty;
            string DirecionaPagina = string.Empty;
            string CodigoUsuario = string.Empty;
            string ts = string.Empty;

            try
            {
                DataTable ds = clsValidaDados.ValidaDados(email, senha);

                if (ds.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(ds.Rows[0]["fg_bloqueado"]))
                    {
                        clsValidaDados.ClientMessage(this, "Usuário Bloqueado, clique em esqueci senha.");
                        return;
                    }
                    else
                    {
                        Param1 = HttpUtility.UrlEncode(clsCriptografia.Encrypt(Convert.ToInt32(ds.Rows[0]["tipo_usuario"]).ToString(), "Eita#$%Nois##", true));
                        Param2 = HttpUtility.UrlEncode(clsCriptografia.Encrypt(Convert.ToInt32(ds.Rows[0]["id_usuario"]).ToString(), "Eita#$%Nois##", true));
                        Param3 = HttpUtility.UrlEncode(clsCriptografia.Encrypt("home", "Eita#$%Nois##", true));
                        Param4 = HttpUtility.UrlEncode(clsCriptografia.Encrypt(email, "Eita#$%Nois##", true));

                        DirecionaPagina = Convert.ToInt32(ds.Rows[0]["tipo_usuario"]).ToString();
                        CodigoUsuario = Convert.ToInt32(ds.Rows[0]["id_usuario"]).ToString();
                    }
                }
                else
                {
                    int resultado = clsValidaDados.AlteraDados(email, 0);
                    if (resultado == 5)
                    {
                        clsValidaDados.ClientMessage(this, "Usuário ou senha incorretos.");
                        return;
                    }
                    else
                    {
                        clsValidaDados.ClientMessage(this, "Usuário ou senha incorretos.");
                        clsValidaDados.ClientMessage(this, "Tentativas restantes: " + resultado.ToString());
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsValidaDados.ClientMessage(this, "Usuário ou senha incorretos.");
                return;
            }

            //if (DirecionaPagina == "2")
            //    Response.Redirect("padraoPaciente.aspx?Param1=" + Param1 + "&Param2=" + Param2 + "&Param3=" + Param3 + "&Param4=" + Param4 + "&Param5=home");
            //else if (DirecionaPagina == "3")
            //    Response.Redirect("padraoEnfermagem.aspx?Param1=" + Param1 + "&Param2=" + Param2 + "&Param3=" + Param3 + "&Param4=" + Param4 + "&Param5=home");
            //else if (DirecionaPagina == "4")
            //    Response.Redirect("padraoMedico.aspx?Param1=" + Param1 + "&Param2=" + Param2 + "&Param3=" + Param3 + "&Param4=" + Param4 + "&Param5=home");
            if(DirecionaPagina != "" && DirecionaPagina != string.Empty && DirecionaPagina != null)
                Response.Redirect("Home.aspx?Param1=" + Param1 + "&Param2=" + Param2 + "&Param3=" + Param3 + "&Param4=" + Param4);
        }
    }
}