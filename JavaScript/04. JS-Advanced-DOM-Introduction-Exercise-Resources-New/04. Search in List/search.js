function search() {
  const elements = Array.from(document.querySelectorAll("ul li"));
  const text = document.getElementById("searchText").value;
  const result = document.getElementById("result");
  let matchesCount = 0;

  elements.forEach((el) => {
    if (el.textContent.includes(text)) {
      el.style.fontWeight = "bold";
      el.style.textDecoration = "underline";
      matchesCount++;
    } else {
      el.style.fontWeight = "normal";
      el.style.textDecoration = "";
    }
  });

  result.textContent = `${matchesCount} matches found`;
}
