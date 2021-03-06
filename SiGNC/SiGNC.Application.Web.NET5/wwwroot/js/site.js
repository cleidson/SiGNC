﻿(function () {
    let user = {
        Email: "",
        Senha: ""
    };

    $("#ErroLogin").hide();
    $("#btn-login-entrar").click(function (event) {
        if (validaCampos() == false)
            return;
         
        user.Email = $("#inputEmail").val();
        user.Senha = $("#inputPassword").val()

        $.ajax({
            type: "POST",
            url: "page/login",
            dataType: "json",
            data: user,
            success: (data) => {
                if (data != null) {
                    window.location.href = 'Home';
                } else {  
                    $("#ErroLogin").show();
                    $("#inputEmail").removeClass('is-valid');
                    $("#inputPassword").removeClass('is-valid'); 
                }
            },
            error: (data) => { 
                console.table(data);
                $("#ErroLogin").show();
                $("#inputEmail").removeClass('is-valid');
                $("#inputPassword").removeClass('is-valid'); 
            }
        });

        event.preventDefault();
    });
    function validaCampos() {
        var resultEmail = true;
        var resultSenha = true;
        
        if ($("#inputEmail").val() == "") {
            $("#inputEmail").addClass('is-invalid');
            resultEmail = false;
        } else {
            $("#inputEmail").removeClass('is-invalid');
            $("#inputEmail").addClass('is-valid');
            resultEmail = true;
        }


        if ($("#inputPassword").val() == "") {
            $("#inputPassword").addClass('is-invalid');
            resultSenha = false;
        } else {
            $("#inputPassword").removeClass('is-invalid');
            $("#inputPassword").addClass('is-valid');
            resultSenha = true;
        }

        if (resultEmail && resultSenha) {
            return true
        } else {
            return false;
        }
    }
})(jQuery)