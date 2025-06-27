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

//---------------------------------------- Start pop up modal create  section
$(function () {

    $('#addBtn').on('click', function () {
        $.get('/Product/Create', function (data) {
            // Masukkan HTML yang didapat ke dalam placeholder
            console.log('------------------------------------');
            console.log(data);
            $('#modal-placeholder').html(data);
            // Tampilkan modal
            $('#productModal').modal('show');
            $('#cancelBtn').on('click', function () {
                $('#productModal').modal('hide');
                location.reload();
            });
            $('#xClose').on('click', function () {
                $('#productModal').modal('hide');
                location.reload();
            });
        });
    });

    // due to I used event delegation using jquery, I disabled the default submit form behavior
    $(document).on('submit', '#productForm', function (e) {
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
                        $('#productModal').modal('hide');
                        alert(response.message);
                        location.reload();
                    } else {// if not  display the error
                        console.log('error submit---------------');
                        console.log(response);
                        $('#modal-placeholder').html(response);
                        $('#productModal').modal('show');
                        $('#cancelBtn').on('click', function () {
                            $('#productModal').modal('hide');
                            location.reload();
                        });
                        $('#xClose').on('click', function () {
                            $('#productModal').modal('hide');
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