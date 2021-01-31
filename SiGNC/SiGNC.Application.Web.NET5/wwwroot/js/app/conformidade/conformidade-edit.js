(function () {

    let conformidade = {
        Eminente: "",
        NumeroConformidade: "",
        Status: "",
        DataEmissao: "",
        Origem: "",
        Reincidente: "",
        Requisito: "",
        Detalhamentos: [
            {
                Descricao: "",
                Detalhamento: "",
            }
        ],
        AcaoCorretiva: {
            Descricao: "",
            DataImplatacao: "",
            Responsavel: ""
        },
        CausaRaizes: [
            {
                Id: "",
                Ocorreu: "",
                Quais: ""
            }
        ]
    };


    $("#btn-editar-conformidade").click(() => {
        $("#block-cadastro").removeAttr("hidden", "hidden"); 
    });

    $("#btn-salvar-conformidade").click(function (event) {

        conformidade.Eminente = $("#emitente").val();
        conformidade.NumeroConformidade = $("#numero-nao-conformidade").val();
        conformidade.Status = "Novo";
        conformidade.DataEmissao = $("#data-emissao").val();
        conformidade.Origem = $('#selectOrigem').selectpicker('val');
        conformidade.Reincidente = $('#selectReincidente').selectpicker('val');
        conformidade.Requisito = $('#selectRequisito').selectpicker('val');


        conformidade.Detalhamentos = getValuesTableConformidadePendentes();

        conformidade.AcaoCorretiva.Descricao = $("#acao-imediata-descricao").val();
        conformidade.AcaoCorretiva.DataImplatacao = $("#data-acao-imediata").val();
        conformidade.AcaoCorretiva.Responsavel = $("#responsavel-acao-imediata").val();

        console.table(conformidade);
        //conformidade.CausaRaizes.push({
        //    //Id =  $("#causa-raiz-id").val(),
        //    Ocorreu = $("#causa-raiz-descritivo").val(),
        //    Quais=  $("#causa-raiz-quais").val()
        //});
    });

    function getValuesTableConformidadePendentes() {
        var tbl = $('#table-detalhamento tr:has(td)').map(function (i, v) {
            var $td = $('td', this);
            return {
                id: ++i,
                descricao: $td.eq(1).text(),
                detalhamento: $td.eq(2).text()
            }
        }).get();

        return tbl;
    };


    $("#btn-cancelar-conformidade").click(() => {

        $("#block-cadastro").attr("hidden", "hidden"); 

    });

    $("#btn-editar-conformidade").click(function () {
        var $row = $(this).closest("tr");    // Find the row
        var $text = $row.find(".nr").text(); // Find the text

        // Let's test it out
        alert($text);
    });

    $("#btn-editar-conformidade").click(function () {
        var $row = $(this).closest("tr");    // Find the row
        var $text = $row.find(".nr").text(); // Find the text

        // Let's test it out
        alert($text);
    });

    function GetConformidades() {
        $.ajax({
            type: "GET",
            url: "conformidade/list",
            success: (data) => {
                $.each(data, function (key, item) {

                    var $tr = $('<tr data-id="' + item.Id + '">').append(
                        $('<td>').text(item.Id),
                        $('<td>').text(item.NumeroConformidade),
                        $('<td>').text(item.DataEmissao),
                        $('<td>').text(item.UsuarioEmitente),
                        $('<td data-id="' + item.IdStatusConformidade + '" style="text-align:center" >').text(item.DescricaoStatusConformidade),
                        $('<td>').html('<a class="text-muted pointer  text-center" onClick="Edit(' + item.Id + ')" data-toggle="tooltip" data-html="true" title="Editar" id="btn-editar-conformidade"> <span data-feather="edit">Editar</span> </a> <a  class="text-muted pointer margin-left-5px text-center"   onClick="Details('+ item.Id +')"    data-toggle="tooltip" data-html="true" title="Visualizar"><span data-feather="eye">Visualizar</span></a>'),
                    
                         



                    //    < td colspan = "2" > 
                    //    <a class="text-muted pointer" data-toggle="tooltip" data-html="true" title="Editar" id="btn-editar-conformidade">
                    //        <span data-feather="edit"></span>
                    //    </a>
                    //    <a class="text-muted  margin-left-5px" asp-controller="Conformidade" asp-action="DetalhaConformidade" asp-route-id="10" data-toggle="tooltip" data-html="true" title="Visualizar">
                    //        <span data-feather="eye"></span>
                    //    </a>
                    //</td >



                    ).appendTo('#table-conformidades-pendentes');
                });
            },
            error: (data) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Ocorreu um erro na requisição, tente novamente mais tarde)'
                })
            }
        });
    }

     
    function Init() {
       
        GetConformidades();
        $("#block-cadastro").attr("hidden", "hidden"); 


        $("#data-emissao").val(dataAtualFormatada);
        $("#btnDeletarRow").hide();
        setTableEmpty();

        $("#data-acao-imediata").mask("99/99/9999");
    };

    function setTableEmpty() {
        var markup = "<tr id='tr-blank'> <td colspan='3' class='text-center'>Não há detalhes de não conformidade</td></tr>";
        $("#table-detalhamento tbody").append(markup);

    }

    $("#adicionar-detalhamento-naoconformidade").click(function (event) {

        if ($("#descricao-detalhamento-nao-conformidade").val() == "" || $("#detalhamento-nao-conformidade").val() == "") {

            if ($("#descricao-detalhamento-nao-conformidade").val() == "") {

                $("#descricao-detalhamento-nao-conformidade").addClass('is-invalid');
            }

            if ($("#detalhamento-nao-conformidade").val() == "") {

                $("#detalhamento-nao-conformidade").addClass('is-invalid');
            }
        } else {
            $("#tr-blank").remove();
            var descricao = $("#descricao-detalhamento-nao-conformidade").val();
            var detalhamento = $("#detalhamento-nao-conformidade").val();
            var markup = "<tr><td><input type='checkbox' name='record'></td><td>" + descricao + "</td><td>" + detalhamento + "</td></tr>";
            $("#table-detalhamento tbody").append(markup);

            $("#descricao-detalhamento-nao-conformidade").val("");
            $("#detalhamento-nao-conformidade").val("");

            $("#descricao-detalhamento-nao-conformidade").removeClass('is-valid');
            $("#detalhamento-nao-conformidade").removeClass('is-valid');

            $("#btnDeletarRow").show();
        }

    });

    $("#delete-row").click(function () {
        $("#table-detalhamento tbody").find('input[name="record"]').each(function () {
            if ($(this).is(":checked")) {
                $(this).parents("tr").remove();
                if ($("#table-detalhamento tbody").children('tr').length == 0) {
                    setTableEmpty();
                    $("#btnDeletarRow").hide();
                }
            }
        });
    });



    function dataAtualFormatada() {
        var data = new Date(),
            dia = data.getDate().toString(),
            diaF = (dia.length == 1) ? '0' + dia : dia,
            mes = (data.getMonth() + 1).toString(), //+1 pois no getMonth Janeiro começa com zero.
            mesF = (mes.length == 1) ? '0' + mes : mes,
            anoF = data.getFullYear();
        return diaF + "/" + mesF + "/" + anoF;
    }


    //######################################################### REGRA DE INPUT ###############################################
    //## DESCRICAO
    $("input[name=descricao-detalhamento-nao-conformidade]").hover(
        function () {
            if (!$("#descricao-detalhamento-nao-conformidade").val() == "") {
                $("#descricao-detalhamento-nao-conformidade").removeClass('is-invalid');
            }
        }
    );

    $('input[name=descricao-detalhamento-nao-conformidade]').change(function () {
        if ($("#descricao-detalhamento-nao-conformidade").val() == "") {
            $("#descricao-detalhamento-nao-conformidade").addClass('is-invalid');
        } else {
            $("#descricao-detalhamento-nao-conformidade").removeClass('is-invalid');
            $("#descricao-detalhamento-nao-conformidade").addClass('is-valid');
        }
    });

    // ## DETALHA
    $("textarea[name=detalhamento-nao-conformidade]").hover(
        function () {
            if (!$("#detalhamento-nao-conformidade").val() == "") {
                $("#detalhamento-nao-conformidade").removeClass('is-invalid');
            }
        }
    );

    $('textarea[name=detalhamento-nao-conformidade]').change(function () {
        if ($("#detalhamento-nao-conformidade").val() == "") {
            $("#detalhamento-nao-conformidade").addClass('is-invalid');
        } else {
            $("#detalhamento-nao-conformidade").removeClass('is-invalid');
            $("#detalhamento-nao-conformidade").addClass('is-valid');
        }
    });
     
    jQuery.extend(jQuery.validator.messages, {
        required: "O campo é obrigatório",
        remote: "Please fix this field.",
        email: "Please enter a valid email address.",
        url: "Please enter a valid URL.",
        date: "Please enter a valid date.",
        dateISO: "Please enter a valid date (ISO).",
        number: "Please enter a valid number.",
        digits: "Please enter only digits.",
        creditcard: "Please enter a valid credit card number.",
        equalTo: "Please enter the same value again.",
        accept: "Please enter a value with a valid extension.",
        maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
        minlength: jQuery.validator.format("Please enter at least {0} characters."),
        rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
        range: jQuery.validator.format("Please enter a value between {0} and {1}."),
        max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
        min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
    });

    Init();
})(jQuery)

function Edit(Id) {
    console.log(Id);
}

//function Details(Id) {
//    $.ajax({
//        url: 'usuario/search',
//        type: "POST",
//        dataType: "json",
//        data: { id: Id },

//        success: function (data) {
//            console.log(data);
//        },
//        error: (data) => {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: 'Ocorreu um erro na requisição, tente novamente mais tarde)'
//            })
//        } 
//    });
//}
//function Details(Id) {
     
//        $.ajax({
//            url: 'details/'+Id,
//            type: "GET", 
//            success: function (data) {
//                response($.map(data, function (item) {
//                    return { label: item.Nome, value: item.Nome, id: item.Id };
//                }));
//            },
//            select: function (event, ui) {
//                $("input[type=hidden]").val(ui.item.id);
//            }
//        });
 
//}

function Details(Id) {
    window.location.href = 'details/' + Id;
}