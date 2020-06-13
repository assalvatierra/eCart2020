/* ModalPayment.js */

function SetPaymentCart(cartId) {
    ResetForm();
    $("#payment-cartId").val(cartId);
}


function SetPaymentCart(cartId, shopperName) {
    ResetForm();
    AutoFillPaymentDetails(shopperName);
    $("#payment-cartId").val(cartId);
}


function SetPaymentCart(cartId, shopperName, amount) {
    ResetForm();
    AutoFillPaymentDetails(shopperName);
    $("#payment-cartId").val(cartId);
    $('#payment-amount').val(amount);
    console.log('show price with amount');
    setTimeout(function () { $('#payment-amount').select() }, 500);
}

function ResetForm() {
    $("#payment-date").val(moment(Date.now()).format("MMM DD,YYYY h:mm A"));
    $("#payment-Party option:selected").val(1);
    $("#payment-receiverInfo").val("");
    $("#payment-Recievers option:selected").val(1);
    $("#payment-partyInfo").val("");
    $("#payment-status").val(2);
    $("#payment-amount").val(0);
}


$('#payment-btn').click(() => {
    console.log("set cart payment");

    if ($('#total-amount').text() != "") {
        setTimeout(function () { $('#payment-amount').select() }, 500);
        var totalAmount = $('#total-amount').text();
        $('#payment-amount').val(totalAmount);
    }
})

$('#payment-amount').keyup(function (e) {
    if (e.keyCode == 13) {
        console.log('submit payment');
        $("#submitPaymentBtn").click();
    }
});

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

    console.log(data);

    var res = $.post("/Store/CartDetails/AddPayment", data, (result) => {
        console.log(result);

        console.log('status :' + result.status);
        if (result['status'] == 500) {
            alert('Unable to submit payment.');
        }

        if (result == 'True') {
            addtoTable(data);
        } else {
            alert('Unable to submit payment.');
        }
    });

    console.log(res);

    console.log('status :' + res.status);
    if (res['status'] === 500) {
        alert('Unable to submit payment.');
    }
}


function AddPaymentDetails() {
    $('#payment-error-warning').hide();
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

        console.log(data);

        var res = $.post("/Store/CartDetails/AddPayment", data, (result) => {
            console.log(result);
            if (result == 'True') {
                var amount = parseInt($("#payment-amount").val()).toFixed(2);
                var payment = " " +
                    amount.toString("0.00") + "<br>" +
                    $("#payment-status option:selected").text() ;
                $("#table-payment-" + data.cartDetailId).append(payment);

                //show payment success

                ClosePaymentModal();
                addtoTable(data);
            } else {
                console.log("Invalid input");
                alert("Unable to Submit Payment.");
            }
        });
        console.log(res);

        console.log('status :' + res.status);
        if (res['status'] === 500) {
            alert('Unable to submit payment.');
        }
    } else {
        console.log("Invalid input");
       // alert("Unable to Submit Payment. Please fill all fields.");
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
    location.reload();
}

function validateForm() {

    if ($("#payment-amount").val() == null || $("#payment-amount").val() == " " || isNaN($("#payment-amount").val()) ) {
        console.log("Invalid amount");
        $('#payment-error-warning').show();
        $('#payment-error-warning').text("Invalid amount");
        return false;
    }

    if ($("#payment-receiverInfo").val() == null || $("#payment-receiverInfo").val() == " " || $("#payment-receiverInfo").val() == "") {
        console.log("invalid receiverInfo");
        $('#payment-error-warning').show();
        $('#payment-error-warning').text("Invalid receiver info");
        return false;
    }
    if ($("#payment-partyInfo").val() == null || $("#payment-partyInfo").val() == " " || $("#payment-partyInfo").val() == "") {
        console.log("invalid partyInfo");
        $('#payment-error-warning').show();
        $('#payment-error-warning').text("Invalid partyInfo");
        return false;
    }

    if ($("#payment-date").val() == null || $("#payment-date").val() == " ") {
        console.log("invalid date");
        $('#payment-error-warning').show();
        $('#payment-error-warning').text("Invalid date");
        return false;
    }
    return true;
}

function ClosePaymentModal() {
    $('#payment-error-warning').hide();
    $('#PaymentModal').modal('hide');
}

