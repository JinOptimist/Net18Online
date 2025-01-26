import React from "react";
function СountFridaysUntilNewYear() {
  // Текущая дата
  let today = new Date();

  // Дата наступления Нового Года (31 декабря текущего года)
  let newYear = new Date(today.getFullYear(), 11, 31); // Месяцы в JavaScript начинаются с 0, поэтому 11 - это декабрь

  // Количество пятниц
  let fridaysRemaining = 0;

  // Проход по дням до Нового Года
  while (today <= newYear) {
    if (today.getDay() === 5) {
      // Пятница - это день недели 5
      fridaysRemaining++;
    }
    today.setDate(today.getDate() + 1); // Переходим на следующий день
  }

  // Выводим результат на страницу
  <div>{fridaysRemaining}</div>;
  /*   document.getElementById("fridaysCount").innerText = fridaysRemaining;*/
  return fridaysRemaining;
}

// Запускаем функцию после загрузки страницы
/*window.onload = countFridaysUntilNewYear;*/

export default СountFridaysUntilNewYear;
