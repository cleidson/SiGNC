/* globals Chart:false, feather:false */

(function () {
    'use strict'

    feather.replace()
    carregaChart();
    var dias = [];
    var total = [];
    // Graphs
    var ctx = document.getElementById('myChart') 

    function carregaChart() {
         
            $.ajax({
                type: "GET",
                url: "chart/count",
                success: (data) => {

                    console.log(data);
                    $.each(data, function (key, value) {
                        dias.push(value.Dia)
                        total.push(value.Total)

                    });

                    var myChart = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: dias,
                            datasets: [{
                                data: total,
                                lineTension: 0,
                                backgroundColor: 'transparent',
                                borderColor: '#007bff',
                                borderWidth: 4,
                                pointBackgroundColor: '#007bff'
                            }]
                        },
                        options: {
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: false
                                    }
                                }]
                            },
                            legend: {
                                display: false
                            }
                        }
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

    
})()
