<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarSenha.aspx.cs" Inherits="DashAPI.RecuperarSenha" %>

<!DOCTYPE html>
<html lang="pt-br">

<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login</title>
    <link rel="stylesheet" href="css/styleLogin.css">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
	<link rel="website icon" type="png" href="img/icon_Dash.png"/>

	<style>
        body {
             background: url('img/imgLogin.jpg') no-repeat;
			 background-size: cover;
        }
    </style>

</head>
<body>
    <div class="wrapper">
		<form  id="userForm" runat="server">
			<%--<h1>Recuperar Senha de Acesso</h1>--%>
			<div class="input-box">
				<input type="email" id="txtEmail" runat="server" placeholder="E-mail Cadastrado" required>
				<i class="bx bxs-user"></i>
			</div>
			<div class="input-box" id="a" runat="server" visible="false">
				<input type="text" id="txtSenha" runat="server" placeholder="Senha" required>
				<i class="bx bxs-lock-alt" runat="server" id="iconlock" onclick="mostrarSenha('txtSenha', 'iconlock')"></i>
			</div>
			<div class="input-box" id="b" runat="server" visible="false">
				<input type="text" id="txtNovaSenha" runat="server" placeholder="Nova Senha" required>
				<i class="bx bxs-lock-alt" runat="server" id="iconlock1" onclick="mostrarSenha('txtNovaSenha', 'iconlock1')"></i>
			</div>
			<div class="input-box" id="c" runat="server" visible="false">
				<input type="text" id="txtConfNovaSenha" runat="server" placeholder="Confirmar Nova Senha" required>
				<i class="bx bxs-lock-alt" runat="server" id="iconlock2" onclick="mostrarSenha('txtConfNovaSenha', 'iconlock2')"></i>
			</div>
			<%--<div class="remember-forgot" id="d" runat="server" visible="false">
				<label><input id="btnsenha" onclick="mostrarSenha()" runat="server" type="checkbox"> Mostrar Senha</label>
			</div>--%>
			<button type="submit" runat="server" id="btnEnviar" class="btn">Enviar</button>
			<%--<asp:Button ID="submitButton" runat="server" Text="Valida Token" OnClick="submitButton_Click" CssClass="action-button" />--%>
			<%--<div class="register-link">
				<p>Não tem uma conta? <a id="cadastrar" href="#">Registre-se</a></p>
			</div>--%>
		</form>
	</div>
    <script  src="js/scriptLogin.js"></script>
</body>
</html>

