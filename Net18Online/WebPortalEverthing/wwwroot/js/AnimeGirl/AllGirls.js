$(document).ready(function () {

    init();

    function onToggleButtonClick() {
        $(this)
            .closest(".girl") // tag .girl was founded
            .find(".tag-container")
            .toggle(1000);

        $(".girls").removeClass("highlight-is-active");
    }
    $(".girl .toggle-tags").click(onToggleButtonClick);

    $(".girl .image-container img").click(function () {
        $(this).closest(".image-container").toggleClass("full");
    });

    $(".girl .tag").click(function () {
        if ($(this).hasClass("delete")) {
            return;
        }

        const tagValue = $(this).text();

        const theSameTags = $(`.tag:contains('${tagValue}')`);

        $(".girl").removeClass("highlighted");

        theSameTags.each((index, elem) => {
            $(elem).closest(".girl").addClass("highlighted");
        });

        $(".girls").addClass("highlight-is-active");
    });

    $(".girl .name .view-mode").click(function () {
        const nameBlock = $(this).closest(".name");
        nameBlock.find(".view-mode").hide();
        nameBlock.find(".edit-mode").show();
    });

    $(".update-girl-name-button").click(function () {
        const updateButton = $(this);
        const nameBlock = updateButton.closest(".name");
        const girlId = nameBlock.find(".girl-id").val();
        const girlName = nameBlock.find(".new-girl-name").val();

        const url = `/api/ApiGirl/UpdateName?id=${girlId}&newName=${girlName}`;
        $.get(url).then(function (response) {
            if (response) {
                nameBlock.find(".view-mode").text(girlName);
            } else {
                const oldName = nameBlock.find(".view-mode").text();
                nameBlock.find(".new-girl-name").val(oldName);
            }

            nameBlock.find(".view-mode").show();
            nameBlock.find(".edit-mode").hide();
            updateButton.removeAttr("disabled");
            nameBlock.find(".new-girl-name").removeAttr("disabled");
        });

        updateButton.attr("disabled", "disabled");
        nameBlock.find(".new-girl-name").attr("disabled", "disabled");
    });

    $(".tag.delete").click(function (event) {
        const girlBlock = $(this).closest(".girl");
        const girlId = $(this).attr("data-id");
        const url = `/api/ApiGirl/Remove?id=${girlId}`;
        $.get(url).then(function (response) {
            if (response) {
                girlBlock.remove();
            }
        });

        event.preventDefault();
    });

    $(".create-girl-button").click(function () {
        const url = "/api/ApiGirl/Create";
        const newGirlName = $(".girl.create .new-girl-name").val();
        const girlImageUrl = $(".girl.create .new-girl-url").val();
        const mangaId = $(".girl.create .manga-id").val();
        const data = {
            name: newGirlName,
            url: girlImageUrl,
            mangaId: mangaId,
        };

        SendPost(url, data).then(function (girl) {
            const girlBlock = $(".girl.template").clone();
            girlBlock.removeClass("template");
            girlBlock.find(".view-mode").text(girl.name);
            girlBlock.find(".image-container img").attr("src", girl.imageSrc);
            girlBlock.find(".tag-container .tag.creator").text(girl.creatorName);
            girlBlock.find(".tag-container .tag.manga").text(girl.mangaName);

            girlBlock.find(".toggle-tags").click(onToggleButtonClick);
            girlBlock.insertBefore($(".girl.create"));
        });
    });

    $(".girl .name .icon").click(function () {
        $(this).toggleClass("like");
        $(this).toggleClass("like-pushed");

        const girlId = $(this).closest(".girl").attr("data-id");
        const url = `/api/ApiGirl/Like?girlId=${girlId}`;
        $.get(url);
    });

    $('.order').click(function () {
        const order = $(this).attr('data-order');
        const url = new URL(document.location.href);

        const orderOld = url.searchParams.get('fieldNameForSort');
        if (orderOld == order) {
            url.searchParams.set('orderDirection', 'Desc');
        } else {
            url.searchParams.set('orderDirection', 'Asc');
        }

        url.searchParams.set('page', 1);

        url.searchParams.set('fieldNameForSort', order);
        window.location.href = url.href;
    });

    $('[name=perPage]').change(function () {
        const perPage = $(this).val();
        const url = new URL(document.location.href);
        url.searchParams.set('perPage', perPage);
        url.searchParams.set('page', 1);
        window.location.href = url.href;
    });

    $(".page").click(function () {
        const page = $(this).attr('data-page');
        const url = new URL(document.location.href);
        url.searchParams.set('page', page);
        window.location.href = url.href;
    });

    function init() {
        const url = new URL(document.location.href);
        const perPage = url.searchParams.get('perPage');
        $('[name=perPage]').val(perPage);
    }
});
