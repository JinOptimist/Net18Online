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
        let priceSpan = cartItem.find(".cart-price");

        let priceText = priceSpan.text().match(/[\d,\.]+/g)[0];
        let quantityText = quantitySpan.text().match(/\d+$/)[0];

        let pricePerItem = parseFloat(priceText.replace(',', '.')) / parseInt(quantityText);

        $.ajax({
            url: "/CoffeShop/RemoveFromCart",
            type: "POST",
            data: { coffeId: coffeId },
            success: function (response) {
                if (response.success) {
                    if (response.remainingQuantity > 0) {
                        quantitySpan.text(quantitySpan.text().replace(/\d+$/, response.remainingQuantity));
                        let newTotal = (pricePerItem * response.remainingQuantity).toFixed(2);
                        priceSpan.text(newTotal + " $");
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
