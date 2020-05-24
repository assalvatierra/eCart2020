

$(function () {
    Initialize()
});


function Initialize() {

    $('#dtPayment').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        showDropdowns: true,
        timePicker: true,
        timePicker24Hour: false,
        timePickerIncrement: 10,
        autoUpdateInput: true,
        locale: {
            format: 'MMM DD YYYY h:mm A'
        },
    });

    $('#dtPosted').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        showDropdowns: true,
        timePicker: true,
        timePicker24Hour: false,
        timePickerIncrement: 10,
        autoUpdateInput: true,
        locale: {
            format: 'MMM DD YYYY h:mm A'
        },
    });
}
