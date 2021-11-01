function solve() {
  document.querySelector("#searchBtn").addEventListener("click", onClick);

  function onClick() {
    const searchField = document.getElementById("searchField");
    const tableElements = Array.from(document.querySelectorAll("tbody tr"));

    tableElements.forEach((el) => {
      const text = el.textContent.toLowerCase();

      if (text.includes(searchField.value.toLowerCase())) {
        el.classList.add("select");
      } else {
        el.classList.remove("select");
      }
    });

    searchField.value = "";
  }
}
