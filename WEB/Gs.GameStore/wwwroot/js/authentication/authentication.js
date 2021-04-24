// #region [ Propriedades ]
var txtEmailLogin = document.getElementById("txtEmailLogin");
var txtSenhaLogin = document.getElementById("txtSenhaLogin");

var txtEmailRegistro = document.getElementById("txtEmailRegistro");
var txtSenhaRegistro = document.getElementById("txtSenhaRegistro");
var txtConfirmaSenha = document.getElementById("txtConfirmarSenha");


$(document).ready(function (event) { });
// #endregion [ Propriedades ]


// #region [ Eventos ]
function onClick_Registro() {
    var registro = new Array();
    registro = {
        "Email": txtEmailRegistro.value,
        "Senha": txtSenhaRegistro.value,
        "ConfirmarSenha": txtConfirmaSenha.value
    };

    if (txtSenhaRegistro.value != txtConfirmaSenha.value) {
        tata.error('Erro', 'Senhas não conferem!', {
            duration: 5000,
            position: "tl",
            animate: 'slide'
        });
        return;
    }

    if (registro.Email == "" || registro.Senha == "" || registro.ConfirmarSenha == "") {
        tata.error('Erro', 'Preencha todos os campos!', {
            duration: 5000,
            position: "tl",
            animate: 'slide'
        });
        return;
    }

    $.ajax({
        cache: false,
        type: "POST",
        url: "https://localhost:44389/api/Authorization/createAuthorization",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(registro),
        success: function (response) {
            location.href = "https://localhost:44342/Authentication/Login";
            tata.success('Sucesso', 'Registrado com Sucesso!', {
                duration: 5000,
                position: "tl",
                animate: 'slide'
            });
        },
        error: function (err) {
            tata.error('Erro', err.responseText, {
                duration: 5000,
                position: "tl",
                animate: 'slide'
            });
        }
    });
}

function onClick_Login() {
    var login = new Array();
    login = {
        "Email": txtEmailLogin.value,
        "Senha": txtSenhaLogin.value
    };

    if (login.Email == "" || login.Senha == "") {
        tata.error('Erro', 'Preencha todos os campos!', {
            duration: 5000,
            position: "tl",
            animate: 'slide'
        });
        return;
    }
    
    $.ajax({
        cache: false,
        type: "POST",
        url: "https://localhost:44389/api/Authorization/loginAuthorization",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(login),
        success: function (response) {
            localStorage.setItem("token", response.accessToken);
            localStorage.setItem("email", response.userToken.email);
            location.href = "https://localhost:44342/";
        },
        error: function (err) {
            tata.error('Erro', err.responseText, {
                duration: 5000,
                position: "tl",
                animate: 'slide'
            });
        }
    });
}
// #endregion [ Eventos ]


// #region [ Metodos ]
// #endregion [ Metodos ]