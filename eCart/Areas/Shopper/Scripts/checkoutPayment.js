/*
 *  Checkout Cart Payment
 */
function getPaymentReceivers(cartId) {
    //ready payment list for update
    $("#Receiver-list").children().remove();

    //update payment list with the cartId
    $.get("/Shopper/CartDetails/GetPaymentRecievers", null, (result) => {
        console.log(result);
        if (result.length > 0) {
            //add payment list
            for (var i = 0; i < result.length; i++) {
                var button = "<button class='list-group-item' value='" + result[i]["Id"] + "' style='display:flex;' " +
                    "onclick='selectPayment(this," + result[i]["Id"] + "," + cartId + ")' > " +
                    "<span style='margin:auto;' >" + result[i]["Description"] + "</span></button>";
                $("#Receiver-list").append(button);
            }

            //add default
            var button = "<button class='list-group-item' style='display:flex;' value='0' onclick='selectPayment(this, 0, " + cartId + ")' > " +
                "<span style='margin:auto;'> Cash On Delivery </span></button>";
            $("#Receiver-list").append(button);

        } else {
            alert("Unable to load Payment Options.");
        }
    });
}

function selectPayment(e, receiverId, cartId) {
    $.post("/Shopper/CartDetails/SetPaymentReceiver", { cartId: cartId, receiverId: receiverId }, (result) => {
        if (result == 'True') {
            //udpate display text
            var description = $(e).text();
            $("#paymentMode-" + cartId).text(description);
            $("#paymentModal").modal('hide');
        } else {
            alert('Unable to update payment on cart.');
        }
    })
}