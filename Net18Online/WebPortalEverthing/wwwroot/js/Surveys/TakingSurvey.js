$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/takingSurvey")
        .build();

    hub.on("notify", notify);
    hub.start();

    const surveyBlock = $(".survey");
    const takingId = surveyBlock.find('#taking-id').val() - 0;

    $(".answer-control").change(onSetAnswer);

    function onSetAnswer() {
        let value = $(this).val();
        const questionBlock = $(this).closest('.question');
        let answerId = questionBlock.find('.answer-id').val() - 0;

        hub.invoke("setAnswerValue", answerId, value);
    }

    function notify(message) {
        alert(message);
    }
});
