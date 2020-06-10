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
        } else {
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



// Cart Checkout Date
$('input[name="pickup-date"]').daterangepicker({
    singleDatePicker: true,
    startDate: moment().add('hours', 4),
    showDropdowns: true,
    timePicker: true,
    timePicker24Hour: false,
    timePickerIncrement: 1,
    autoUpdateInput: true,
    locale: {
        format: ' h:mm A MMM DD YYYY'
    },
});

function dateChanged(cartId) {
    let paymentDate = $('input[name="pickup-date"]').val();
    let now = moment();

    let dateComparision = moment(paymentDate).diff(now, 'minutes');
    //console.log(dateComparision);
    if (dateComparision > 120) {    //at least 120 minutes or 2 hours from now

        //update date on session
        $.post('/Shopper/CartDetails/SetCartPickupDate', { cartId: cartId, date: paymentDate },
            (result) => {
                //console.log(result);
                if (result != 'True') {
                    $('input[name="pickup-date"]').val(moment().add('hours', 4).format('MMM DD,YYYY h:mm A'));
                    alert('Invalid Date, please input a valid date for the date and time pickup.');
                }
            }
        );
    } else {
        $('input[name="pickup-date"]').val(moment().add('hours', 4).format('MMM DD,YYYY h:mm A'));
        alert('Invalid Date, please input a valid date for the date and time pickup.');
    }
}

function OnModalClose() {
    window.location.reload();
}