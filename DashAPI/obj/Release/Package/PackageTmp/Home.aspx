<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DashAPI.Home" Async="true" %>
<%--<%@ Register Src="Controls/padraoMenu.ascx" TagName="Menu" TagPrefix="uc" %>--%>

<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="css/styleCampoPadrao.css">
    <link rel="stylesheet" href="css/styleHome.css">
    <title>Home</title>
    <link rel="website icon" type="png" href="img/icon_Dash.png"/>
     <%--<uc:Menu runat="server" ID="MenuControl"/>--%>
</head>
<body>
    <form id="userForm" runat="server">
        <%--<header>
            <h1>Dados</h1>
        </header>--%>
        <section id="pnAtendimento" class="section-common-style" runat="server" visible="true">
            <%--<section id="pnTituloSenha" style="text-align: center;">
                 <h2>SENHA</h2>
            </section>--%>
            
            <%--<section id="pnGeraSenha" runat="server" style="text-align: center; margin-top: 20px;">
                <asp:Label ID="txtappID" Text ="" runat="server">appID: </asp:Label>
            </section>--%>
           <%-- <label for="appID">App ID:</label>
            <input type="text" id="txtappID" name="txtappID" required><br><br>--%>
            
            <div class="form-group">
                <label for="txtappID">App ID:</label>
                <asp:TextBox ID="txtappID" runat="server"></asp:TextBox>
            </div>
            <br><br>
         </section>

        <section id="pnPaciente" class="section-common-style" runat="server" visible="true">
            <section id="pnBotao" runat="server" style="text-align: center; margin-top: 20px;">
                    <asp:Button ID="submitButton" runat="server" Text="Valida Token" OnClick="submitButton_Click" CssClass="action-button" />
            </section>
        </section>
    </form>
    <script>
        function clearDefaultText(inputElement) {
            if (inputElement.value === inputElement.defaultValue) {
                inputElement.value = '';
            }
        }

        function restoreDefaultText(inputElement) {
            if (inputElement.value === '') {
                inputElement.value = inputElement.defaultValue;
            }
        }
    </script>
</body>
</html>

