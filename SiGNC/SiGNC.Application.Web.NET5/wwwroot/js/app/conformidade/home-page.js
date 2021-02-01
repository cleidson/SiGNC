(function () {

    function Init() {
        GetConformidades();
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
    Init();
})(jQuery)