function solve() {
  const result = document.getElementById("result");
  const button = document.querySelector("button");
  const input = document.getElementById("input");
  const convertToMenu = document.getElementById("selectMenuTo");
  const binaryOption = document.createElement("option");
  const hexadecimalOption = document.createElement("option");
  binaryOption.value = "binary";
  hexadecimalOption.value = "hexadecimal";
  binaryOption.textContent = "Binary";
  hexadecimalOption.textContent = "Hexadecimal";

  convertToMenu.appendChild(binaryOption);
  convertToMenu.appendChild(hexadecimalOption);

  button.addEventListener("click", convertNumber);

  function convertNumber() {
    inputValue = Number(input.value);

    if (Number.isNaN(inputValue) || convertToMenu.value === "") {
      return;
    }

    if (convertToMenu.value === "binary") {
      result.value = inputValue.toString(2);
    } else {
      result.value = inputValue.toString(16).toUpperCase();
    }
  }
}
