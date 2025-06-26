var table;

$(document).ready(function () {
    $('.nav-item a').removeClass('active');
    $('#pointOfSales').addClass('active');

    table = $('#grid').DataTable({
        processing: true,
        serverSide: true,

        ajax: {
            url: '/PointOfSales/GetPoses',
            type: 'POST'
        },
        columns: [
            { data: 'id', visible: false },
            { data: 'item' },
            { data: 'quantity' },
            { data: 'price' },
            { data: 'subtotal' },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    return `
                        <a href="/Products/Details/${row.id}" class="btn btn-outline-secondary">Delete</a>
                    `;
                }
            }
        ]
    });

});
