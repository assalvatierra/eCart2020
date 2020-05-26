/* ModalPayment.js */

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