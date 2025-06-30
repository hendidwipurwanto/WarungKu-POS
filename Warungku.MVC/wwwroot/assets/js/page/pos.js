var table;
//var totalAmount = 0;
$(document).ready(function ()
{
    $('#cashPaymentModalBtn').on('click', function () {
        $.get('/PointOfSales/CreateTransaction', function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-add-placeholder').html(data);

            $('#addModal').modal('show');
            $('#totalModal').val(parseInt($('#totalText').text()));
            $('#discountModal').val($('#discount').val());
            $('#voucherModal').val($('#voucher').val());
            $('#grandTotalModal').val(parseInt($('#grandTotalText').text()));
            
            $('#cancelBtn').on('click', function () {
                $('#addModal').modal('hide');
               // location.reload();
            });
            $('#xClose').on('click', function () {
                $('#addModal').modal('hide');
                location.reload();
            });
        });
    });





    //--------------------------------- start jquery data table

    table = $('#grid').DataTable();

    $('#addBtn').on('click', function () {
        var itemId = $('#ProductId').val(); // ambil ID
        var itemName = $('#ProductId option:selected').text(); // ambil nama item
        var qty = parseInt($('#Quantity').val());
        var price = parseInt($('#Price').val());
        var subtotal = qty * price;

        // Tambahkan baris ke DataTable
        table.row.add([
            itemName, // kita tampilkan nama item
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
            var data = this.data();         // ambil array data di row
            var subtotal = parseInt(data[3]); // kolom ke-4 (index ke-3 = Subtotal)
            totalAmount += subtotal;
        });

        $('#totalText').text(totalAmount);

        let grandTotal = totalAmount + parseInt($('#discountText').text())

        $('#grandTotalText').text(grandTotal);

        //-------------------------------------- End grand total-----------------------

        
        // $('#totalAmount').text(gt);
    });

    // Delete handler
    $('#grid tbody').on('click', '.btn-delete', function () {
       // table.row($(this).parents('tr')).remove().draw();
        var row = table.row($(this).closest('tr'));
        var data = row.data();
        let subtotal = parseInt(data[3]);
        let totalText = parseInt($('#totalText').text()) - subtotal;
        $('#totalText').text(totalText);
        let grandTotal = parseInt($('#totalText').text()) + parseInt($('#discountText').text()) + parseInt($('#voucherText').text());
        $('#grandTotalText').text(grandTotal);
        //parseInt($('#granTotalText').text());
        


        row.remove().draw();
    });

    //-------------------------------------- start Cash Payment Calculator -----------------------
    $("#cash").keyup(function () {
        let grandTotal = $('#grandTotalCash').val();
        let cash = $('#cash').val() === "" ? "0" : $('#cash').val();
        let change = parseInt(cash) - parseInt(grandTotal);
        $('#changeCash').val(change);
    });
    //-------------------------------------- End Cash Payment Calculator -----------------------

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


    //---------------------------------------- Start open modal cash Payment--------------------
   /* $('#cashPaymentModalBtn').on('click', function () {
        $('#cashPaymentModal').modal('show');
        let gt = $('#grandTotalText').text();
        $('#totalText').text(gt);
        $('#grandTotalCash').val(parseInt(gt));

    }); */













});


    


