$(document).ready(function () {
  $(".girl .toggle-tags").click(function () {
    $(this)
      .closest(".girl") // tag .girl was founded
      .find(".tag-container")
      .toggle(1000);

    $(".girls").removeClass("highlight-is-active");
  });

  $(".girl .image-container img").click(function () {
    $(this).closest(".image-container").toggleClass("full");
  });

  $(".girl .tag").click(function () {
    const tagValue = $(this).text();

    // $(".tag:contains('" + tagValue +"')")
    const theSameTags = $(`.tag:contains('${tagValue}')`);

    $(".girl").removeClass("highlighted");

    theSameTags.each((index, elem) => {
      $(elem).closest(".girl").addClass("highlighted");
    });

    $(".girls").addClass("highlight-is-active");
  });
});
