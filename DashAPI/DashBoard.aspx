<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="DashAPI.DashBoard" %>

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

