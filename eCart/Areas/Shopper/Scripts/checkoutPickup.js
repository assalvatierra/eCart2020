/*
 *  Cart Checkout Pickup Location
 */

function getPickupPoints(cartId, StoreId) {
    $.getJSON("/Shopper/CartDetails/GetStorePickups", { storeId: StoreId }, (result) => {
        //clear store pickup list
        $("#pickupLocList").children().remove();

        //add store pickup list
        if (result.length > 0) {
            for (var i = 0; i < result.length; i++) {
                var location = "<li type='button' class='list-group-item btn btn-default' value='" + result[i]['Id'] + "'" +
                    " onclick='UpdateLocation(" + cartId + "," + result[i]['Id'] + ")' >" + result[i]['Address'] + "</li>";
                $("#pickupLocList").append(location);
            }
        } else {
            console.log("Unable to retrieve store pickup locations.");
        }

        //store delivery option
        var Delivery = "<li  type='button' class='list-group-item btn btn-default' value='0' onclick='updateAsDelivery(" + cartId + ")' > Delivery </li>";
        $("#pickupLocList").append(Delivery);
    });
}

function UpdateLocation(cartId, pickupPointId) {
    $.post('/Shopper/CartDetails/UpdatePickupPoint', { cartId: cartId, pickupPointId: pickupPointId },
        (response) => {
            if (response == 'True') {
                updateLocationText(cartId, pickupPointId);
            } else {
                alert('Unable to Update Cart Pickup Address.');
            }
        }
    );
}

function updateLocationText(cartId, pickupPointId) {
    if ((cartId != 0 || cartId != null) && (pickupPointId != 0 || pickupPointId != null)) {
        $.getJSON('/Shopper/CartDetails/GetStorePickupAddress', { id: pickupPointId }, (address) => {
            $("#pickupAddress-" + cartId).val(pickupPointId);
            $("#pickupAddress-" + cartId).text(address);
            $("#pickupModal").modal('hide');
        });
    } else {
        alert('Unable to update cart pickup point.');
    }
}

function updateAsDelivery(cartId) {
    console.log("Delivery");
    $.post('/Shopper/CartDetails/UpdateCartAsDelivery', { cartId: cartId },
        (response) => {
            if (response == 'True') {
                $("#pickupAddress-" + cartId).val(0);
                $("#pickupAddress-" + cartId).text("Delivery");
                $("#pickupModal").modal('hide');
            } else {
                alert('Unable to Update Cart Pickup Address.');
            }
        }
    );
}
