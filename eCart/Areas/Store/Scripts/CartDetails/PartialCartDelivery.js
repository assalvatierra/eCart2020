/**
 *  PartialCartDelivery.js
 */

function DeleteCartDelivery(e, id) {
    if (confirm('Do you want to remove this Delivery?')) {
        $.post("/Store/CartDetails/DeleteCartDelivery", { id: id });
        $(e).parent().parent().remove();
    }
}

function EditDelivery(id) {
    $("#Edit-delivery-id").val(id);
}

function GetDeliveryHistory(deliveryId) {

    $.get("/Store/CartDetails/GetCartDeliveryActivities/", { id: deliveryId }, (result) => {
        console.log(result);
        PopulateDeliveryHistory(result);
    });
}

function PopulateDeliveryHistory(data) {

    if (data.length > 0) {
        $("#delivery-history-list").children().remove();
    }

    for (var i = 0; i < data.length; i++) {
        let list = "<button class='list-group-item' > " + data[i]["Date"] + " - <b> " + data[i]["Activity"] + "</b> </button>";
        $("#delivery-history-list").append(list)
    }
}