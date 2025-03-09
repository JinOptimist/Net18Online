let currentPage = 1;
const pageSize = 4;

$(document).ready(function () {
    const hub = new signalR.HubConnectionBuilder()
        .withUrl("/hub/chatCoffePage")
        .build();

    hub.on("newMessageAdded", createNewMessage);

    $(".send-new-coffe-message").click(sendMessage);

    $(".new-coffe-message").on("keyup", function (e) {
        if (e.which == 13) {
            sendMessage();
        }
    });

    $(".pagination").on("click", ".page-link", function () {
        const page = $(this).data("page");
        loadMessages(page);
    });

    function sendMessage() {
        const message = $(".new-coffe-message").val();
        if (!message.trim()) return;
        hub.invoke("AddNewMessage", message)
        $(".new-coffe-message").val("");
    }

    function createNewMessage(message) {
        const messageBlock = $(".coffe-message.template").clone();
        messageBlock.removeClass("template");
        messageBlock.find(".message-text").text(message);
        messageBlock.find(".delete-button").attr("data-message", message);
        messageBlock.show();
        $(".coffe-messages").prepend(messageBlock);
    }


    function loadMessages(page = 1) {
        $.get(`/api/ApiCoffeChat/GetMessages?pageNumber=${page}&pageSize=${pageSize}`)
            .done(function (result) {
                $(".coffe-message").each(function () {
                    const messagePage = $(this).data("page");
                    if (messagePage !== page) {
                        $(this).hide();
                    } else {
                        $(this).show();
                    }
                });

                result.messages.forEach(function (message) {
                    const existingMessage = $(`.coffe-message[data-message='${message}']`);
                    if (existingMessage.length === 0) {
                        createNewMessage(message);
                    }
                });

                updatePagination(page, result.totalCount);
            });
    }

    $(".coffe-messages").on("click", ".delete-button", function () {
        const messageBlock = $(this).closest(".coffe-message");
        const message = $(this).data("message");

        deleteMessage(message, messageBlock);
    });

    function deleteMessage(message, messageBlock) {
        $.ajax({
            url: "/api/ApiCoffeChat/DeleteMessage",
            type: "DELETE",
            data: JSON.stringify({ message: message }),
            contentType: "application/json",
            success: function () {
                messageBlock.remove();
            },
        });
    }

    function updatePagination(page, totalCount) {
        const totalPages = Math.ceil(totalCount / pageSize);
        $(".pagination").empty();
        $(".pagination").append(`
            <li class="page-item ${page === 1 ? 'disabled' : ''}">
                <a class="page-link" href="#" data-page="${page - 1}">Previous</a>
            </li>
            <li class="page-item disabled">
                <span class="page-link">${1}</span>
            </li>
           <li class="page-item">
                <span class="page-link">${page}</span>
            </li>
            <li class="page-item">
                <span class="page-link disabled">${totalPages}</span>
            </li>
            <li class="page-item ${page === totalPages ? 'disabled' : ''}">
                <a class="page-link" href="#" data-page="${page + 1}">Next</a>
            </li>
        `);
    }

    hub.start()
        .then(() => loadMessages(currentPage))
});
