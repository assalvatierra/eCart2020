
function AddPaymentDetails_Cashier() {
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

        var change = parseFloat($("#payment-amount").val()) - parseFloat($("#total-amount").text());

        var res = $.post("/Store/CartDetails/AddPayment", data, (result) => {
            console.log(result);
            if (result == 'True') {
                var amount = parseInt($("#payment-amount").val()).toFixed(2);
                var payment = " " +
                    amount.toString("0.00") + "<br>" +
                    $("#payment-status option:selected").text();
                $("#table-payment-" + data.cartDetailId).append(payment);

                ClosePaymentModal();

                //show payment success
                $("#payment-total").text($("#payment-amount").val());
                $("#payment-change").text(change);
                $("#payment-success-msg").show();
                //focus cursor on
                setTimeout(function () { $('#CartId').select() }, 500);
            } else {
                console.log("Invalid input");
                $("#payment-error-msg").show();
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