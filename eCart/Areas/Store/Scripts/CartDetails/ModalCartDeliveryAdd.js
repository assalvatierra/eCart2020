/**
 *  ModalCartDeliveryAdd.js
 * 
 */

function SetCartDeliveryDate(Date) {
    $('#delivery-date').val(Date.toString("MMM DD,yyyy hh:mm tt"));
}

$('#delivery-date').daterangepicker({
    singleDatePicker: true,
    startDate: moment(),
    showDropdowns: true,
    timePicker: true,
    timePicker24Hour: false,
    timePickerIncrement: 10,
    autoUpdateInput: true,
    locale: {
        format: 'MMM DD,YYYY h:mm A'
    },
});

function AddDeliveryRider(cartId) {
    var data = {
        date: $("#delivery-date").val(),
        address: $("#delivery-address").val(),
        riderId: parseInt($("#delivery-riderId option:selected").val()),
        cartDetailId: parseInt(cartId),
        remarks: $("#delivery-remarks").val()
    }

   $.post("/Store/CartDetails/AddDeliveryRider", data, (res) => {
        if (res == 'True') {
            addtoDeliveryTable(data);
        } else {
            alert("Unable to assign delivery to this cart.");
        }
    });
}

function addtoDeliveryTable(data) {
    var delivery = "<tr>" +
        "<td>" + $("#delivery-riderId option:selected").text() + "</td>" +
        "<td>" + data.date.toString("M/d/yyyy h:mm:ss tt") + "</td>" +
        "<td>" + data.address + "</td>" +
        "<td>" + data.remarks + "</td>" +
        "<td> Pending </td>" +
        "<td> <a href=''> Edit </a> | <a href=''> Delete </a> </td>" +
        "</tr>"

    $("#table-delivery").append(delivery);
}