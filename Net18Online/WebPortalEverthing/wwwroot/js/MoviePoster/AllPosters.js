$(document).ready(function () {

  $(".movie .toggle-tags").click(function () {
    $(this)
      .closest('.movie')
      .find('.poster-tag-container')
      .toggle(500);

      $('.movies')
      .removeClass('highlighting');
  });

  $('.movie .tag').click(function(){
    const tagValue = $(this).text();

    const sameTags = $(`.tag:contains('${tagValue}')`);

    $('.movie')
    .removeClass('highlighted');

    sameTags.each((index, elem) => {
      $(elem)
        .closest('.movie')
        .addClass('highlighted');
    });

    $('.movies')
      .addClass('highlighting');

  });
});