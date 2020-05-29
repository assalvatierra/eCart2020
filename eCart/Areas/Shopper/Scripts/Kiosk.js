

//On Add Button Click
function AddQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int
    if (qty < 100) {
        qty += 1;
    }

    $(e).siblings('.item-qty').text(qty);
}

//On Subtract Button Click
function SubtractQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int

    if (qty > 1) {
        qty -= 1;
    }

    $(e).siblings('.item-qty').text(qty);
}

function AddtoCart_Submit(e, itemId, itemName, price) {
    var qty = $(e).siblings('.item-qty').text();
    var data = {
        id: parseInt(itemId),
        qty: parseInt(qty)
    }

    //calculate total Price of item
    var totalPrice = qty * price;

    //get item image
    var itemImg = $(e).parent().parent().siblings('img').attr('src');

    //add item with qty to cart
    $.post("/kiosk/AddToCart", data, (response) => {
        console.log(response);
        if (response == 'True') {
            //add item to cart summary at footer
            cartItem = "<div class='col-sm-2 cart-item'> " +
                "<img src='" + itemImg + "' width='35' class='col-sm-4 img-thumbnail' style='height:50px;width:50px;'>" +
                "<p class='col-sm-7'> <b> " + itemName + "</b> <br> Qty:" + qty + "  &nbsp;&nbsp;&nbsp; " +
                "<span class='text-success'> Price: " + totalPrice + " </span> </p>" +
                "<div><span class='cart-remove-item' onclick='RemoveItem(this," + itemId + ")'> x </span></div>" +
                "</div>";
            $("#Cart-Summary").append(cartItem);
        } else {
            alert("Item cannot be added to the cart.");
        }
    });


    //Show buttons for adding new item
    $(e).parent().siblings().removeClass('hidden');   //show Add Cart Button
    $(e).parent().addClass('hidden');      //hide add cart button
    
}

function RemoveItem(e, Id) {

    $.post('/kiosk/RemoveCartItem', { id: Id }, (response) => {
        console.log(response);
        if (response == 'True') {  //remove element 
            $(e).parent().parent().remove();
        } else {
            alert("Item cannot be removed from the cart.");
        }
    });

}
