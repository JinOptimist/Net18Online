$(document).ready(function () {
    $(".survey-group .title .icon").click(function () {
        $(this)
            .closest(".survey-group")
            .find(".survey-group-content")
            .toggle(1000);

        $(this).toggleClass("arrow-right");
        $(this).toggleClass("arrow-down");
    });
});