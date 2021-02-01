(function () {
    let user = {
        Email: "",
        Senha: ""
    }; 
    $("#btn-login-entrar").click(function (event) {
        if (ValidaCampos() == false)
            return;


        user.Email = $("#inputEmail").val() == "";
        user.Senha = $("#inputPassword").val()

        $.ajax({
            type: "POST",
            url: "page/login",
            dataType: "json",
            data: user,
            success: (data) => {
                if (data != null) {
                    window.location.href = 'conformidade/index';
                } else {
                    $("#ErroLogin").addClass("is-invalid");
                }
            },
            error: (data) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Ocorreu um erro na requisição, tente novamente mais tarde)'
                })
                console.table(data)
            }
        });

        event.preventDefault();
    });
    function validaCampos() {
        if ($("#inputEmail").val() == "") {
            $("#inputEmail").addClass('is-invalid');
        } else {
            $("#inputEmail").removeClass('is-invalid');
            $("#inputEmail").addClass('is-valid');
        }


        if ($("#inputPassword").val() == "") {
            $("#inputPassword").addClass('is-invalid');
            validaEminente = false;
        } else {
            $("#inputPassword").removeClass('is-invalid');
            $("#inputPassword").addClass('is-valid');
        }
    }
})(jQuery)