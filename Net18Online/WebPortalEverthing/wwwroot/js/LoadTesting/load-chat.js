$(document).ready(function () {
    // const baseUrl = `https://localhost:7292`;

    const authorId = $(".user-id").val() - 0;
    const authorName = $(".user-name").val();

    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/loadChat")
        // .withUrl(baseUrl + "/hub/loadChat")
        .build();

    init();

    hub.on("newMessageAdded", createNewMessage);

    $(".send-new-message").click(sendMessage);

    $(".new-message").on("keyup", function (e) {
        if (e.which == 13) {
            // 13 is a code of Enter key
            sendMessage();
        }
    });

    function sendMessage() {
        if (hub.state !== signalR.HubConnectionState.Connected) {
            console.error("Connection is not established.");
            return;
        }

        const message = $(".new-message").val();
        hub
            .invoke("addNewMessage", authorId, authorName, message)
            .catch((err) => console.error("Error sending message:", err));
        $(".new-message").val("");
    }

    hub
        .start()
        .then(function () {
            console.log("Connected to /hub/loadChat");
            //     hub.invoke("userEnteredToChat", authorId, authorName);
        })
        .catch((err) => {
            console.error("Connection failed", err);
            alert("Unable to connect to the chat. Please try again later.");
        });

    //TODO refactor the method. Get data from new minimal api server
    function init() {
        const url = "/api/ApiLoadChat/GetLastMessages";
        $.get(url).then(function (messages) {
            messages.forEach((message) => createNewMessage(message));
        });

        setTimeout(() => {
            $(".joke").show();
        }, 3 * 1000);
    }

    function createNewMessage(message) {
        const messageBlock = $(".message.template").clone();
        messageBlock.removeClass("template");
        messageBlock.text(message);
        $(".messages").append(messageBlock);
    }
});
