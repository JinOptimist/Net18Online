$(document).ready(function () {
  // const baseUrl = `https://localhost:7292`;

  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hub/loadChat")
    .build();
  hub.on("newMessageAdded", function (message) {
    if (hub.state !== signalR.HubConnectionState.Connected) {
      console.error("Connection is not established.");
      return;
    }
    const messageBlock = $(".message.template").clone();
    messageBlock.removeClass("template");
    messageBlock.text(message);
    $(".messages").append(messageBlock);
  });

  $(".send-new-message").click(function () {
    const message = $(".new-message").val();
    hub.invoke("addNewMessage", message);

    $(".new-message").val();
  });

  hub.start();
});
