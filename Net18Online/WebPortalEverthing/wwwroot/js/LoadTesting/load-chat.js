$(document).ready(function () {
  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hub/loadChat")
    .build();

  init();

  function init() {
    const url = "/api/ApiLoadChat/GetLastMessages";
    $.get(url)
      .then(function (messages) {
        console.log("Полученные сообщения:", messages);
        messages.forEach((message) => createNewMessage(message));
      })
      .catch((err) => console.error("Ошибка загрузки сообщений:", err));
  }

  hub.on("newMessageAdded", createNewMessage);

  $(".send-new-message").click(function () {
    const message = $(".new-message").val();
    if (!message.trim()) {
      console.warn("Сообщение не может быть пустым.");
      return;
    }

    hub
      .invoke("addNewMessage", message)
      .then(() => {
        $(".new-message").val(""); // Очищаем поле ввода
      })
      .catch((err) => console.error("Ошибка при отправке сообщения:", err));
  });

  $(".send-help").click(function () {
    let message = $(".help-admin").val();
    if (!message.trim()) {
      console.warn("Сообщение не может быть пустым.");
      return;
    }

    hub
      .invoke("addNewMessageToAdmin", message)
      .then(() => {
        $(".help-admin").val(""); // Очищаем поле ввода
        console.log("Сообщение отправлено администратору:", message);
      })
      .catch((err) =>
        console.error("Ошибка при отправке сообщения администратору:", err)
      );
  });

  hub
    .start()
    .then(function () {
      hub.invoke("userEnteredToChat");
    })
    .catch(function (err) {
      console.error("Ошибка подключения к хабу:", err.toString());
    });

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
