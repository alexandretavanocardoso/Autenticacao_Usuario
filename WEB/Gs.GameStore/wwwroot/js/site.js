$(document).ready(function () {
    carregarMenu();
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
            if (result.result != null) {
                if (result.result.email != null) {
                    carregarMenuLogado(result.result.email);
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

    html += '<ul class="navbar-nav flex-grow-1" style="display: flex; justify-content: flex-end;"> '
    html += '   <li class="nav-item" style="display: flex; margin-right: 8px;"> '
    html += '       <i style="margin-top: 12px;" class="fas fa-user"></i> '
    html += '       <a style="cursor: pointer;" class="nav-link text-dark">Olá ' + usuario + '</a> '
    html += '   </li> '
    html += '   <li class="nav-item" style="display: flex;"> '
    html += '       <i style="margin-top: 12px;" class="fas fa-shopping-cart"></i> '
    html += '       <a style="cursor: pointer;" onclick="onClick_Logout()" class="nav-link text-dark">Sair</a> '
    html += '   </li> '
    html += '</ul> '

    $("#Menu").append(html);
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