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

    function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async function demo() {
        console.log('Taking a break...');
        await sleep(7000);
        console.log('Two seconds later, showing sleep in a loop...');

     
        // Sleep in loop
        for (let i = 0; i < 5; i++) {
            if (i === 3)
                await sleep(2000);
            console.log(i);
        }
    }



    function Init() {
        GetTipoAcao();
        GetOrigem(); 
        GetStatus();
        loadData();
        $("#form-detalha-conformidade :input").attr("disabled", "disabled");
        $("#form-detalha-conformidade :button").attr("hidden", "hidden");
        $("#data-emissao").val(dataAtualFormatada);
        $("#btnDeletarRow").hide();
        $("#data-acao-imediata").mask("99/99/9999");
    };


    function loadData() {
        var idRequest = $("#ConformidadeHiddenId").val();

        $.ajax({
            url: '/detalhe',
            type: "POST",
            dataType: "json",
            data: { requestId: idRequest },
            success: function (data) { 

                $("#emitente").val(data.UsuarioSolicitante.Nome);


                $("#numero-nao-conformidade").val(data.NumeroConformidade);
                $("#data-emissao").val(data.DataEmissao);
                $('#selectOrigem').val(data.OrigemConformidade.Id);
                $('#selectStatus').val(data.StatusConformidade.Id);
                $('#selectReincidente').val(data.Reincidente.trim());
                $('#selectRequisito').val(data.Requisito.trim());


                $('#selectStatus').selectpicker('refresh');
                $("#selectOrigem").selectpicker('refresh');
                $("#selectReincidente").selectpicker('refresh');
                $("#selectRequisito").selectpicker('refresh'); 

                $.each(data.Detalhamentos, function (key, item) {

                    var $tr = $('<tr data-id="' + item.Id + '">').append(
                        $('<td>').text(item.Id),
                        $('<td>').text(item.Descricao),
                        $('<td>').text(item.Detalhamento)
                    ).appendTo("#table-detalhamento");
                });

                $.each(data.CausaRaizes, function (key, item) {

                    var $tr = $('<tr data-id="' + item.Id + '">').append(
                        $('<td>').text(item.CausaRaizId),
                        $('<td>').html('<input type="text" class="form-control" id="causa-raiz-descritivo-' + item.CausaRaizId + '" disabled placeholder="' + item.CausaRaizDescricao + '" />'),
                        $('<td>').html('<select class=" form-control"  title="Ocorreu?" disabled  id="causa-raiz-ocorreu-' + item.Id + '"><option value="Sim">Sim</option><option value="Não">Não</option></select>'),
                        $('<td>').html(' <input type="text" class="form-control" id="causa-raiz-quais' + item.CausaRaizId + '" disabled placeholder='+ item.Quais + ' >'),
                    ).appendTo('#table-cr'); 
                     
                    $("#causa-raiz-ocorreu-" + item.Id).val(item.OcorreuFormated);
                    
                });

                //Ação Corretiva
                $("#acao-imediata-descricao").val(data.AcaoCorretiva.Descricao);
                $("#data-acao-imediata").val(data.AcaoCorretiva.DataImplantacao);
                $("#acao-imediata-riscoOportunidade").val(data.AcaoCorretiva.RiscoOportunidade);
                $('#selectTipoAcao').val(data.AcaoCorretiva.TipoAcaoId); 
                $('#selectTipoAcao').selectpicker('refresh');

                $("#responsavel-acao-imediata").val(data.UsuarioGestor.Nome);


                console.log(data);
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
    function GetTipoAcao() {
        $.ajax({
            type: "GET",
            url: "/acao/tipo/list",
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

    function GetOrigem() {
        $.ajax({
            type: "GET",
            url: "/origem/list",
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

    function dataAtualFormatada() {
        var data = new Date(),
            dia = data.getDate().toString(),
            diaF = (dia.length == 1) ? '0' + dia : dia,
            mes = (data.getMonth() + 1).toString(), //+1 pois no getMonth Janeiro começa com zero.
            mesF = (mes.length == 1) ? '0' + mes : mes,
            anoF = data.getFullYear();
        return diaF + "/" + mesF + "/" + anoF;
    }

    Init();
})(jQuery)