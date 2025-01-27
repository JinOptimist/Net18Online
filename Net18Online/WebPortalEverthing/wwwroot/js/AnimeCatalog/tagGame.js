$(document).ready(function () {
    function initializeTileClickHandlers() {
        $('.tag').off('click').on('click', function () {
            var tileX = $(this).data('x');
            var tileY = $(this).data('y');

            console.log("Координаты:", tileX, tileY);

            $.ajax({
                url: '/AnimeCatalog/MoveTile',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ x: tileX, y: tileY }),
                success: function (response) {
                    if (response.success) {
                        updateBoard(response.tags);

                        if (response.isWin) {
                            setTimeout(function () {
                                console.log("Вы победили!");
                                changeTags();
                            }, 50);
                        }
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Ошибка AJAX:", status, error);
                    alert("Ошибка: " + error);
                }
            });
        });
    }

    function updateBoard(tags) {
        var puzzleBoard = $('.puzzle-board');
        puzzleBoard.empty();

        for (var i = 0; i < tags.length; i++) {
            var row = $('<div class="row"></div>');
            for (var j = 0; j < tags[i].length; j++) {
                var tile = $('<div class="tag"></div>');
                tile.data('x', i).data('y', j);

                if (tags[i][j] === 0) {
                    tile.addClass('empty');
                } else {
                    tile.text(tags[i][j]);
                }

                row.append(tile);
            }
            puzzleBoard.append(row);
        }

        initializeTileClickHandlers();
    }

    function changeTags() {
        $.ajax({
            url: '/AnimeCatalog/ChangeTags',
            type: 'GET',
            success: function (response) {
                if (response.success) {
                    updateBoard(response.tags);
                    console.log("Новая игра началась!");
                } else {
                    alert(response.message || "Не удалось обновить пятнашки.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Ошибка при смене пятнашек:", status, error);
                alert("Ошибка: " + error);
            }
        });
    }

    initializeTileClickHandlers();
});
