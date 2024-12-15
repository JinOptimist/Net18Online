//alert("Hellow Russian");
$(".discription-type .type-container img").click(function () {
  console.log("Image click/ Hellow/==== Russian");
  $(this) //элемент, по которому кликнули
    .closest(".type-container")
    .toggleClass("full");
});

$(".toggle-button-tag-additional-info").click(function () {
  console.log("toggle metrics");
  $(this) //элемент, по которому кликнули
    .closest(".discription-type") //родителя нашли
    .find(".additional-info") //у родителя нашли по селектору элемент
    .slideToggle(1000); // Используем slideToggle для плавного появления/скрытия элементов
});

$(".additional-info .tag").click(function () {
  console.log("click on tag");
  const tagValue = $(this).text();
  console.log("finded text of tag");
  //  const theSameTags = $(`.tag:contains('${tagValue}')`);
  const theSameTags = $(".tag").filter(function () {
    return $(this).text().includes(tagValue);
  });
  if (theSameTags.length > 0) {
    $(".discription-type").removeClass("highlighted");

    theSameTags.each((index, elem) => {
      $(elem).closest(".discription-type").addClass("highlighted");
    });

    $(".discription-type").addClass("highlight-is-active");
  } else {
    console.error("Element not finded.");
  }
});

$(".update-metric-button").click(function () {
  const editModeBlock = $(this).closest(".edit-mode");
  const metricId = editModeBlock.find(".update-metric-id").val();
  const metricName = editModeBlock.find(".update-metric-name").val();
  const metricGuid = editModeBlock.find(".update-metric-guid").val();
  const metricThrough = editModeBlock.find(".update-metric-throughput").val();
  const metricAverage = editModeBlock.find(".update-metric-average").val();

  const url = `/LoadTesting/UpdateMetric?
  id=${metricId}&
  name=${metricName}&
  guid=${metricGuid}&
  ThroughputInput=${metricThrough}&
  AverageInput=${metricAverage}`;

    $.get(url).then(function (response) {
      if (response) {
        nameBlock.find(".view-mode").text(metricName);
      } else {
        const oldName = nameBlock.find(".view-mode").text();
        nameBlock.find(".update-metric-name").val(oldName);
      }

      nameBlock.find(".view-mode").show();
      nameBlock.find(".edit-mode").hide();
      updateButton.removeAttr("disabled");
      nameBlock.find(".update-metric-name").removeAttr("disabled");
});

// $(".discription-type .type-container img").click(function () {
// .css("display", "inline-block"); //сменили у найденного элемента скрытое(none) отображение на видимое
//покажи// .show();
//спрятать// hide();
//было // $(".discription-type .additional-info .tag").css("display", "inline-block");

/*$(document).ready(function () {
    $(".discription-type .type-container img").click(function () {
        console.log("Hellow/==== Russian");
    $(this) //элемент, по которому кликнули
      .closest(".discription-type") //родителя нашли
      .find(".additional-info .tag") //у родителя нашли по селектору элемент
      .slideToggle(); // Используем slideToggle для плавного появления/скрытия элементов
  });
});*/
