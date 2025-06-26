var table;

$(document).ready(function () {
    $('.nav-item a').removeClass('active');
    $('#product').addClass('active');

    table = $('#grid').DataTable({
        processing: true,
        serverSide: true,

        ajax: {
            url: '/Product/GetProducts',
            type: 'POST'
        },
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'category' },
            { data: 'price' },
            { data: 'stock' },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    return `
                        <a href="/Products/Details/${row.id}" class="btn btn-sm btn-info">Details</a>
                        <a href="/Products/Edit/${row.id}" class="btn btn-sm btn-warning">Edit</a>
                        <a href="/Products/Delete/${row.id}" class="btn btn-sm btn-danger">Delete</a>
                    `;
                }
            }
        ]
    });

    $('#findButton').on('click', function () {
        let keyword = $('#inputSearch').val();
        table.search(keyword).draw();
    });


});
