
function prepCreateModal() {
    $("#CreateItem-ItemMaster").val('');
    $("#CreateItem-UnitPrice").val('');
    $("#CreateItem-ItemImgUrl").val('');
}

function AddItem() {
    var storeId = parseInt($("#CreateItem-StoreId").val());
    var itemName = $("#CreateItem-ItemMaster").val();
    var price = parseFloat($("#CreateItem-UnitPrice").val());
    var imgUrl = $("#CreateItem-ItemImgUrl").val();

    $("#warning").hide();
    $("#warning-price").hide();
    if ((itemName != null && price.length != 0) && (itemName != "" && price != "")) {

        if (!isNaN(price)) {

            var Item = {
                storeId: storeId,
                itemName: itemName,
                price: price,
                imgUrl: imgUrl
            }

            $.post("/Store/StoreItems/AddStoreItem", Item, (response) => {
                console.log(response);
                if (response == 'True') {
                    console.log("OK");

                    AddItemToTable(itemName, price, storeId, imgUrl);

                } else {
                    console.log("error");
                }
            });

            console.log("OK");
        } else {
            $("#warning-price").show();
        }
    } else {
        $("#warning").show();
    }
}


function AddItemToTable(item, price, Id, imgUrl) {

    var item = "<tr>" +
        "<td width='80'><img src='" + imgUrl + "' class='img-thumbnail' width='80' /></td>" +
        "<td style='display:flex;'>  <b style='margin: auto 0;'> " + item + " </td>" +
        "<td><button class='btn btn-default btn-xs' href='' data-target='#AddCategory' data-toggle='modal' onclick='GetCategoryList(" + Id + ")'>Add Category</button> </td>" +
        "<td> " + price + " <b></td>" +
        "<td><a href='/Store/StoreItems/Edit/" + Id + "'> Edit </a> |" +
        "<a href='/Store/StoreItems/Delete/" + Id + "'> Delete </a> " +
        " </td>"
    " </tr>";

    $("tbody").append(item);
    $("#CreateItem").modal('hide');
}