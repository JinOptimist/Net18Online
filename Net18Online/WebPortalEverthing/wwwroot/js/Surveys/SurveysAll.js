$(document).ready(function () {
    $(".survey-group .title .icon").click(function () {
        let value = $(this).hasClass('arrow-right');
        showSurveyGroupContentBlock($(this), value);
    });

    $('.check-expand-groups').click(function () {
        let value = this.checked;

        setCookie("survey-expand-groups", value);

        $(".survey-group .title .icon").each(function () {
            showSurveyGroupContentBlock($(this), value);
        });
    });

    function showSurveyGroupContentBlock(arrowBlock, isShow) {
        let surveyGroupContentBlock = arrowBlock
            .closest(".survey-group")
            .find(".survey-group-content");

        if (isShow) {
            surveyGroupContentBlock.addClass("d-block");
            surveyGroupContentBlock.removeClass("d-none");

            arrowBlock.addClass("arrow-down");
            arrowBlock.removeClass("arrow-right");
        }
        else {
            surveyGroupContentBlock.addClass("d-none");
            surveyGroupContentBlock.removeClass("d-block");

            arrowBlock.addClass("arrow-right");
            arrowBlock.removeClass("arrow-down");
        }
    }
});