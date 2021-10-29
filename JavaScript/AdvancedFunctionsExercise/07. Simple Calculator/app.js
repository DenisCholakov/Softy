function calculator() {
  let fisrtInput;
  let secondInput;
  let resultBox;

  function init(selector1, selector2, selector3) {
    fisrtInput = document.querySelector(selector1);
    secondInput = document.querySelector(selector2);
    resultBox = document.querySelector(selector3);
  }

  function add() {
    resultBox.value = Number(fisrtInput.value) + Number(secondInput.value);
  }

  function subtract() {
    resultBox.value = Number(fisrtInput.value) - Number(secondInput.value);
  }

  return {
    init,
    add,
    subtract,
  };
}

const calculate = calculator();
calculate.init("#num1", "#num2", "#result");
