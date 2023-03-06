var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id =btn.data('id');
            $.ajax({
                url: "/Admin/User/Delete",
                data: { id: id },
                dataType: "json",
                type:"POST",
                success: function (response) {
                    alert("Delete secessfull");
                }
            })
        });
    }
}
user.init();