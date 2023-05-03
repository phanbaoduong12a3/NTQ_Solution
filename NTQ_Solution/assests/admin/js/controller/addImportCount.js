var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('#btnAddCount').off('click').on('click', function (e) {
            e.preventDefault;
            var btn = $(this);
            var productID = btn.data('productID');
            var importCount = btn.data('importCount');
            $.ajax({
                url: "Supplier/Index",
                data: {
                    productID: productID,
                    importCount: importCount + 1,
                },
                dataType: 'json',
                type: 'GET',
            });
        });
    }
}
product.init();