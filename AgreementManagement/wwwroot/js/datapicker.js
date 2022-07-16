var dataTable;


$(document).ready(function () {
    console.log("sdfdsf");
});

$('.datepicker').datepicker({
    format: 'mm/dd/yyyy',
    changemonth: true,
    changeyear: true,
    startDate: '-3d'
}).on('changeDate', function (ev) {
    if ($('#datepicker').valid()) {
        $('#datepicker').removeClass('invalid').addClass('success');
    }
});;

$('.datepicker').on('focus', function (e)
{
    e.preventDefault();
    $(this).attr("autocomplete", "off");
});