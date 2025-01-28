$(document).ready(function () {
    $('.question-title').change(onSetTitleEvent);
    $('.question-required').change(onSetRequiredEvent);
    $('.answer-type').change(onSetAnswerTypeEvent);
    $('.del-button').click(onDeleteButtonClick);
    $('#add-button').click(onAddButtonClick);

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
        $.get(url).then(function (response) {
            if (response) {
                viewControlForQuestion(questionBlock, value);
            }
        });
    }

    function viewControlForQuestion(questionBlock, selectorValue) {
        let controlBlock = questionBlock.find('.control');
        controlBlock.empty();

        const ctrlByValue = `.ctrl-${selectorValue}-template`;
        const questionTemplateBlock = $(".question.template");
        let controlTemplateBlock = questionTemplateBlock.find('.control');
        let control = controlTemplateBlock.find(ctrlByValue).clone();

        controlBlock.append(control);
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

    function onAddButtonClick() {
        let surveyId = $("#survey-id").val();
        const url = `/api/ApiQuestion/Create?surveyId=${surveyId}`;
        $.get(url).then(function (questionId) {
            const questionTemplateBlock = $(".question.template").clone();
            questionTemplateBlock.removeClass("template");
            questionTemplateBlock.find('.question-id').val(questionId);
            questionTemplateBlock.find('.question-title').change(onSetTitleEvent);
            questionTemplateBlock.find('.question-required').change(onSetRequiredEvent);
            let answerTypeSelector = questionTemplateBlock.find('.answer-type');
            answerTypeSelector.change(onSetAnswerTypeEvent);
            questionTemplateBlock.find('.del-button').click(onDeleteButtonClick);

            viewControlForQuestion(questionTemplateBlock, answerTypeSelector.val());

            let questionsBlock = $('.questions');
            questionsBlock.append(questionTemplateBlock);
        });
    }
});