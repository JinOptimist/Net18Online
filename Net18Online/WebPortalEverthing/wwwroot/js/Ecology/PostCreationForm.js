// Получение элементов
let modal = document.getElementById("myModal");
let btn = document.getElementById("openFormBtn");
let span = document.getElementsByClassName("close")[0];

// открытие
btn.onclick = function() {
    modal.style.display = "block";
}

// (x), закрывается окно
span.onclick = function() {
    modal.style.display = "none";
}

// закрытие
window.onclick = function(event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
}

// редактирования
function showEditForm(id, url, text) {
    document.getElementById("editId").value = id;
    document.getElementById("editImageUrl").value = url;
    document.getElementById("editImageText").value = text;
    document.getElementById("editForm").style.display = "block";
}
