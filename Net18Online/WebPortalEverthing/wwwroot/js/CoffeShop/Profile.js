$(document).ready(function () {
    $(".profile .list-of-coffe").click(function () {
        $(".all-coffe-user").toggleClass("open");
    });

    $(document).on("click", ".cart-remove-btn", function (e) {
        e.preventDefault();

        let button = $(this);
        let cartItem = button.closest(".cart-item");
        let coffeId = cartItem.find("input[name='coffeId']").val();
        let quantitySpan = cartItem.find(".cart-quantity");

        $.ajax({
            url: "/CoffeShop/RemoveFromCart",
            type: "POST",
            data: { coffeId: coffeId },
            success: function (response) {
                if (response.success) {
                    if (response.remainingQuantity > 0) {
                        quantitySpan.text(quantitySpan.text().replace(/\d+$/, response.remainingQuantity));
                    } else {
                        cartItem.fadeOut(300, function () {
                            $(this).remove();
                        });
                    }
                }
            },
        });
    });
});
