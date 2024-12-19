$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/chatCoffePage")
        .build();
    
    
    hub.on("newMessageAdded", createNewMessage);
       
    $(".send-new-coffe-message").click(sendMessage);
    
    $(".new-coffe-message").on("keyup", function (e) {
        if (e.which == 13) {
            // 13 is a code of Enter key
            sendMessage();
        }
    });

    function sendMessage() {
        const message = $(".new-coffe-message").val()
        hub.invoke("AddNewMessage", message)
        $(".new-coffe-message").val("");
            
    }

    function createNewMessage(message) {
        const messageBlock = $(".coffe-message.template").clone();
        messageBlock.removeClass("template");
        messageBlock.text(message);
        messageBlock.show();
        $(".coffe-messages").append(messageBlock);
    }

    hub.start();
});
