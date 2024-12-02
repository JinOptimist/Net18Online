var nameV1 = "Ivan"; // var is OUTDATED
var nameV2 = "Ivan"; // var is OUTDATED

let nameV3 = "Ivan";
let nameV4; // undefined

const nameV5 = "Ivan"; // some sort of readonly

let age = 15; // number
let chanceToWin = 0.5; // number

let isMan = true;

let userV1 = {
  name: nameV2,
  age: age,
  isMan: isMan,
};

let userV2 = {
  name: "Lera",
  age: 17,
  isAdult: false,
};

function DoFun() {
  console.log("Hi");
}

function Calc(firstNumber, secondNumber) {
  return firstNumber + secondNumber;
}

let CalcV2 = function (firstNumber, secondNumber) {
  return firstNumber + secondNumber;
};

let CalcV3 = (firstNumber, secondNumber) => {
  return firstNumber + secondNumber;
};

CalcV2(1, 5);
Calc(5, 9);

function unusualFunction(user, favMethod) {
  console.log(user.name);
  favMethod(user.age);
}

function checkIsAdult(age) {
  return age > 18;
}

function sayMyAge(age) {
  console.log(age);
}

unusualFunction(userV1, checkIsAdult);
unusualFunction(userV2, sayMyAge);
unusualFunction(userV2, function (a) {
  return a * a;
});
