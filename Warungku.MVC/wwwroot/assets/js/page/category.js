var table;

$(document).ready(function () {
    $('.nav-item a').removeClass('active');
    $('#category').addClass('active');
    table = $('#grid').DataTable({
        processing: true,
        serverSide: true,

        ajax: {
            url: '/Category/GetCategories',
            type: 'POST'
        },
        columns: [
            { data: 'id', visible: false },
            { data: 'name' },
            { data: 'description' },
            {
                data: 'status',
                className: 'text-center',
                render: function (data, type, row) {
                    if (data === "Active") {
                        return '<span class="badge badge-success">Active</span>';
                    } else if (data === "Inactive") {
                        return '<span class="badge badge-danger">Inactive</span>';
                    } else {
                        return '<span class="badge badge-secondary">' + data + '</span>';
                    }
                }
            },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    return `
                        <a href="/Category/Details/${row.id}" class="btn btn-sm btn-info">Details</a>
                        <a href="/Category/Edit/${row.id}" class="btn btn-sm btn-warning">Edit</a>
                        <a href="/Category/Delete/${row.id}" class="btn btn-sm btn-danger">Delete</a>
                    `;
                }
            }
        ]
    })

    $('#findButton').on('click', function () {
        let keyword = $('#inputSearch').val();
        table.search(keyword).draw();
    });


});
