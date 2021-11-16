function aplicaFiltroConsultaAvancada() {
    var vPlaca = document.getElementById('Placa').value;
    var vCarreta = document.getElementById('Carreta').value;

    $.ajax({
        url: "/caminhao/ObtemDadosConsultaAvancada",
        data: { Placa: vPlaca, Carreta: vCarreta },
        success: function (dados) {
            if (dados.erro != undefined) {
                swal({
                    title: "Erro",
                    text: dados.msg,
                    icon: "error",
                    button: "OK",
                });
            }
            else {
                document.getElementById('resultadoConsulta').innerHTML = dados;
            }
        },
    });
}

function aplicaFiltroConsultaAvancadaCelula() {
    var vEquipamentoID = document.getElementById('EquipamentoID').value;
    var vPeso = document.getElementById('Peso').value;
    var vPosicao = document.getElementById('Posicao').value;

    $.ajax({
        url: "/celula/ObtemDadosConsultaAvancadaCelula",
        data: { EquipamentoID: vEquipamentoID, Peso: vPeso, Posicao: vPosicao },
        success: function (dados) {
            if (dados.erro != undefined) {
                swal({
                    title: "Erro",
                    text: dados.msg,
                    icon: "error",
                    button: "OK",
                });
            }
            else {
                document.getElementById('resultadoConsultaCelula').innerHTML = dados;
            }
        },
    });
}

function aplicaFiltroConsultaAvancadaEquipamento() {
    var vCaminhaoID = document.getElementById('CaminhaoID').value;
    var vQtdBalanca = document.getElementById('QtdBalanca').value;

    $.ajax({
        url: "/equipamento/ObtemDadosConsultaAvancadaEquipamento",
        data: { CaminhaoID: vCaminhaoID, QtdBalanca: vQtdBalanca},
        success: function (dados) {
            if (dados.erro != undefined) {
                swal({
                    title: "Erro",
                    text: dados.msg,
                    icon: "error",
                    button: "OK",
                });
            }
            else {
                document.getElementById('resultadoConsultaEquipamento').innerHTML = dados;
            }
        },
    });
}