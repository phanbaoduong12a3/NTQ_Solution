var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('#btnReviewNew').off('click').on('click', function (e) {
            e.preventDefault;
            var btn = $(this);
            var productid = btn.data('productid');
            var userid = btn.data('userid');
            var title = $('#txtReviewNew').val();
            //var rate = document.getElementById('ddlRate');
            if (title.value == "") {
                window.alert("Chưa nhập nội dung bình luận");
                return;
            }
            $.ajax({
                url: "/Home/AddNewReview",
                data: {
                    productid: productid,
                    userid: userid,
                    title: title,
                    // rate: rate
                },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        title.value = "";
                        window.alert("Bạn đã thêm bình luận thành công!");
                        $("#div_allreview").load("/Home/Detail?productid=" + productid);
                    }
                    else {
                        bootbox.alert("Thêm bình luận lỗi");
                    }
                }
            });
        });




    }
}
product.init();