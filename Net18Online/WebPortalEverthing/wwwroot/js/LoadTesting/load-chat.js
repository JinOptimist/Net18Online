$(document).ready(function () {
  const hub = new signalR.HubConnectionBuilder()
    .withUrl("/hub/loadChat")
    .build();

  // Обработка нового сообщения
  hub.on("newMessageAdded", function (message) {
    if (hub.state !== signalR.HubConnectionState.Connected) {
      console.error("Соединение не установлено.");
      return;
    }

    // Клонирование шаблона сообщения
    const messageBlock = $(".message.template").clone();
    messageBlock.removeClass("template"); // Убираем класс template
    messageBlock.text(message); // Устанавливаем текст сообщения
    $(".messages").append(messageBlock); // Добавляем сообщение в контейнер
  });

  // Отправка нового сообщения
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
      .catch((err) => console.error(err.toString()));
  });

  // Отправка сообщения администратору
  $(".send-help").click(function () {
    const message = $(".help-admin").val();
    if (!message.trim()) {
      console.warn("Сообщение не может быть пустым.");
      return;
    }

    hub
      .invoke("addNewMessageToAdmin", message)
      .then(() => {
        $(".help-admin").val(""); // Очищаем поле ввода
      })
      .catch((err) => console.error(err.toString()));
  });

  // Запуск подключения к SignalR-хабу
  hub
    .start()
    .than(function (data) {
      // When connection with server is alive
      hub.invoke("userEnteredToChat");
    })
    .catch(function (err) {
      console.error("Ошибка подключения к хабу:", err.toString());
    });
});
