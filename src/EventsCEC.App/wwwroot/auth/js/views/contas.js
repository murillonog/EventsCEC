$(document).ready(function () {
    isParcelado($('#Parcelado'));
    $("#CategoriaId").on("change", function () {
        makeSubcategoria(this.value);
    });
    $("#TipoPagamento").on("change", function () {
        isMoney(this.value);
        isPlastic(this.value);
    });
    $('#Parcelado').click(function () {
        isParcelado($(this));
    });
});

function isParcelado(element) {
    if (element.is(':checked')) {
        $(".div-parcelado").removeClass("hide");
    } else {
        $(".div-parcelado").addClass("hide");
    }
}

function isMoney(value) {
    if (value == 5) {
        $("#div-banco").addClass("hide");
    } else {
        $("#div-banco").removeClass("hide");
    }
}

function isPlastic(value) {
    if (value == 1) {
        $("#div-datacompra").removeClass("hide");
    } else {
        $("#div-datacompra").addClass("hide");
    }
}

function makeSubcategoria(value) {
    $.ajax({
        method: "GET",
        url: "/SubCategoria/GetByCategoriaId",
        data: { id: value }
    }).done(function (response) {
        var selectHtml = "<select class='form-control' data-val='true' data-val-required='The SubCategoriaId field is required.' id='SubCategoriaId' name='SubCategoriaId'>";
        selectHtml += "<option value=''> Selecione uma subcategoria </option>";

        response.data.forEach((e) => selectHtml += "<option value='" + e.id + "'>" + e.nome + "</option>");

        selectHtml += "</select>";
        $("#select-subcategoria").html(selectHtml);
        $("#div-subcategoria").removeClass("hide");
    });
}

function changeCategoria(e) {
    makeSubcategoria(e.value)
}