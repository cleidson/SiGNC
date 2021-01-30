(function () {

    let conformidade = {
        Eminente: { Id: "", Nome: "" },
        NumeroConformidade: "",
        StatusConformidadeId: "",
        DataEmissao: "",
        OrigemConformidadeId: "",
        Reincidente: "",
        Requisito: "",
        Detalhamentos: [
            {
                Id: "",
                Descricao: "",
                Detalhamento: "",
            }
        ],
        AcaoCorretiva: {
            Descricao: "",
            DataImplatacao: "",
            Responsavel: { Id: "", Nome: "" }
        },
        CausaRaizes: [
            {
                Id: "",
                Ocorreu: "",
                Quais: ""
            }
        ]
    };
    $(".nav-link").on("click", function () {
        $("nav-link").find(".active").removeClass("active");
        $(this).addClass("active");
    });
    $("#btn-salvar-conformidade").click(function (event) {

        var x = getValuesTableCausaRaiz();


        conformidade.Eminente.Nome = $("#emitente").val();
        conformidade.NumeroConformidade = $("#numero-nao-conformidade").val();
        conformidade.StatusConformidadeId = "1";
        conformidade.DataEmissao = $("#data-emissao").val();
        conformidade.OrigemConformidadeId = $('#selectOrigem').val();
        conformidade.Reincidente = $('#selectReincidente').val();
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

        $.ajax({
            type: "POST",
            url: "salvar",
            dataType: "json",
            data: conformidade,
            success: (data) => {
                if (data.StatusCode == 200) {
                    let timerInterval;
                    Swal.fire({
                        title: 'Bom trabalho!',
                        html: 'Conformidade cadastrada com sucesso!',
                        icon: 'success',
                        timer: 6000,
                        willClose: () => {
                            clearInterval(timerInterval)
                            document.location.reload(true);
                        },
                        showCloseButton: true,
                        focusConfirm: false
                    })
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


    function Init() {

        GetOrigem();
        GetTipoAcao();
        GetCausasRaizes();

        $("#data-emissao").val(dataAtualFormatada);
        $("#btnDeletarRow").hide();
        setTableEmpty();
        $("#data-acao-imediata").mask("99/99/9999");
    };



    $("#responsavel-acao-imediata").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'usuario/search', type: "POST", dataType: "json",
                //original code
                //data: { searchText: request.id, maxResults: 10 },
                //updated code; updated to request.term 
                //and removed the maxResults since you are not using it on the server side
                data: { term: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        //original code
                        //return { label: item.FullName, value: item.FullName, id: item.TagId }; 
                        //updated code
                        return { label: item.Nome, value: item.Nome, id: item.Id };
                    }));
                },
                select: function (event, ui) {
                    //update the jQuery selector here to your target hidden field
                    $("input[type=hidden]").val(ui.item.id);
                }
            });
        },
    });

    $('#responsavel-acao-imediata').on('autocompleteselect', function (e, ui) {
        console.log(ui.item.id)
        console.log(ui.item.value)
    });



    $("#emitente").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'usuario/search', type: "POST", dataType: "json",
                //original code
                //data: { searchText: request.id, maxResults: 10 },
                //updated code; updated to request.term 
                //and removed the maxResults since you are not using it on the server side
                data: { term: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        //original code
                        //return { label: item.FullName, value: item.FullName, id: item.TagId }; 
                        //updated code
                        return { label: item.Nome, value: item.Nome, id: item.Id };
                    }));
                },
                select: function (event, ui) {
                    //update the jQuery selector here to your target hidden field
                    $("input[type=hidden]").val(ui.item.id);
                }
            });
        },
    });

    $('#emitente').on('autocompleteselect', function (e, ui) {
        console.log(ui.item.id)
        console.log(ui.item.value)
    });

    //$("#responsavel-acao-imediata").autoComplete({
    //    resolver: 'ajax',
    //    bootstrapVersion: 'auto',
    //    noResultsText: 'Usuário não encontrado',
    //    valueKey: 'Nome', 
    //    events: {
    //        search: function (qry, callback) {
    //            // let's do a custom ajax call
    //            $.ajax(
    //                'usuario/search' 
    //            ).done(function (res) { 
    //                callback(res)
    //            });
    //        }
    //    }
    //});

    //$('#responsavel-acao-imediata').on('autocomplete.select', function (evt, item) {

    //    console.log(item);

    //});




    function GetTipoAcao() {
        $.ajax({
            type: "GET",
            url: "acao/tipo/list",
            success: (data) => {
                $.each(data, function (key, value) {
                    $("#selectTipoAcao").append('<option value=' + value.Id + '>' + value.Nome + '</option>');
                    $("#selectTipoAcao").selectpicker('refresh');
                });
                console.table(data);
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
    function GetOrigem() {
        $.ajax({
            type: "GET",
            url: "origem/list",
            success: (data) => {
                $.each(data, function (key, value) {
                    $("#selectOrigem").append('<option value=' + value.Id + '>' + value.Nome + '</option>');
                    $("#selectOrigem").selectpicker('refresh');
                });
                console.table(data);
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


    function GetCausasRaizes() {
        $.ajax({
            type: "GET",
            url: "causaraiz/list",
            success: (data) => {
                $.each(data, function (key, item) {

                    var $tr = $('<tr data-id="' + item.Id + '">').append(
                        $('<td>').text(item.Id),
                        $('<td>').html('<input type="text" class="form-control" id="causa-raiz-descritivo-' + item.Id + '" disabled placeholder="' + item.Descricao + '" />'),
                        $('<td>').html('<select class=" form-control"  title="Ocorreu?"  id="causa-raiz-ocorreu-' + item.Id + '"><option>Sim</option><option>Não</option></select>'),
                        $('<td>').html(' <input type="text" class="form-control" id="causa-raiz-quais' + item.Id + '">'),
                    ).appendTo('#table-cr');
                    //$("#selectTipoAcao").append('<option value=' + value.Id + '>' + value.Nome + '</option>');
                    //$("#selectTipoAcao").selectpicker('refresh');
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

    //$("#table-cr").click(function () {
    //    var data1 = $(this).find("td:eq(0) input[type='text']").val();
    //    var data2 = $(this).find("td:eq(1) input[type='text']").val();

    //    console.log(data1);
    //});
    //var tbl = $('#table-detalhamento tr:has(td)').map(function (i, v) {
    //    var $td = $('td', this);
    //    return {
    //        id: ++i,
    //        descricao: $td.eq(1).text(),
    //        detalhamento: $td.eq(2).text()
    //    }
    //}).get();

    //return tbl;
    function getValuesTableCausaRaiz() {
        var table = $("#table-cr");
        var list = [];

        table.find('tr').each(function (i, el) {
            var $tds = $(this).find('td');

            if ($tds.eq(0).text() != "") {

                var Id = $tds.eq(0).text();
                var Ocorreu = $tds.eq(2).find("select").val();
                var Quais = $tds.eq(3).find("input").val();

                list.push({
                    Id: $tds.eq(0).text(),
                    Ocorreu: $tds.eq(2).find("select").val(),
                    Quais:$tds.eq(3).find("input").val()
                });
                //console.log(
                //    '\nId: ' + Id
                //    + '\nOcorreu: ' + Ocorreu
                //    + '\n Quais:' + Quais);
            } 
        }); 

        return list;
    };


    //function GetCausasRaizes() {
    //    $.ajax({
    //        type: "GET",
    //        url: "causaraiz/list",
    //        success: (data) => {

    //            console.log(data);
    //            var count = 0;
    //            var item_per_row = 3;
    //            var partyHTML = "";
    //            $.each(data, function (partyIdx, party) {


    //                ///*optional stuff to do after success */
    //                //if (count === 0) { // Start of a row
    //                //    partyHTML += '<div class = "form-row">';
    //                //}
    //                partyHTML += '<div class = "form-row">';
    //                partyHTML += '<div class = "form-group col-md-7">';
    //                partyHTML += '<label for="causa-raiz-descritivo">Descrição breve</label>';
    //                partyHTML += '<input type="text" class="form-control" id="causa-raiz-descritivo-' + party.Id + '" disabled placeholder="' + party.Descricao + '" />';
    //                partyHTML += '</div>';

    //                partyHTML += '<div class="form-group col-md-1">';
    //                partyHTML += '<label for="causa-raiz-ocorreu-' + party.Id + '">Ocorreu?</label>';
    //                partyHTML += '<select class=" form-control" data-live-search="true" title="Ocorreu?" id="causa-raiz-ocorreu-' + party.Id + '">';
    //                partyHTML += '<option>Sim</option>';
    //                partyHTML += '<option>Não</option>';
    //                partyHTML += '</select>';
    //                partyHTML += '</div>'; 
    //                partyHTML += ' <div class="form-group col-md-4">';
    //                partyHTML += ' <label for="causa-raiz-quais">Descreva quais</label>';
    //                partyHTML += ' <input type="text" class="form-control" id="causa-raiz-quais' + party.Id + '">'; 
    //                partyHTML += '</div>';
    //                partyHTML += '</div>';
    //                ++count;
    //                //if (count === item_per_row) {  
    //                //    partyHTML += '</div>';
    //                //}

    //            });
    //            //if (count > 0) {  
    //            //    partyHTML += '</div>';
    //            //}

    //            $('#form-causa-raiz').html(partyHTML);


    //            getValuesCauzaRaiz();
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