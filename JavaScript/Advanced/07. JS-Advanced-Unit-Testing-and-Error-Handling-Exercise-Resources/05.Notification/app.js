function notify(message) {
  const resultDiv = document.getElementById("notification");
  resultDiv.style.display = "block";
  resultDiv.textContent = message;

  resultDiv.addEventListener("click", () => {
    resultDiv.style.display = "none";
  });
}
