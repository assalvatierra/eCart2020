/*
 *  Cart Checkout View
 */

function RemoveItem(e, Id, subtotal) {
    var data = {
        id: Id
    }

    $.post('/Shopper/CartDetails/RemoveCartItem', data, (response) => {
        console.log(response);
        UpdatePrice(subtotal);
    });

    $(e).parent().parent().remove();
}

function UpdatePrice(removedsubtotal) {
    var totalPrice = parseFloat($('#Total-Price').text());
    totalPrice -= removedsubtotal;
    $('#Total-Price').text(totalPrice.toLocaleString('en-US', { minimumFractionDigits: 2 }));
}

function SubmitOrder(e, cartId) {
    var order = {
        customer : $("#customer-name").text(),
        kioskId : $("#kioskId").text(),
        id : cartId
    };
    console.log("Submitting Order...");
    var res = $.post("/Shopper/Kiosk/SubmitOrder", order, (result) => {
        console.log(result);
        if (result == "True") {
            //on success, disable button and show success modal
            $(e).attr("disabled", true);
            $("#CheckOutSuccessModal").modal('show');
        } else {
            //on error : order not submitted, error on saving on db
            $("#cart-alert-box-" + cartId).children('p').text("We cannot process you cart. Please try again later.");
            $("#cart-alert-box-" + cartId).show();
        }
    });

    console.log(res);
}

function DisableAllButton() {
    var hasActive = '@Model.Any(s => s.CartStatusId == 1)';
    if (hasActive) {
        $(".btn-primary").attr("disabled", "disabled");
    }
}
