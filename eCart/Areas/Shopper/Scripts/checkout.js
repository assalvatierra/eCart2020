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
    $.post("/Shopper/CartDetails/SubmitOrder", { id: cartId }, (result) => {
        console.log(result);
        if (result == "True") {
            //on success, disable button and show success modal
            $(e).attr("disabled", true);
            $("#CheckOutSuccessModal").modal('show');
        } else if (result == "Order NOT Submitted") {
            //on error : order not submitted, error on saving on db
            $("#cart-alert-box-" + cartId).children('p').text("We cannot process you cart. Please try again later.");
            $("#cart-alert-box-" + cartId).show();
        }
    })
}

function SubmitAllOrder() {
    $.post("/Shopper/CartDetails/SubmitAllOrder", null, (result) => {
        console.log(result == "True");
        if (result == "True") {
            //all order submitted successfully
            $(".btn-primary").attr("disabled", "disabled");
            $("#CheckOutSuccessModal").modal('show');
        } else {
            //alert("An error has occured while processing your cart.")
            $("#cart-alert-box-all").children('p').text("An error has occured while processing your cart.");
            $("#cart-alert-box-all").show();
        }

    });
}

function DisableAllButton() {
    var hasActive = '@Model.Any(s => s.CartStatusId == 1)';
    if (hasActive) {
        $(".btn-primary").attr("disabled", "disabled");
    }
}
