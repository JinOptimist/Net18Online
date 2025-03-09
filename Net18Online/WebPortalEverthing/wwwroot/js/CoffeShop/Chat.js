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

        console.log("Sending message:", message);
        hub.invoke("AddNewMessage", message)
        $(".new-coffe-message").val("");
    }

    function createNewMessage(message) {
        const messageBlock = $(".coffe-message.template").clone();
        messageBlock.removeClass("template");
        messageBlock.text(message);
        messageBlock.show();
        $(".coffe-messages").prepend(messageBlock);
    }

    function loadMessages(page = 1) {
        $.get(`/api/ApiCoffeChat/GetMessages?pageNumber=${page}&pageSize=${pageSize}`)
            .done(function (result) {
                result.messages.forEach(createNewMessage);
                updatePagination(page, result.totalCount);
            })
    }

    function updatePagination(page, totalCount) {
        const totalPages = Math.ceil(totalCount / pageSize);
        $(".pagination").empty();
        $(".pagination").append(`
            <li class="page-item ${page === 1 ? 'disabled' : ''}">
                <a class="page-link" href="#" data-page="${page - 1}">Previous</a>
            </li>
            <li class="page-item ${page === totalPages ? 'disabled' : ''}">
                <a class="page-link" href="#" data-page="${page + 1}">Next</a>
            </li>
        `);
    }

    hub.start()
        .then(() => loadMessages(currentPage))
});
