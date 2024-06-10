using DashAPI.Classes;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DashAPI
{
    public partial class Home : System.Web.UI.Page
    {
        protected TextBox authorizationCodeTextBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            string TokenFacebook = Request.QueryString["long_lived_token"];
            string appId = Request.QueryString["app_id"];
            if (!string.IsNullOrEmpty(TokenFacebook) && !string.IsNullOrEmpty(appId))
            {
                GravaToken(TokenFacebook, appId);
            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (txtappID.Text == "" && txtappID.Text == null && txtappID.Text == string.Empty)
            {
                clsValidaDados.ClientMessage(this, "Informar código do AppID.");
                return;
            }
            RunAsync().Wait();
        }

        protected async Task RunAsync()
        {
            // Configuração do aplicativo
            if (!ContemApenasNumeros(txtappID.Text))
            {
                clsValidaDados.ClientMessage(this, "Informar código do AppID corretamente.");
                return;
            }

            string Param1 = Request.QueryString["Param1"];
            string Param2 = Request.QueryString["Param2"];
            string Param3 = Request.QueryString["Param3"];
            string Param4 = Request.QueryString["Param4"];

            string appId = txtappID.Text;
            string redirectUri = "https://graphinsight.azurewebsites.net/Home"; //"https://localhost:44374/Home";
            string scope = "read_insights";  // Adicione outras permissões conforme necessário

            // Construção do URL de autorização
            string authUrl = $"https://www.facebook.com/v13.0/dialog/oauth?response_type=token&display=popup&client_id={appId}&redirect_uri={redirectUri}&auth_type=rerequest&scope={scope}";

            string script = $@"
                <script type='text/javascript'>
                    var authPopup = window.open('{authUrl}', 'FacebookAuthPopup', 'width=600,height=400');
        
                    // Aguarda o popup ser fechado
                    var intervalId = setInterval(function() {{
                        if (authPopup.closed) {{
                            clearInterval(intervalId);

                            // Extrai o long_lived_token da URL e redireciona para a página principal
                            var urlParams = new URLSearchParams(authPopup.location.hash.substr(1));
                            var longLivedToken = urlParams.get('long_lived_token');
                            if (longLivedToken) {{
                                window.location.href = '{redirectUri}?long_lived_token=' + longLivedToken + '&app_id={appId}&Param1={Param1}&Param2={Param2}&Param3={Param3}&Param4={Param4}';
                            }}
                        }} else if (authPopup.location.href.includes('{redirectUri}')) {{
                            // Se a URL do popup contiver o redirecionamento desejado, fecha o popup
                            authPopup.close();
                        }}
                    }}, 1000);
                </script>";

            ClientScript.RegisterStartupScript(this.GetType(), "FacebookAuthPopup", script);
        }

        private void GravaToken(string TokenFacebook, string appId)
        {
            try
            {
                string idUsuario = clsCriptografia.Decrypt(Request.QueryString["Param2"], "Eita#$%Nois##", true);
                int CodigoUsuario = Convert.ToInt32(idUsuario);

                int CodigoEmpresa = 1;

                clsAlteraDados.DadosEmpresa(TokenFacebook, appId, CodigoUsuario, CodigoEmpresa);

                clsValidaDados.ClientMessage(this, "Conexão Realizada com sucesso.");
            }
            catch (Exception ex)
            {
                clsValidaDados.ClientMessage(this, "Erro ao realizar conexão. Caso o problema persista, por favor, entre em contato com o suporte.");
                return;
            }

            Response.Redirect("Home.aspx?Param1=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["Param1"]) + "&Param2=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["Param2"]) + "&Param3=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["Param3"]) + "&Param4=" + System.Web.HttpUtility.UrlEncode(Request.QueryString["Param4"]));
        }

        public bool ContemApenasNumeros(string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }
    }
}
