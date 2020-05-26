/*
 *  CartStatus.js
 */
function UpdateCartStatusBtn(statusId) {
    //set status
    switch (statusId) {
        case '3':
            //processing
            $("#status-processing").addClass("btn-primary");
            $("#status-processing").siblings().removeClass("btn-primary");
            break;
        case '4':
            //ready
            $("#status-ready").addClass("btn-primary");
            $("#status-ready").siblings().removeClass("btn-primary");
            break;
        case '5':
            //delivered
            $("#status-delivered").addClass("btn-primary");
            $("#status-delivered").siblings().removeClass("btn-primary");
            break;
        case '6':
            //cancel
            $("#status-cancel").addClass("btn-primary");
            $("#status-cancel").siblings().removeClass("btn-primary");
            break;
        default:
            break;
    }

}

function SetCartStatus(cartId, statusId, status) {
    $.get("/Store/CartDetails/SetCartStatus", { id: cartId, statusId, statusId }, (result) => {
        console.log(result);
        if (result) { //if res = true
            $("#cart-status-text").text(status);
            var statusID = statusId.toString(); // convert to string
            UpdateCartStatusBtn(statusID);
        }
    });
}