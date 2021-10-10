function validate() {
  document.getElementById("email").addEventListener("change", onChange);

  function onChange({ target }) {
    ev.target.classList.add("error");
    const pattern = /^[a-z]+@[a-z]+\.[a-z]+$/;

    if (pattern.test(traget.value)) {
      target.classList.remove("error");
    } else {
      target.classList.add("error");
    }
  }
}
