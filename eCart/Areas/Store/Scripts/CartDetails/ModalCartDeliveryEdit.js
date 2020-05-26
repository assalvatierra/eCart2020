
/**
 *  ModalCartDeliveryEdit 
 */ 

$('#Edit-delivery-date').daterangepicker({
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



function EditDeliveryRider() {
    var data = {
        id: $("#Edit-delivery-id").val(),
        date: $("#Edit-delivery-date").val(),
        address: $("#Edit-delivery-address").val(),
        riderId: parseInt($("#Edit-delivery-riderId option:selected").val()),
        cartDetailId: parseInt('@ViewBag.CartId'),
        remarks: $("#Edit-delivery-remarks").val()
    }

    $.post("/Store/CartDetails/UpdateDeliveryRider/", data, (res) => {
        console.log(res);
        window.location.reload();
        //UpdateDeliveryTable(data);
    });

}

function UpdateDeliveryTable(data) {
    var delivery = "<tr>" +
        "<td>" + $("#Edit-delivery-riderId option:selected").text() + "</td>" +
        "<td>" + data.date.toString("M/d/yyyy h:mm:ss tt") + "</td>" +
        "<td>" + data.address + "</td>" +
        "<td>" + data.remarks + "</td>" +
        "</tr>"

    $("#table-delivery").append(delivery);
}