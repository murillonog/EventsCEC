$(document).ready(function () {
    $("#btn-hidden-values").on("click", function () {
        hiddenValues();
    });
});

function hiddenValues() {
    if ($("#hidden-values").val() == '0') {
        $(".value-dashboard").removeClass('hide');
        $(".value-dashboard-anonymous").addClass('hide');
        $("#hidden-values").val('1');
    } else {
        $(".value-dashboard").addClass('hide');
        $(".value-dashboard-anonymous").removeClass('hide');
        $("#hidden-values").val('0');
    }
}