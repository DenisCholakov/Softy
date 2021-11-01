function toggle() {
  const extraText = document.getElementById("extra");
  const button = document.querySelector(".button");

  button.textContent = button.textContent == "More" ? "Less" : "More";
  extraText.style.display = button.textContent == "More" ? "none" : "block";
}
