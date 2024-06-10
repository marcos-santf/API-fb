<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DashAPI.Login" %>

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
			<h1>Login</h1>
			<div class="input-box">
				<input type="email" id="email" runat="server" placeholder="E-mail" required>
				<i class="bx bxs-user"></i>
			</div>
			<div class="input-box">
				<input type="password" id="password" runat="server" placeholder="Senha"  required>
				<i class="bx bxs-lock-alt" runat="server" id="iconlock"></i>
			</div>
			<div class="remember-forgot">
				<label><input id="btnsenha" onclick="mostrarSenha('password', 'iconlock')" runat="server" type="checkbox"> Mostrar Senha</label>
				<a href="RecuperarSenha.aspx">Esqueceu a senha?</a>
			</div>
			<button type="submit" id="submit" class="btn">Entrar</button>
			<%--<div class="register-link">
				<p>Não tem uma conta? <a id="cadastrar" href="#">Registre-se</a></p>
			</div>--%>
		</form>
	</div>
    <script  src="js/scriptLogin.js"></script>
</body>
</html>

