function mostrarSenha(inputId, iconId) {
    var inputPass = document.getElementById(inputId);
    var iconLock = document.getElementById(iconId);

    if (inputPass.type === 'password') {
        inputPass.setAttribute('type', 'text');
        iconLock.className = 'bx bxs-lock-open-alt';
    } else {
        inputPass.setAttribute('type', 'password');
        iconLock.className = 'bx bxs-lock-alt';
    }
}


function redirecionarParaCadastro() {
    window.location.href = 'Home.aspx?P=' + '1';
}
document.getElementById('cadastrar').addEventListener('click', redirecionarParaCadastro);

function validaDadosAcesso() {
    var email = document.getElementById('email').value;
    var password = document.getElementById('password').value;
    if (email.trim() !== "" && password.trim() !== "") {
        window.location.href = 'Login.aspx';
    }
}
document.getElementById('submit').addEventListener('click', validaDadosAcesso);