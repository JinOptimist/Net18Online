$(document).ready(function () {
    $(".custom-accordion .custom-accordion-toggle").click(function () {
        $(this)
        .closest(".custom-accordion-item")
        .toggleClass("open")
    });
});
