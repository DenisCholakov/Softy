function validate() {
  const validEmailPattern = /^[a-z]+@[a-z]+\.[a-z]+$/;
  const inputField = document.getElementById("email");
  inputField.addEventListener("change", () => {
    if (!validEmailPattern.test(inputField.value)) {
      inputField.classList.add("error");
    } else {
      inputField.classList.remove("error");
    }
  });
}
