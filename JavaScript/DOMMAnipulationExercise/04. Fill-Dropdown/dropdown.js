function addItem() {
  const dropdownMenu = document.getElementById("menu");
  const content = document.getElementById("newItemText");
  const value = document.getElementById("newItemValue");

  const option = document.createElement("option");

  option.textContent = content.value;
  option.value = value.value;

  dropdownMenu.appendChild(option);

  content.value = "";
  value.value = "";
}
