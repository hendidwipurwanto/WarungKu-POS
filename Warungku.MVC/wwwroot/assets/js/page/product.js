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
            { data: 'id', visible: false },
            { data: 'name' },
            { data: 'categoryName' },
            { data: 'price' },
            { data: 'stock' },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: 'text-center',
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-info" data-id="${row.id}">Details</button>
                        <button class="btn btn-sm btn-warning btn-edit" data-id="${row.id}">Edit</button>
                       <button class="btn btn-sm btn-danger" data-id="${row.id}">Delete</button>
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

//---------------------------------------- Start pop up modal create  section
$(function () {

    $('#addBtn').on('click', function () {
        $.get('/Product/Create', function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-add-placeholder').html(data);
            // Tampilkan modal
            $('#addModal').modal('show');
            $('#cancelBtn').on('click', function () {
                $('#addModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#addModal').modal('hide');
                location.reload();
            });
        });
    });

    // due to I used event delegation using jquery, I disabled the default submit form behavior
    $(document).on('submit', '#addForm', function (e) {
        e.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: "POST",
                url: form.attr('action'),
                data: form.serialize(),
                success: function (response) {
                    if (response.success) {
                        $('#addModal').modal('hide');
                        Swal.fire({
                            title: 'Success!',
                            text: response.message,
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then((result) => {
                            if (result.isConfirmed) {

                                location.reload();
                            }
                        });

                    } else {
                        $('#modal-add-placeholder').html(response);
                        $('#addModal').modal('show');
                        $('#cancelBtn').on('click', function () {
                            $('#addModal').modal('hide');
                            location.reload();
                        });
                        $('#xClose').on('click', function () {
                            $('#addModal').modal('hide');
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




//---------------------------------------- Start pop up modal Edit  section
$(function () {

    $('#grid').on('click', '.btn-edit', function () {
        let id = $(this).data('id');
        $.get('/Product/Edit/'+ id, function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-edit-placeholder').html(data);
            // Tampilkan modal
            $('#editModal').modal('show');
            $('#cancelBtn').on('click', function () {
                $('#editModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#editModal').modal('hide');
                location.reload();
            });
        });
    });

    // due to I used event delegation using jquery, I disabled the default submit form behavior
    $(document).on('submit', '#editForm', function (e) {
        e.preventDefault();
        var form = $(this);
        if (form.valid()) {
            $.ajax({
                type: "POST",
                url: form.attr('action'),
                data: form.serialize(),
                success: function (response) {
                    if (response.success) {
                        $('#editModal').modal('hide');
                        Swal.fire({
                            title: 'Success!',
                            text: response.message,
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then((result) => {
                            if (result.isConfirmed) {

                                location.reload();
                            }
                        });
                    } else {
                        $('#modal-edit-placeholder').html(response);
                        $('#editModal').modal('show');
                        $('#cancelBtn').on('click', function () {
                            $('#editModal').modal('hide');
                            location.reload();
                        });
                        $('#xClose').on('click', function () {
                            $('#editModal').modal('hide');
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
//---------------------------------------- End pop up modal Edit  section

//---------------------------------------- Start pop up modal detail  section
$(function () {

    $('#grid').on('click', '.btn-info', function () {
        let id = $(this).data('id');
        $.get('/Product/Details/'+id, function (data) {

            console.log('------------------------------------');
            console.log(data);
            $('#modal-detail-placeholder').html(data);
           
            $('#detailModal').modal('show');
            $('#closeBtn').on('click', function () {
                $('#detailModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#detailModal').modal('hide');
                location.reload();
            });
        });
    });
    
});
//---------------------------------------- End pop up modal detail  section

//---------------------------------------- Start pop up modal Delete  section
$(function () {

    $('#grid').on('click', '.btn-danger', function () {
        let id = $(this).data('id');
        Swal.fire({
            title: 'Are you sure will delete this?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Product/Delete/' + id,
                    type: 'POST',
                    success: function (response) {
                        Swal.fire(
                            'Deleted!',
                            'Your record has been deleted.',
                            'success'
                        ).then(() => {
                            location.reload(); // atau update tabel pakai JS
                        });
                    },
                    error: function () {
                        Swal.fire(
                            'Error!',
                            'Something went wrong.',
                            'error'
                        );
                    }
                });
            }
        });
    });

});
//---------------------------------------- End pop up modal Delete  section