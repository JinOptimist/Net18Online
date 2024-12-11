$(document).ready(function () {

    $(".shop-info-table .view-mode").click(function(){
        const nameObject = $(this).closest(".table-block");
        nameObject.find(".view-mode").hide();
        nameObject.find(".edit-mode").show();
    });

    $(".shop-info-table .edit-mode .close-edit-button").click(function(){
        const nameObject = $(this).closest(".table-block");
        nameObject.find(".edit-mode").hide();
        nameObject.find(".view-mode").show();
    });

    $(".update-coffe-button").click(function () {
        const updateButton = $(this);
        const nameObject = updateButton.closest(".table-block");
        const coffeId = nameObject.find(".coffe-id").val();
        const coffeName = nameObject.find(".new-coffe-name").val();

        const url = `/api/ApiCoffe/UpdateCoffe?id=${coffeId}&name=${coffeName}`;
        $.get(url).then(function(response){
            if (response){
               nameObject.find(".view-mode").text(coffeName);     
            } else {
                const oldCoffeName = nameObject.find(".view-mode").text();
                nameObject.find(".new-coffe-name").val(oldCoffeName);
            }

            nameObject.find(".edit-mode").hide();
            nameObject.find(".view-mode").show();
            updateButton.removeAttr("disabled");
            nameObject.find(".new-coffe-name").removeAttr("disabled");
        });

        updateButton.attr("disabled", "disabled");
        nameObject.find(".new-coffe-name").attr("disabled", "disabled");
    });

    $(".object-delete").click(function(event){
        const coffeBlock = $(this).closest(".coffe-object");
        const coffeId = $(this).attr("data-id");
        const url = `/api/ApiCoffe/Delete?id=${coffeId}`;
        $.get(url).then(function (response) {
          if (response) {
            coffeBlock.remove();
          }
        });
    
        event.preventDefault();
    });


});
