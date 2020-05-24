// Add Store Item
// File: /Areas/Store/StoreItems/_ModalAddItem.cshtml

function PrepAddItemModal() {
    //console.log("Preparing Modal");
    $('#addItem-category-group').show();
    $('#addItem-category-list').hide();
    $('#addItem-list').hide();
    $('#addItem-price').hide();

    $('#addItem-category-group').children().remove();

    $.get('/Store/StoreItems/GetItemCategoryGroups', null,
        (result) => {
            //console.log(result);
            for (let i = 0; i < result.length; i++) {
                let category = "<button class='list-group-item' value='" + result[i]['Id'] + "' onclick='AddItemCatGroupSelect(this);'> " + result[i]['Name'] + " </button>";

                $('#addItem-category-group').append(category);
            }

            if (result.length == 0) {
                $('#addItem-category-group').append("<p> No Item Found</p>");
            }
        }
    );
}

function AddItemCatGroupSelect(e) {
    $('#addItem-category-group').hide();
    $('#addItem-category-list').show();

    $('#addItem-category-list').children().remove();

    //set selected as active
    $(e).addClass('active');

    var catgroupId = $('#addItem-category-group').find('.active').val();
    //console.log(catgroupId);
    $.get('/Store/StoreItems/GetItemCategoriesById', { id: catgroupId },
        (result) => {
            //console.log(result);
            for (let i = 0; i < result.length; i++) {
                let category = "<button class='list-group-item' value='" + result[i]['Id'] + "' onclick='AddItemCatSelect(this);'> " + result[i]['Name'] + " </button>";

                $('#addItem-category-list').append(category);
            }

            if (result.length == 0) {
                $('#addItem-category-list').append("<p> No Item Found</p>");
            }
        }
    );
    //console.log(res);
}

function AddItemCatSelect(e) {

    $('#addItem-category-list').hide();
    $('#addItem-list').show();

    //set selected as active
    $(e).addClass('active');

    var categoryId = $('#addItem-category-list').find('.active').val();
    //console.log('category selected' + categoryId);

    $('#addItem-list').children().remove();
    //search for items under category
    $.get('/Store/StoreItems/GetItemMasters', { id: categoryId },
        (result) => {
            //console.log(result);
            for (let i = 0; i < result.length; i++) {
                let item = "<button class='list-group-item' value='" + result[i]['ItemMasterId'] + "' onclick='AddItemSelect(this);'> " + result[i]['Name'] + " </button>";

                $('#addItem-list').append(item);
            }

            if (result.length == 0) {
                $('#addItem-list').append("<p> No Item Found</p>");
            }
        }
    );
}

function AddItemSelect(e) {
    $('#addItem-list').hide();
    $('#addItem-price').show();

    //set selected as active
    $(e).addClass('active');
}


function AddItemSubmit(storeId) {
    var categoryGroupId = $('#addItem-category-group').find('.active').val();
    var categoryId = $('#addItem-category-list').find('.active').val();
    var itemId = $('#addItem-list').find('.active').val();
    var itemPrice = $('#addItem-price-input').val();

    //console.log("categoryGroupId: " + categoryGroupId);
    //console.log("categoryId: " + categoryId);
    //console.log("itemId: " + itemId);
    //console.log("Price: " + itemPrice);

    let item = {
        storeId: storeId,
        itemMasterId: itemId,
        price: itemPrice
    };

    $.post('/Store/StoreItems/AddItemToStore', item, (result) => {
        console.log(result);
        if (result == 'True') {
            // reload table
            window.location.reload();
        } else {
            alert("Unable to add the item. Please try again later.");
        }
    });
}

function GetItemDetails(itemId) {
    console.log("Getting Item details");
    var response = $.get('/Store/StoreItems/GetItemMasterDetails', { id: itemId }, (result) => {
        console.log(result);
    });

    console.log(response);
}