var Dashboard = {

    Init: function () {

        //****************************
        // Theme Onload Toast
        //****************************
        $(window).on("load", function () {
            let myAlert = $('.toast').first();
            if (myAlert.length) {
                let bsAlert = new bootstrap.Toast(myAlert[ 0 ]);
                bsAlert.show();
            }
        });

        // =====================================
        // Breakup
        // =====================================s
        var breakup = {
            series: [ 38, 40, 25, 50 ],
            labels: [ "WhatsApp Enviados", "Correos Enviados", "Citas Agendadas", "Citas Atendidas" ],
            chart: {
                width: 380,
                type: "donut",
                fontFamily: "inherit",
                foreColor: "#adb0bb",
            },
            plotOptions: {
                pie: {
                    startAngle: 0,
                    endAngle: 360,
                    donut: {
                        size: "75%",
                    },
                },
            },
            stroke: {
                show: false,
            },
            dataLabels: {
                enabled: false,
            },
            legend: {
                show: true,
                position: 'top', // Muestra la leyenda en la parte superior
                horizontalAlign: 'center',
                fontSize: '14px',
                markers: {
                    width: 12,
                    height: 12,
                },
            },
            colors: [ "#4e79a7", "#76b7b2", "#ffdd71", "#e15759" ], // Colores personalizados
            responsive: [
                {
                    breakpoint: 991,
                    options: {
                        chart: {
                            width: 240,
                        },
                    },
                },
            ],
            tooltip: {
                theme: "dark",
                fillSeriesColor: false,
            },
        };

        var chart = new ApexCharts(document.querySelector("#breakup"), breakup);
        chart.render();

    },
}


