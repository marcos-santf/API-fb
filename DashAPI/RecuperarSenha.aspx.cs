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
using System.Security.Policy;
using System.Web.Services.Description;

namespace DashAPI
{
    public partial class RecuperarSenha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Value) && txtEmail.Disabled == false)
                ValidaDadosBanco(txtEmail.Value);
            else if(string.IsNullOrEmpty(txtEmail.Value) && txtEmail.Disabled == true || string.IsNullOrEmpty(txtSenha.Value) || txtNovaSenha.Value != "" || txtConfNovaSenha.Value != "")
                AlteraSenha(txtEmail.Value);
        }
        private void ValidaDadosBanco(string email)
        {
            clsValidaDados validaDados = new clsValidaDados();

            DataTable ds = clsEnviaEmail.ValidaEmail(email);

            try
            {
                if (ds.Rows.Count > 0)
                {
                    string novaSenha = clsEnviaEmail.AlteraSenha(email);

                    string assunto = "Recuperação de Senha";
                    string corpo = "Nova Senha: " + novaSenha;
                    string diretorio = ""; // Não há anexo, portanto, deixe o diretório vazio
                    string[] emails = new string[] { email };

                    clsEnviaEmail.EnviarEmail(assunto, corpo, diretorio, emails);

                    clsValidaDados.ClientMessage(this, "E-mail enviado com sucesso.");

                    txtEmail.Disabled = true;
                    a.Visible = b.Visible = c.Visible = true;
                    btnEnviar.InnerText = "Alterar";
                }
                else
                {
                    clsValidaDados.ClientMessage(this, "E-mail não cadastrado no sistema.");
                    return;
                }
            }
            catch (Exception ex)
            {
                clsValidaDados.ClientMessage(this, "Erro ao enviar e-mail: " + ex.Message);
                return;
            }
        }

        private void AlteraSenha(string email)
        {
            txtEmail.Value = email;

            if (txtEmail.Value == "" || txtSenha.Value == "" || txtNovaSenha.Value == "" || txtConfNovaSenha.Value == "")
            {
                clsValidaDados.ClientMessage(this, "Informar todos os campos.");
                return;
            }

            if(txtNovaSenha.Value != txtConfNovaSenha.Value)
            {
                clsValidaDados.ClientMessage(this, "As senhas não estao iguais.");
                return;
            }

            DataTable ds = clsEnviaEmail.ValidaEmail(email);

            if(ds.Rows.Count > 0)
            {
                if(Convert.ToBoolean(ds.Rows[0]["fg_recupera_senha"]))
                {
                    string senhaAtual = clsCriptografia.Encrypt(txtSenha.Value, "Eita#$%Nois##", true);

                    if(ds.Rows[0]["senha_usuario"].ToString() != senhaAtual)
                    {
                        clsValidaDados.ClientMessage(this, "Informar senha enviada por e-mail.");
                        return;
                    }

                    if(clsEnviaEmail.AlteraSenhaNova(email, txtNovaSenha.Value))
                    {
                        clsValidaDados.ClientMessage(this, "Senha alterada com sucesso.");
                        Response.Redirect("Login.aspx");
                    }
                }
            }
        }
    }
}