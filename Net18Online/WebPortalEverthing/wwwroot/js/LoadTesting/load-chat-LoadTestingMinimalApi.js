$(document).ready(function () {
  const baseUrl = "https://localhost:7121";

  //const baseUrl = window.location.origin;

  const authorId = $(".user-id").val() - 0;
  const authorName = $(".user-name").val();

  /*const hub = new signalR.HubConnectionBuilder()
    .withUrl(baseUrl + "/hub/loadChat")
    .build();*/

  const hub = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7130/hub/loadChat", {
      transport: signalR.HttpTransportType.WebSockets,
    })
    .configureLogging(signalR.LogLevel.Information)
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
    const message = $(".new-message").val();
    if (!message.trim()) {
      console.warn("Сообщение не может быть пустым.");
      return;
    }

    hub
      .invoke("addNewMessage", authorId, authorName, message)
      .then(() => {
        $(".new-message").val(""); // Очищаем поле ввода
      })
      .catch((err) => console.error("Ошибка при отправке сообщения:", err));
  }

  hub
    .start()
    .then(function () {
      hub.invoke("userEnteredToChat", authorId, authorName);
    })
    .catch(function (err) {
      console.error("Ошибка подключения к хабу:", err.toString());
    });

  //TODO refactor the method. Get data from new minimal api server
  function init() {
    const url = "/api/ApiLoadChat/GetLastMessages";
    $.get(url)
      .then(function (messages) {
        console.log("Полученные сообщения:", messages);
        messages.forEach((message) => createNewMessage(message));
      })
      .catch((err) => console.error("Ошибка загрузки сообщений:", err));
  }

  function createNewMessage(message) {
    if (hub.state !== signalR.HubConnectionState.Connected) {
      console.error("Соединение не установлено.");
      return;
    }
    const messageBlock = $(".message.template").clone();
    messageBlock.removeClass("template"); // Убираем класс template
    messageBlock.text(message); // Устанавливаем текст сообщения
    $(".messages").append(messageBlock); // Добавляем сообщение в контейнер
  }
});
