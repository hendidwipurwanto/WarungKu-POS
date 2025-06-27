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

//---------------------------------------- Start pop up modal create  section
$(function () {

    $('#addBtn').on('click', function () {
        $.get('/UserManagement/Create', function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-placeholder').html(data);
            // Tampilkan modal
            $('#userModal').modal('show');
            $('#cancelBtn').on('click', function () {
                $('#userModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#userModal').modal('hide');
                location.reload();
            });
        });
    });

    // due to I used event delegation using jquery, I disabled the default submit form behavior
    $(document).on('submit', '#userForm', function (e) {
        e.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: "POST",
                url: form.attr('action'),
                data: form.serialize(),
                success: function (response) {
                    if (response.success) {
                        //if succeed close the modal and reload the page
                        $('#userModal').modal('hide');
                        alert(response.message);
                        location.reload();
                    } else {// if not  display the error
                        console.log('error submit---------------');
                        console.log(response);
                        $('#modal-placeholder').html(response);
                        $('#userModal').modal('show');
                        $('#cancelBtn').on('click', function () {
                            $('#userModal').modal('hide');
                            location.reload();
                        });
                        $('#xClose').on('click', function () {
                            $('#userModal').modal('hide');
                            location.reload();
                        });
                    }
                },
                error: function () {
                    alert("There is some issue when trying to send data");
                }
            });
        }
    });
    //clean modal dialog from DOM after modal dialog closed
    $(document).on('hidden.bs.modal', function (e) {
        $(e.target).remove();
    });
});
//----------