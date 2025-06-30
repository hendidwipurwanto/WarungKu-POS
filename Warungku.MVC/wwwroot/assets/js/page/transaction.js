var table;

$(document).ready(function () {
    $('.nav-item a').removeClass('active');
    $('#listTransaction').addClass('active');

    table = $('#grid').DataTable({
        processing: true,
        serverSide: true,
        deferRender: true,

        ajax: {
            url: '/ListTransaction/GetTransactions',
            type: 'POST'
        },
        columns: [
            { data: 'id' },
            { data: 'date', className: 'text-center' },
            { data: 'user' },
            { data: 'total', className: 'text-center' },
            {
                data: 'discount',
                render: function (data, type, row) {
                    return data + '%';
                },
                className: 'text-center'
            },
            { data: 'voucher', className: 'text-center' },
            { data: 'grandTotal', defaultContent: '', className: 'text-center' }
        ]
    });

    $('#findButton').on('click', function () {
        let keyword = $('#inputSearch').val();
        table.search(keyword).draw();
    });

});
