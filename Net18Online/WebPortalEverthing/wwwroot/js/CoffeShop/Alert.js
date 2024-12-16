$(document).ready(function () {
    const $alertPlaceholder = $('#liveAlertPlaceholder');

    function appendAlert(message, type) {
        const $alert = $(`
            <div class="alert alert-${type} alert-dismissible" role="alert">
                <div>${message}</div>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>
        `);
        $alertPlaceholder.append($alert);

        setTimeout(function () {
            $alert.fadeOut(500, function () {
                $(this).remove();
            });
        }, 2000);
    }

    const $alertTrigger = $('#liveAlertBtn');
    if ($alertTrigger.length) {
        $alertTrigger.on('click', function () {
            appendAlert('Сообщение доставлено', 'success');
        });
    }
});
