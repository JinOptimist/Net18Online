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

$(".discription-type .view-mode").click(function () {
  const nameBlock = $(this).closest(".discription-type");
  nameBlock.find(".view-mode").hide();
  nameBlock.find(".edit-mode").show();
});

$(".update-metric-button").click(function () {
  const updateButton = $(this);

  // Найти ближайший родительский контейнер с классом .discription-type
  const discriptionBlock = updateButton.closest(".discription-type");

  // Найти .edit-mode и .view-mode внутри этого контейнера
  const editModeBlock = discriptionBlock.find(".edit-mode");
  const viewModeBlock = discriptionBlock.find(".view-mode");

  // Считать значения из полей редактирования
  const metricId = editModeBlock.find(".update-metric-id").val();
  const metricName = editModeBlock.find(".update-metric-name").val();
  const metricGuid = editModeBlock.find(".update-metric-guid").val();
  const metricThrough = editModeBlock.find(".update-metric-throughput").val();
  const metricAverage = editModeBlock.find(".update-metric-average").val();

  console.log("finded id, name, guid, throughput, average.");

  const url =
    `/LoadTesting/UpdateMetric?` +
    `id=${metricId}&` +
    `name=${metricName}&` +
    `guid=${metricGuid}&` +
    `ThroughputInput=${metricThrough}&` +
    `AverageInput=${metricAverage}`;

  // Отправить GET-запрос
  $.get(url).then(function (response) {
    if (response) {
      // Успешное обновление, обновляем текст в режиме просмотра
      viewModeBlock.text(metricName);
      viewModeBlock.text(metricGuid);
    } else {
      // В случае ошибки вернуть старое значение
      const oldName = viewModeBlock.text();
      editModeBlock.find(".update-metric-name").val(oldName);
    }

    // Скрыть edit-mode и показать view-mode
    viewModeBlock.show();
    console.log("view-mode is now visible");
    editModeBlock.hide();
    console.log("edit-mode is now hidden");

    // Разблокировать кнопки и поля
    updateButton.removeAttr("disabled");
    editModeBlock.find(".update-metric-name").removeAttr("disabled");
  });

  // Заблокировать кнопку и поле ввода во время запроса
  updateButton.attr("disabled", "disabled");
  editModeBlock.find(".update-metric-name").attr("disabled", "disabled");
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
