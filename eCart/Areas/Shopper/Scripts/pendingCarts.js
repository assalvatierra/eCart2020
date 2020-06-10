

function CancelOrder(e, cartId) {
    $.get("/Shopper/CartDetails/CancelCart", { cartId: cartId }, (response) => {
        console.log(response);
        if (response == "Order Cancelled") {
            $(e).attr("disabled", "disabled");
            $(e).parent().siblings('.col-lg-2').children('.cart-status-text').text("Cancelled");
        } else {
            alert(response);
        }
    });
}