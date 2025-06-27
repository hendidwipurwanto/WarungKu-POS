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
<button class="btn btn-sm btn-warning btn-edit" data-id="${row.id}">Edit</button>
                        <a href="/Category/Delete/${row.id}" class="btn btn-sm btn-danger">Delete</a>
                    `;
                }
            }
        ]
    })

        $('#grid').on('click', '.btn-edit', function () {
            let id = $(this).data('id');
            $('#cashPaymentModal').modal('show');
            $('#hiddenUserId').val(id);
        });

        $('#findButton').on('click', function () {
            let keyword = $('#inputSearch').val();
            table.search(keyword).draw();
        });
});

//---------------------------------------- Start pop up modal create  section
$(function () {

    $('#addBtn').on('click', function () {
        $.get('/Category/Create', function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-placeholder').html(data);
            // Tampilkan modal
            $('#categoryModal').modal('show');
            $('#cancelBtn').on('click', function () {
                $('#categoryModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#categoryModal').modal('hide');
                location.reload();
            });
        });
    });
    // due to I used event delegation using jquery, I disabled the default submit form behavior
    $(document).on('submit', '#categoryForm', function (e) {
        e.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: "POST",
                url: form.attr('action'),
                data: form.serialize(),
                success: function (response) {
                    if (response.success) {                       
                        $('#categoryModal').modal('hide');
                        alert(response.message);
                        location.reload();
                    } else {
                        $('#modal-placeholder').html(response);
                        $('#categoryModal').modal('show');
                        $('#cancelBtn').on('click', function () {
                            $('#categoryModal').modal('hide');
                            location.reload();
                        });
                        $('#xClose').on('click', function () {
                            $('#categoryModal').modal('hide');
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
//---------------------------------------- End pop up modal create section