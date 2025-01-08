$(document).ready(function () {
  $(".discription-type .icon").click(function () {
    $(this).toggleClass("like"); //если был, убрать, если не було-добавит
    $(this).toggleClass("like-pushed");

    const metricId = $(this).closest(".discription-type").attr("metric-id");
    const url = `/api/ApiLoadTesting/Like?metricId=${metricId}`;
    $.get(url);
  });
});
