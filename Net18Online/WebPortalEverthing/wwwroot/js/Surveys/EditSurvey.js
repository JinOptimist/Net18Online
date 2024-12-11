$(document).ready(function () {
    $('#add-button').click(function () {
        let surveyId = $("#survey-id").val();
        const url = `/api/ApiQuestion/Create?surveyId=${surveyId}`;
        $.get(url).then(function (questionId) {
            const questionTemplateBlock = $(".question.template").clone();
            questionTemplateBlock.removeClass("template");
            questionTemplateBlock.find('.question-id').val(questionId);
            questionTemplateBlock.find('.question-title').change(onSetTitleEvent);
            questionTemplateBlock.find('.question-required').change(onSetRequiredEvent);
            questionTemplateBlock.find('.answer-type').change(onSetAnswerTypeEvent);
            questionTemplateBlock.find('.del-button').click(onDeleteButtonClick);

            let questionsBlock = $('.questions');
            questionsBlock.append(questionTemplateBlock);
        });
    });

    $('.question-title').change(onSetTitleEvent);
    $('.question-required').change(onSetRequiredEvent);
    $('.answer-type').change(onSetAnswerTypeEvent);
    $('.del-button').click(onDeleteButtonClick);

    function onSetTitleEvent() {
        let value = $(this).val();
        const questionBlock = $(this).closest('.question');
        let questionId = questionBlock.find('.question-id').val();
        const url = `/api/ApiQuestion/UpdateTitle?id=${questionId}&value=${value}`;
        $.get(url);
    }

    function onSetRequiredEvent() {
        let value = this.checked;
        const questionBlock = $(this).closest('.question');
        let questionId = questionBlock.find('.question-id').val();
        const url = `/api/ApiQuestion/UpdateRequired?id=${questionId}&value=${value}`;
        $.get(url);
    }

    function onSetAnswerTypeEvent() {
        let value = $(this).val();
        const questionBlock = $(this).closest('.question');
        let questionId = questionBlock.find('.question-id').val();
        const url = `/api/ApiQuestion/UpdateAnswerType?id=${questionId}&value=${value}`;
        $.get(url);
    }

    function onDeleteButtonClick() {
        const questionBlock = $(this).closest('.question');
        let questionId = questionBlock.find('.question-id').val();
        const url = `/api/ApiQuestion/Delete?id=${questionId}`;
        $.get(url).then(function (response) {
            if (response) {
                questionBlock.remove();
            }
        });
    }
});