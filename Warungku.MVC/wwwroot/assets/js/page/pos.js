var table;
$(document).ready(function ()
{
    $('#cashPaymentModalBtn').on('click', function () {
        $.get('/PointOfSales/CreateTransaction', function (data) {
            console.log('------------------------------------');
            console.log(data);
            $('#modal-add-placeholder').html(data);

            $('#addModal').modal('show');
            $('#totalModal').val(parseInt($('#totalText').text()));
            $('#discountModal').val($('#discount').val());
            $('#voucherModal').val($('#voucher').val());
            $('#grandTotalModal').val(parseInt($('#grandTotalText').text()));
            $('#grandTotalCashReadOnly').val(parseInt($('#grandTotalText').text()));

            $('#cancelBtn').on('click', function () {
                $('#addModal').modal('hide');
            });
            $('#xClose').on('click', function () {
                $('#addModal').modal('hide');
                location.reload();
            });
            //-------------------------------------- start Cash Payment Calculator -----------------------
            $('#cashPayment').keyup(function () {
                let grandTotal = parseInt($('#grandTotalCashReadOnly').val());
                let cash = parseInt($('#cashPayment').val());
                let change = cash - grandTotal;
                $('#changeCashReadOnly').val(change);
            });
            //-------------------------------------- End Cash Payment Calculator -----------------------
        });
    });

 //--------------------------------- start jquery data table

    table = $('#grid').DataTable();

    $('#addBtn').on('click', function () {
        var itemId = $('#ProductId').val();
        var itemName = $('#ProductId option:selected').text(); // get item name
        var qty = parseInt($('#Quantity').val());
        var price = parseInt($('#Price').val());
        var subtotal = qty * price;

        // add new row into table
        table.row.add([
            itemName, 
            qty,
            price,
            subtotal,
            '<button class="btn-delete btn btn-sm btn-danger">Delete</button>'
        ]).draw(false);

        // Kosongkan input
        $('#quantityInput').val(1);
        $('#priceInput').val(0);
        //-------------------------------------- start grand total-----------------------
        var totalAmount = 0;
        table.rows().every(function () {
            var data = this.data();         
            var subtotal = parseInt(data[3]); 
            totalAmount += subtotal;
        });

        $('#totalText').text(totalAmount);

        let grandTotal = totalAmount + parseInt($('#discountText').text())
        //-------------------------------------- end grand total-----------------------
        $('#grandTotalText').text(grandTotal);

    });

    // Delete handler
    $('#grid tbody').on('click', '.btn-delete', function () {
        var row = table.row($(this).closest('tr'));
        var data = row.data();
        let subtotal = parseInt(data[3]);
        let totalText = parseInt($('#totalText').text()) - subtotal;
        $('#totalText').text(totalText);
        let grandTotal = parseInt($('#totalText').text()) + parseInt($('#discountText').text()) + parseInt($('#voucherText').text());
        $('#grandTotalText').text(grandTotal);
        row.remove().draw();
    });

    $("#discount").keyup(function () {
        let discountAmount = parseInt($('#discount').val()) * parseInt($('#totalText').text()) / 100; 
        $('#discountText').text('-'+discountAmount);
        let voucherAmount = parseInt($('#voucher').val() == "" ? 0 : $('#voucher').val());
        $('#voucherText').text(voucherAmount);

        let grandTotal = parseInt($('#totalText').text()) - discountAmount - voucherAmount;
       
        console.log(grandTotal);
        $('#grandTotalText').text(grandTotal);
    });


    $("#voucher").keyup(function () {
        let discountAmount = parseInt($('#discount').val()) * parseInt($('#totalText').text()) / 100;
        $('#discountText').text(discountAmount);
        let voucherAmount = parseInt($('#voucher').val() == "" ? 0 : $('#voucher').val());
        $('#voucherText').text('-'+voucherAmount);

        let grandTotal = parseInt($('#totalText').text()) - discountAmount - voucherAmount;
        $('#grandTotalText').text(grandTotal);
        console.log(grandTotal);

    });

});


    


