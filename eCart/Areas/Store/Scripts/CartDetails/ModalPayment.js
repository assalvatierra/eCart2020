/* ModalPayment.js */

function SetPaymentCart(cartId) {
    ResetForm();
    $("#payment-cartId").val(cartId);
}

function ResetForm() {
    $("#payment-date").val(moment(Date.now()).format("MMM DD,YYYY h:mm A"));
    $("#payment-Party option:selected").val(1);
    $("#payment-receiverInfo").val("");
    $("#payment-Recievers option:selected").val(1);
    $("#payment-partyInfo").val("");
    $("#payment-status option:selected").val(1);
    $("#payment-amount").val(0);
}

$('#payment-date').daterangepicker({
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


function AddPaymentDetails(cartId) {
    var data = {
        date: $("#payment-date").val(),
        partyId: parseInt($("#payment-Party option:selected").val()),
        receiverInfo: $("#payment-receiverInfo").val(),
        receiverId: parseInt($("#payment-Recievers option:selected").val()),
        partyInfo: $("#payment-partyInfo").val(),
        statusId: parseInt($("#payment-status option:selected").val()),
        amount: parseFloat($("#payment-amount").val()),
        //cartDetailId: parseInt('@ViewBag.CartId
        cartDetailId: parseInt(cartId)
    }

    var res = $.post("/Store/CartDetails/AddPayment", data, (result) => {
        console.log(result);
        if (result == 'True') {
            addtoTable(data);
        }
    });

    console.log(res);
}


function AddPaymentDetails() {
    if (validateForm()) {
        var data = {
            date: $("#payment-date").val(),
            partyId: parseInt($("#payment-Party option:selected").val()),
            receiverInfo: $("#payment-receiverInfo").val(),
            receiverId: parseInt($("#payment-Recievers option:selected").val()),
            partyInfo: $("#payment-partyInfo").val(),
            statusId: parseInt($("#payment-status option:selected").val()),
            amount: parseFloat($("#payment-amount").val()),
            cartDetailId: $("#payment-cartId").val(),
        }

        var res = $.post("/Store/CartDetails/AddPayment", data, (result) => {
            console.log(result);
            if (result == 'True') {
                var amount = parseInt($("#payment-amount").val()).toFixed(2);
                var payment = " " +
                    amount.toString("0.00") + "<br>" +
                    $("#payment-status option:selected").text() ;
                $("#table-payment-" + data.cartDetailId).append(payment);
            }
        });

        console.log(res);
    } else {
        console.log("Invalid input");
        alert("Unable to Submit Payment. Please fill all fields.");
    }
}

function addtoTable(data) {
    var payment = "<tr>" +
        "<td>" + data.date.toString("M/d/yyyy h:mm:ss tt") + "</td>" +
        "<td>" + $("#payment-Party option:selected").text() + "</td>" +
        "<td>" + data.partyInfo + "</td>" +
        "<td>" + $("#payment-Recievers option:selected").text() + "</td>" +
        "<td>" + data.receiverInfo + "</td>" +
        "<td>" + $("#payment-status option:selected").text() + "</td>" +
        "<td>" + $("#payment-amount").val().toString("0.00") + "</td>" +
        "</tr>"
    $("#table-payments").append(payment);
}

function validateForm() {

    if ($("#payment-amount").val() == null || $("#payment-amount").val() == " ") {
        console.log("invalid amount");
        return false;
    }

    if ($("#payment-receiverInfo").val() == null || $("#payment-receiverInfo").val() == " " || $("#payment-receiverInfo").val() == "") {
        console.log("invalid receiverInfo");
        return false;
    }
    if ($("#payment-partyInfo").val() == null || $("#payment-partyInfo").val() == " " || $("#payment-partyInfo").val() == "") {
        console.log("invalid partyInfo");
        return false;
    }

    if ($("#payment-date").val() == null || $("#payment-date").val() == " ") {
        console.log("invalid date");
        return false;
    }

    return true;

}