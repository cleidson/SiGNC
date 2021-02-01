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
            RiscoOportunidade: "",
            DataImplatacao: "",
            Responsavel: { Id: "", Nome: "" },
            TipoAcaoId: ""
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
        if (ValidaCampos() == false)
            return;

        conformidade.NumeroConformidade = $("#numero-nao-conformidade").val();
        conformidade.StatusConformidadeId = "1";
        conformidade.DataEmissao = $("#data-emissao").val();
        conformidade.OrigemConformidadeId = $('#selectOrigem').val();
        conformidade.Reincidente = $('#selectReincidente').val();
        conformidade.Requisito = $('#selectRequisito').selectpicker('val');

        //Ação Corretiva
        conformidade.AcaoCorretiva.Descricao = $("#acao-imediata-descricao").val();
        conformidade.AcaoCorretiva.DataImplantacao = $("#data-acao-imediata").val();
        conformidade.AcaoCorretiva.RiscoOportunidade = $("#acao-imediata-riscoOportunidade").val();
        conformidade.AcaoCorretiva.TipoAcaoId = $('#selectTipoAcao').selectpicker('val');


        conformidade.Detalhamentos = getValuesTableConformidadePendentes();
        conformidade.CausaRaizes = getValuesTableCausaRaiz();

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

    function ValidaCampos() {
        var validaEminente = false;
        var validaAcao = false;
        var validaResponsavel = false;
        var validaDataAcaoImediata = false;

        if ($("#emitente").val() == "") {
            $("#emitente").addClass('is-invalid');
            validaEminente = false;
        } else {
            $("#emitente").removeClass('is-invalid');
            $("#emitente").addClass('is-valid');
            validaEminente = true;
        }

        if ($("#acao-imediata-descricao").val() == "") {
            $("#acao-imediata-descricao").addClass('is-invalid');
            validaAcao = false;
        } else {
            $("#acao-imediata-descricao").removeClass('is-invalid');
            $("#acao-imediata-descricao").addClass('is-valid');
            validaAcao = true;
        }


        if ($("#responsavel-acao-imediata").val() == "") {
            $("#responsavel-acao-imediata").removeClass('is-valid');
            $("#responsavel-acao-imediata").addClass('is-invalid');
            validaResponsavel = false;
        } else {

            $("#responsavel-acao-imediata").removeClass('is-invalid');
            $("#aresponsavel-acao-imediata").addClass('is-valid');
            validaResponsavel = true;
        }

        if ($("#data-acao-imediata").val() == "") {
            $("#data-acao-imediata").addClass('is-invalid');
            validaDataAcaoImediata = false;
        } else {

            $("#data-acao-imediata").removeClass('is-valid');
            $("#data-acao-imediata").addClass('is-valid');
            validaDataAcaoImediata = true;
        }

        if (validaEminente && validaAcao && validaResponsavel && validaDataAcaoImediata)
            return true;
        else
            return false;
    }

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
        GetStatus();
        GetOrigem();
        GetTipoAcao();
        GetCausasRaizes();
        GetConformidades();

        $("#data-emissao").val(dataAtualFormatada);
        $("#btnDeletarRow").hide();
        setTableEmpty();
        $("#data-acao-imediata").mask("99/99/9999");
    };

    $("#responsavel-acao-imediata").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'usuario/search', type: "POST", dataType: "json",
                data: { term: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Nome, value: item.Nome, id: item.Id };
                    }));
                },
                select: function (event, ui) {
                    $("input[type=hidden]").val(ui.item.id);
                }
            });
        },
    });

    $('#responsavel-acao-imediata').on('autocompleteselect', function (e, ui) {
        var responsavelAcao = {
            Id: '',
            Nome: ''
        };
        responsavelAcao.Id = ui.item.id;
        responsavelAcao.Nome = ui.item.value;
        conformidade.AcaoCorretiva.Responsavel = responsavelAcao;


        $("#responsavel-acao-imediata").removeClass('is-invalid');
        $("#responsavel-acao-imediata").addClass('is-valid');
    });

    $("#emitente").hover(
        function () {
            if (!$("#emitente").val() == "") {
                $("#emitente").removeClass('is-invalid');
                $("#emitente").addClass('is-valid');
            } else {

                $("#emitente").removeClass('is-valid');
                $("#emitente").addClass('is-invalid');
            }
        }
    );

    $("#emitente").on("change",
        function () {
            if (!$("#emitente").val() == "") {
                $("#emitente").removeClass('is-invalid');
                $("#emitente").addClass('is-valid');
            } else {

                $("#emitente").removeClass('is-valid');
                $("#emitente").addClass('is-invalid');
            }
        }
    );

    $("#acao-imediata-descricao").hover(
        function () {
            if (!$("#acao-imediata-descricao").val() == "") {
                $("#acao-imediata-descricao").removeClass('is-invalid');
                $("#acao-imediata-descricao").addClass('is-valid');
            } else {

                $("#acao-imediata-descricao").removeClass('is-valid');
                $("#acao-imediata-descricao").addClass('is-invalid');
            }
        }
    );

    $("#acao-imediata-descricao").on("change",
        function () {
            if (!$("#acao-imediata-descricao").val() == "") {
                $("#acao-imediata-descricao").removeClass('is-invalid');
                $("#acao-imediata-descricao").addClass('is-valid');
            } else {

                $("#acao-imediata-descricao").removeClass('is-valid');
                $("#acao-imediata-descricao").addClass('is-invalid');
            }
        }
    );

    $("#responsavel-acao-imediata").hover(
        function () {
            if (!$("#responsavel-acao-imediata").val() == "") {
                $("#responsavel-acao-imediata").removeClass('is-invalid');
                $("#aresponsavel-acao-imediata").addClass('is-valid');
            } else {

                $("#responsavel-acao-imediata").removeClass('is-valid');
                $("#responsavel-acao-imediata").addClass('is-invalid');
            }
        }
    );

    $("#responsavel-acao-imediata").on("change",
        function () {
            if (!$("#responsavel-acao-imediata").val() == "") {
                $("#responsavel-acao-imediata").removeClass('is-invalid');
                $("#aresponsavel-acao-imediata").addClass('is-valid');
            } else {

                $("#responsavel-acao-imediata").removeClass('is-valid');
                $("#responsavel-acao-imediata").addClass('is-invalid');
            }
        }
    );

    $("#data-acao-imediata").hover(
        function () {
            if (!$("#data-acao-imediata").val() == "") {
                $("#data-acao-imediata").removeClass('is-invalid');
                $("#data-acao-imediata").addClass('is-valid');
            } else {

                $("#data-acao-imediata").removeClass('is-valid');
                $("#data-acao-imediata").addClass('is-invalid');
            }
        }
    );

    $("#responsavel-acao-imediata").on("change",
        function () {
            if (!$("#data-acao-imediata").val() == "") {
                $("#data-acao-imediata").removeClass('is-invalid');
                $("#data-acao-imediata").addClass('is-valid');
            } else {

                $("#data-acao-imediata").removeClass('is-valid');
                $("#data-acao-imediata").addClass('is-invalid');
            }
        }
    );

    $("#emitente").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'usuario/search', type: "POST", dataType: "json",
                data: { term: request.term },

                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Nome, value: item.Nome, id: item.Id };
                    }));
                },
                select: function (event, ui) {
                    $("input[type=hidden]").val(ui.item.id);
                }
            });
        },
    });

    $('#emitente').on('autocompleteselect', function (e, ui) {

        var eminenteSelect = {
            Id: '',
            Nome: ''
        };

        eminenteSelect.Id = ui.item.id;
        eminenteSelect.Nome = ui.item.value;

        conformidade.Eminente = eminenteSelect;

        $("#emitente").removeClass('is-invalid');
        $("#emitente").addClass('is-valid');
    });

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

    function GetStatus() {
        $.ajax({
            type: "GET",
            url: "/status/list",
            success: (data) => {
                $.each(data, function (key, value) {
                    $("#selectStatus").append('<option value=' + value.Id + '>' + value.Nome + '</option>');
                    $("#selectStatus").selectpicker('refresh');
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

    function GetConformidades() {
        $.ajax({
            type: "GET",
            url: "conformidade/list/1",
            success: (data) => {
                $.each(data, function (key, item) {

                    var $tr = $('<tr data-id="' + item.Id + '">').append(
                        $('<td>').text(item.Id),
                        $('<td>').text(item.NumeroConformidade),
                        $('<td>').text(item.DataEmissao),
                        $('<td>').text(item.UsuarioEmitente),
                        $('<td data-id="' + item.IdStatusConformidade + '" style="text-align:center" >').text(item.DescricaoStatusConformidade)
                    ).appendTo('#table-conformidades-pendentes');
                });



                if ($("#table-conformidades-pendentes tbody").children('tr').length == 0) {
                    setTableEmpty("#table-conformidades-pendentes");
                } else {
                    $("#tr-blank").remove();
                }
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
    function getValuesTableCausaRaiz() {
        var table = $("#table-cr");
        var list = [];
        table.find('tr').each(function (i, el) {
            var $tds = $(this).find('td');
            if ($tds.eq(0).text() != "") {
                list.push({
                    CausaRaizConformidadeId: $tds.eq(0).text(),
                    Ocorreu: $tds.eq(2).find("select").val(),
                    Quais: $tds.eq(3).find("input").val()
                });
            }
        });
        return list;
    };

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

    function setTableEmpty(table) {
        var markup = "<tr id='tr-blank'> <td colspan='3' class='text-center'>Não há detalhes de não conformidade</td></tr>";

        if (table == "#table-conformidades-pendentes") { 
            var markupPendente = "<tr id='tr-blank'> <td colspan='6' class='text-center'>Não há detalhes de não conformidade</td></tr>";
            $("#table-conformidades-pendentes tbody").append(markupPendente);
        }
        else
            $("#table-detalhamento tbody").append(markup);
    }


    Init();
})(jQuery)