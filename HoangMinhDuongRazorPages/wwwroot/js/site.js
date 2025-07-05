$(document).ready(function () {
    $('.create-btn').click(function () {
        var url = $(this).data('url');
        $.get(url, function (data) {
            $('#modal-body').html(data);
            $('#modal').modal('show');
        });
    });

    $('.delete-btn').click(function () {
        if (confirm('Bạn có chắc chắn muốn xóa?')) {
            var url = $(this).data('url');
            $.post(url, function (data) {
                if (data.success) {
                    location.reload();
                } else {
                    alert(data.message);
                }
            });
        }
    });
});