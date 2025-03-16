$(document).ready(function () {
    $(".filter-radio").on("change", function () {
        var selectedBrand = $(this).attr("id");

        $.ajax({
            url: "/CoffeShop/FilterByBrand",
            type: "GET",
            data: { brandName: selectedBrand },
            success: function (response) {
                var tempDiv = $('<div>').html(response);
                var newContent = tempDiv.find('.main-content').html();
                $(".main-content").html(newContent);
            },
        });
    });

    $('#resetFilter').on('click', function () {
        $.ajax({
            url: '/CoffeShop/Coffe',
            type: 'GET',
            success: function (result) {
                var newContent = $(result).find('.main-content').html();
                $('.main-content').html(newContent);
                $('.filter-radio').prop('checked', false);
            },
        });
    });
});
