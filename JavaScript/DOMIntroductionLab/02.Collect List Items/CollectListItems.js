function extractText() {
  const text = document.getElementById("items").textContent;
  const result = document.getElementById("result");

  result.value = text;
}
