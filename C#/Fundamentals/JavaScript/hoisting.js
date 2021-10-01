//==================hoisting======================
"use strict";

var myName = "Denis Cholakov";
console.log(myName.length);

myName = 21312;
let a;
console.log(a); //undefined

// в повечето случаи ползваме let и const
// var и let само в нашата функция. Ако няма нищо, променливата е глобална. (област на видимост)

function test() {
  let a = 100;

  function best() {
    a = 100 + a;
    console.log(a);
  }
}

//  не се препоръчва
var arr = new Array();
arr.push(10);
arr.push(25);
arr.push("Kiril");
console.log(arr.length);

// с литерали
let cars = ["Ford", "BMW", "Peugeot"];
var firstItem = cars[0];

var obj = {
  prop: 1,
  prop2: "string",
  prop3: function () {},
};

// при срвняване с == JS прави имплицитно преобразуване, а === не го прави, същото важи и за !=

var someValue;

if (someValue) {
}

// !! for bool transfromation

var obj = {
  func: function test() {},
};
