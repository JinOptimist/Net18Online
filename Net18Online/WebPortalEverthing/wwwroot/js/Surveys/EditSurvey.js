$(document).ready(function () {
    $('#add-button').click(function () {
        let newQestion = $(".question-template");
        let questions = $(".questions");

        questions.html(questions.html() + newQestion.html());
    });

    $('.del-button').click(function () {
        let question = $(this)
            .closest(".question")
            .remove();
    });
});