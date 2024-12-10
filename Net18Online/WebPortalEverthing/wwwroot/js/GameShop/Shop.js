alert(1)
$(document).ready(function (){
    function onToggleButtonClick() {
        $(this)
        .closest(".game-block")
        .find(".info")
        .toggle(1000)        
            
        $(".content").removeClass("highlight-is-active");
    }
    $(".game-block .toggle-tags").click(onToggleButtonClick);              
    
    
    $(".game-block .image-container img").click(function(){
        $(this)
            .closest(".image-container")
            .toggleClass("full");
    });

    $(".game-block .tag").click(function (){
        if($(this).hasClass('delete')){
            return;
        }
        const tagValue = $(this).text();

        const theSameTags = $(`.tag:contains(${tagValue})`)

        $('.game-block').removeClass('highlighted')

        theSameTags.each((index, elem) => {
            $(elem)
                .closest('.game-block')
                .addClass('highlighted');
        });

            $(".content")
                .addClass("highlight-is-active")

    });

    $('.game-block .name .view-mode').click(function(){
        const nameBlock = $(this).closest('.name');         
        nameBlock.find('.view-mode').hide();
        nameBlock.find('.edit-mode').show();
    });

    $(".update-game-name-button").click(function(){
        const updateButton = $(this);
        const nameBlock = updateButton.closest('.name');
        const gameID = nameBlock.find('.game-id').val();
        const gameName = nameBlock.find('.new-game-name').val();

        const url = `/api/ApiGameStore/UpdateName?id=${gameID}&newName=${gameName}`;
        $.get(url)
            .then(function(response){
                if (response){
                    nameBlock.find('.view-mode').text(gameName)
                }
                else{
                    const oldName = nameBlock.find('.view-mode').text();
                    nameBlock.find('.new-game-name').val(oldNames);
                }                
                nameBlock.find('.view-mode').show();
                nameBlock.find('.edit-mode').hide();

                updateButton.removeAttr('disabled', 'disabled');
                nameBlock.find('.new-game-name').removeAttr('disabled', 'disabled');
            });        

            updateButton.attr('disabled', 'disabled');
            nameBlock.find('.new-game-name').attr('disabled', 'disabled');
    });

    $(".tag.delete").click(function(event){
        const gameBlock = $(this).closest('.game-block');
        const gameId = $(this).data('id');
        const url = `/api/ApiGameStore/Remove?id=${gameId}`;
        $.get(url)
            .then(function(response){
                if(response){
                    gameBlock.remove();       
                }
        });
        event.preventDefault();
    });
    $('.create-game-button').click(function(){
        const url = "/api/ApiGameStore/Add";
        const newGameName = $(".game-block.create .new-game-name").val();
        const gameImageUrl = $(".game-block.create .new-game-url").val();
        const cost = $(".game-block.create .cost").val();
        const data = {
          name: newGameName,
          url: gameImageUrl,
          cost: cost,
        };

        SendPost(url, data)
            .then(function(game){
                const gameBlock = $('.game-block.template').clone();
                gameBlock.removeClass("template");
                gameBlock.find(".view-mode").text(game.nameGame);
                gameBlock.find(".image-container img").attr("src", game.imageSrc);
                gameBlock.find(".info.cost").text(game.cost);
                gameBlock.find(".info.studios").text(game.studios);

                gameBlock.find(".toggle-tags").click(onToggleButtonClick);
                gameBlock.insertBefore($(".game-block.create"))
            });
    });
});
