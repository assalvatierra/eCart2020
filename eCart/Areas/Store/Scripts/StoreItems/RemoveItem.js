

function SetRemoveItem(Id) {
    $.get('/Store/StoreItems/GetStoreItem', { id: Id }, (result) => {
        console.log(result);

        $('#removeItem-Id').text(result['Id']);
        $('#removeItem-name').text(result['ItemName']);
        $('#removeItem-price').text(result['UnitPrice']);
        $('#removeItem-img').attr("src", result['ImageUrl']);
    });
}

function RemoveItem() {
    var itemId = $('#removeItem-Id').text();
    $.post('/Store/StoreItems/RemoveStoreItem', { id: itemId }, (result) => {
        if (result == 'True') {
            window.location.reload();
        } else {
            alert('Unable to remove item. Please try again later.');
        }
    });
}

function RemoveItemCategory(e, categoryId) {
    $.post("/Store/StoreItems/RemoveItemCategory", { Id: categoryId }).then(() => {
        $(e).parent().remove(); //remove item from table
    }).catch((err) => {
        console.log(err);
    });
}


