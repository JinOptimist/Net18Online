$(document).ready(function () {
    $(".survey-group .expander").click(function () {
        $(this)
            .closest(".survey-group")
            .find(".survey-group-content")
            .toggle(1000);

        let newValue = $(this).text() == '+' ? '-' : '+';
        $(this).text(newValue);
    });
});