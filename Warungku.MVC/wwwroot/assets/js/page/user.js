var table;


$(document).ready(function () {
    $('.nav-item a').removeClass('active');
    $('#user').addClass('active');

    table = $('#grid').DataTable({
        processing: true,
        serverSide: true,
        deferRender: true,
        ajax: {
            url: '/UserManagement/GetUsers',
            type: 'POST'
        },
        columns: [
            { data: 'id', defaultContent: ''  },
            { data: 'userName', defaultContent: '' },
            { data: 'email' },
            {
                data: 'role', className: 'text-center',
                render: function (data, type, row) {
                    if (data === "Staff") {
                        return '<span class="badge badge-warning">' + data + '</span>';
                    } else if (data === "Admin") {
                        return '<span class="badge badge-primary">' + data + '</span>';
                    } else if (data === "Manager") {
                        return '<span class="badge badge-success">' + data + '</span>';
                    } else {
                        return '<span class="badge badge-secondary">' + data + '</span>';
                    }
                } },
            {
                data: 'status', className: 'text-center',
                render: function (data, type, row) {
                    if (data === "Active") {
                        return '<span class="badge badge-success">' + data + '</span>';
                    } else if (data === "Inactive") {
                        return '<span class="badge badge-danger">' + data + '</span>';
                    } else {
                        return '<span class="badge badge-secondary">' + data + '</span>';
                    }
                }
},
            { data: 'lastLogin', defaultContent: '' },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    return `
                        <a href="/UserManagement/Details/${row.id}" class="btn btn-sm btn-info">Details</a>
                        <a href="/UserManagement/Edit/${row.id}" class="btn btn-sm btn-warning">Edit</a>
                        <a href="/UserManagement/Delete/${row.id}" class="btn btn-sm btn-danger">Delete</a>
                    `;
                }
            }
        ]
    });
});
