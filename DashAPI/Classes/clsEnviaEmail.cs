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
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace DashAPI.Classes
{
    public class clsEnviaEmail
    {
        public static DataTable ValidaEmail(string email)
        {
            DataTable DataTable = new DataTable();

            string query = @"SELECT * FROM usuario WHERE email_usuario = @email";

            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(DataTable);
                        }
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
        public static void EnviarEmail(string assunto, string corpo, string diretorio, string[] email)
        {
            // Configuração do servidor SMTP
            string[] destinatarios = email;
            string smtpHost = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUser = "sp.marcosfilho@gmail.com";
            string smtpPassword = "b x l l x t w j c g u e w q u y";

            // Autenticação do cliente SMTP
            var credentials = new NetworkCredential(smtpUser, smtpPassword);

            // Configuração da mensagem de e-mail
            var message = new MailMessage();
            message.From = new MailAddress(smtpUser);
            foreach (string destinatario in destinatarios)
            {
                message.To.Add(destinatario);
            }
            message.Subject = assunto;
            message.Body = corpo;

            if (diretorio != "" && diretorio != null && diretorio != string.Empty)
            {
                // Adiciona o anexo
                Attachment anexo = new Attachment(diretorio);
                message.Attachments.Add(anexo);
            }

            // Configuração do cliente SMTP
            var client = new SmtpClient(smtpHost, smtpPort);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            // Envio do e-mail
            try
            {
                client.Send(message);
                //Console.WriteLine($"\nE-mail enviado com sucesso para: {string.Join(", ", destinatarios)}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
            }
        }

        public static string AlteraSenha(string email)
        {
            string senhaAleatoria = GerarSenha();
            string senhaNova = clsCriptografia.Encrypt(senhaAleatoria, "Eita#$%Nois##", true);
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE usuario SET senha_usuario = @senha, fg_bloqueado = 0, ind_tentativas_login = 0, fg_recupera_senha = 1, dt_alteracao = @data WHERE email_usuario = @email";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@senha", senhaNova);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    return senhaAleatoria;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os dados: " + ex.Message);
            }
        }

        public static string GerarSenha(int tamanho = 8)
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var senha = new StringBuilder();

            for (int i = 0; i < tamanho; i++)
            {
                int indice = random.Next(0, caracteresPermitidos.Length);
                senha.Append(caracteresPermitidos[indice]);
            }

            return senha.ToString();
        }

        public static bool AlteraSenhaNova(string email, string senhaAlterada)
        {
            string senhaNova = clsCriptografia.Encrypt(senhaAlterada, "Eita#$%Nois##", true);
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["fbDashConnectionString"].ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE usuario SET senha_usuario = @senha, fg_recupera_senha = 0, dt_alteracao = @data WHERE email_usuario = @email";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@senha", senhaNova);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@data", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar os dados: " + ex.Message);
            }
        }
    }
}