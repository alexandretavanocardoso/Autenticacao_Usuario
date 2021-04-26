$(document).ready(function () {
    carregarMenu();

    $("#dropdown").click(function () {
        $("#dropUsuario").slideToggle("fast");
    });
});

function carregarMenu() {
    var urlMenu = "https://localhost:44389/api/Authorization/recuperarListaUsuario?email=" + localStorage.getItem("email");

    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        cache: false,
        url: urlMenu,
        data: JSON.stringify({}),
        success: function (result) {
            if (result != null) {
                if (result.email != null) {
                    carregarMenuLogado(result.email);
                }
                tata.success('Sucesso', 'Logado com Sucesso!', {
                    duration: 5000,
                    position: "tl",
                    animate: 'slide'
                });
            } else {
                carregarMenuDesLogado();
            }
        }
    });
}

function carregarMenuLogado(usuario) {
    var html = "";

    html += '<a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-bs-toggle="dropdown" aria-expanded="false">';
    html += '   Olá  ' + usuario;
    html += '</a>';
    html +='<ul class="dropdown-menu" aria-labelledby="dropdownMenuLink" id="dropUsuario">'
    html += '   <li><a class="dropdown-item" href="#" style="cursor: pointer;" onclick="onClick_DeletarConta(' + usuario+')">Deletar Conta</a></li>'
    html +='    <li><a class="dropdown-item" href="#" style="cursor: pointer;" onclick="onClick_Logout()">Sair</a></li>'
    html += '</ul>'

    $("#dropdown").append(html);
    $("#dropdown").css("display", "inline");
}

function carregarMenuDesLogado() {
    var html = "";

    html += '<ul class="navbar-nav flex-grow-1" style="display: flex; justify-content: flex-end;"> '
    html += '   <li class="nav-item" style="display: flex; margin-right: 8px;"> '
    html += '       <i style="margin-top: 12px;" class="fas fa-user"></i> '
    html += '       <a style="cursor: pointer;" class="nav-link text-dark" href="https://localhost:44342/Authentication/Login">Login</a> '
    html += '   </li> '
    html += '   <li class="nav-item" style="display: flex;"> '
    html += '       <i style="margin-top: 12px;" class="fas fa-shopping-cart"></i> '
    html += '       <a style="cursor: pointer;" class="nav-link text-dark" href="https://localhost:44342/Authentication/Register">Registrar</a> '
    html += '   </li> '
    html += '</ul> '

    $("#Menu").append(html);
}

function onClick_Logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("email");
    carregarMenu();
    location.href = "https://localhost:44342/Authentication/Login";
    tata.success('Sucesso', 'Logout realizado com sucesso!', {
        duration: 5000,
        position: "tl",
        animate: 'slide'
    });
}

function onClick_DeletarConta(email) {
    $.ajax({
        type: "DELETE",
        url: "https://localhost:44389/api/Authorization/deletarContaUsuario?email=" + localStorage.getItem("email"),
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify({}),
        success: function () {
            localStorage.removeItem("token");
            localStorage.removeItem("email");
            carregarMenu();
            location.href = "https://localhost:44342/Authentication/Login";
            tata.success('Sucesso', 'Conta deletada com sucesso!', {
                duration: 5000,
                position: "tl",
                animate: 'slide'
            });
        }
    });
}