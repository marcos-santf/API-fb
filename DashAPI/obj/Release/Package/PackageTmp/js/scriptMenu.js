// Verifica se há um item ativo no localStorage
var ativo = localStorage.getItem('menu-ativo');

// Se houver um item ativo, defina-o como ativo
if (ativo) {
    var elementoAtivo = document.getElementById(ativo);
    if (elementoAtivo) {
        elementoAtivo.classList.add('ativo');
    }
}

// Seleciona os itens de menu
var menuItem = document.querySelectorAll('.item-menu a');

function selectLink() {
    menuItem.forEach((item) => {
        item.classList.remove('ativo');
    });
    this.classList.add('ativo');

    // Armazena o ID do item ativo no localStorage
    localStorage.setItem('menu-ativo', this.id);
}

menuItem.forEach((item) => {
    item.addEventListener('click', selectLink);
});

// Expandir o menu

var btnExp = document.querySelector('#btn-exp');
var menuSide = document.querySelector('.menu-lateral');

btnExp.addEventListener('click', function () {
    menuSide.classList.toggle('expandir');
});

function redirecionarParaHome() {
    var urlParams = new URLSearchParams(window.location.search);
    var Param1 = encodeURIComponent(urlParams.get("Param1"));
    var Param2 = encodeURIComponent(urlParams.get("Param2"));
    var Param3 = encodeURIComponent(urlParams.get("Param3"));
    var Param4 = encodeURIComponent(urlParams.get("Param4"));

    var novoURL = 'Default.aspx?Param1=' + Param1 + '&Param2=' + Param2 + '&Param3=' + Param3 + '&Param4=' + Param4 + '&Param5=home';

    window.location.href = novoURL;
}
document.getElementById('home').addEventListener('click', redirecionarParaHome);

function redirecionarParaExame() {
    var urlParams = new URLSearchParams(window.location.search);
    var Param1 = encodeURIComponent(urlParams.get("Param1"));
    var Param2 = encodeURIComponent(urlParams.get("Param2"));
    var Param3 = encodeURIComponent(urlParams.get("Param3"));
    var Param4 = encodeURIComponent(urlParams.get("Param4"));

    var novoURL = 'padraoExames.aspx?Param1=' + Param1 + '&Param2=' + Param2 + '&Param3=' + Param3 + '&Param4=' + Param4 + '&Param5=exame';

    window.location.href = novoURL;
}
document.getElementById('exame').addEventListener('click', redirecionarParaExame);

function redirecionarParaAgenda() {
    var urlParams = new URLSearchParams(window.location.search);
    var Param1 = encodeURIComponent(urlParams.get("Param1"));
    var Param2 = encodeURIComponent(urlParams.get("Param2"));
    var Param3 = encodeURIComponent(urlParams.get("Param3"));
    var Param4 = encodeURIComponent(urlParams.get("Param4"));

    var novoURL = 'padraoAgenda.aspx?Param1=' + Param1 + '&Param2=' + Param2 + '&Param3=' + Param3 + '&Param4=' + Param4 + '&Param5=agenda';

    window.location.href = novoURL;
}
document.getElementById('agenda').addEventListener('click', redirecionarParaAgenda);

function redirecionarParaConta() {
    var urlParams = new URLSearchParams(window.location.search);
    var Param1 = encodeURIComponent(urlParams.get("Param1"));
    var Param2 = encodeURIComponent(urlParams.get("Param2"));
    var Param3 = encodeURIComponent(urlParams.get("Param3"));
    var Param4 = encodeURIComponent(urlParams.get("Param4"));

    var novoURL = 'Default.aspx?Param1=' + Param1 + '&Param2=' + Param2 + '&Param3=' + Param3 + '&Param4=' + Param4 + '&Param5=conta';

    window.location.href = novoURL;
}
document.getElementById('conta').addEventListener('click', redirecionarParaConta);


function Sair() {
    localStorage.clear();

    window.location.href = 'padraoLogin.aspx';
}
document.getElementById('sair').addEventListener('click', Sair);
