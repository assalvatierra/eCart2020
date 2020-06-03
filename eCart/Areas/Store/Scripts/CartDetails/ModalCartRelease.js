/* ModalPayment.js */

$('#release-date').daterangepicker({
    singleDatePicker: true,
    startDate: moment(),
    showDropdowns: true,
    timePicker: true,
    timePicker24Hour: false,
    timePickerIncrement: 10,
    autoUpdateInput: true,
    locale: {
        format: 'MMM DD,YYYY h:mm A'
    },
});


function AddCartRelease() {

    if (validateForm()) {
        var data = {
            date: $("#release-date").val(),
            releaseBy: $("#release-by").val(),
            userId: $("#release-userId").val(),
            cartDetailId: $("#release-cartDetailId").val(),
            userDetailId: parseInt($("#UserDetails").val()),
            storePickupPointId: parseInt($("#StorePickupPoints").val())
        }

        var res = $.post("/Store/CartDetails/AddCartRelease", data, (result) => {
            console.log(result);
            if (result != '0') {
                window.location.href = "/Store/CartDetails/CartRelease/" + result;
            }
        });

        console.log(res);
    } else {
        console.log("Invalid input");
        alert("Unable to Submit Payment. Please fill all fields.");
    }
}

function validateForm() {

    if ($("#release-by").val() == null || $("#release-by").val() == " ") {
        console.log("invalid amount");
        return false;
    }

    if ($("#release-userId").val() == null || $("#release-userId").val() == " " || $("#release-userId").val() == "") {
        console.log("invalid receiverInfo");
        return false;
    }
    if ($("#release-cartDetailId").val() == null || $("#release-cartDetailId").val() == " " || $("#release-cartDetailId").val() == "") {
        console.log("invalid partyInfo");
        return false;
    }

    if ($("#release-date").val() == null || $("#release-date").val() == " ") {
        console.log("invalid date");
        return false;
    }

    return true;

}