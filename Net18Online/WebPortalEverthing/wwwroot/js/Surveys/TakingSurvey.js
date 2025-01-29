$(document).ready(function () {
    const delayBeforeDeleteNotificatoin = 3 * 1000;
    const animationTime = 500;

    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/takingSurvey")
        .build();

    hub.on("notify", notify);
    hub.on("redirectPage", redirectPage);
    hub.on("markUnansweredQuestions", markUnansweredQuestions);
    hub.start();

    const surveyBlock = $(".survey");
    const takingId = surveyBlock.find('#taking-id').val() - 0;

    $(".answer-control").change(onSetAnswer);
    $(".save-button").click(onSave);

    function onSetAnswer() {
        let value = $(this).val();
        const questionBlock = $(this).closest('.question');
        let answerId = questionBlock.find('.answer-id').val() - 0;

        hub.invoke("setAnswerValue", answerId, value);
    }

    function notify(message) {
        const notificationContainer = $(".notification-container")
        let alertTemplate = notificationContainer.find(".alert.template").clone();

        alertTemplate.removeClass("template");
        alertTemplate.find(".text").text(message);

        notificationContainer.append(alertTemplate);

        setTimeout(() => {
            alertTemplate.hide(animationTime);
        }, delayBeforeDeleteNotificatoin);
    }

    function onSave() {
        hub.invoke("submitSurvey", takingId);
    }

    function redirectPage(url) {
        window.location.href = url;
    }

    function markUnansweredQuestions(ids) {
        $('.questions .question').each(function (index, element) {
            let answerId = $(element).find('.answer-id').val() - 0;
            if (ids.includes(answerId)) {
                $(element).addClass('unanswered');
            }
            else {
                $(element).removeClass('unanswered');
            }
        });

        notify(`Необходимо ответить на все вопросы. Неотвеченных вопросов: ${ids.length}.`);
    }
});
