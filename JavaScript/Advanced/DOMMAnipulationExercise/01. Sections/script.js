function create(words) {
  const content = document.getElementById("content");
  content.addEventListener("click", reveal);

  for (const word of words) {
    const divElement = document.createElement("div");
    const paraElement = document.createElement("p");

    paraElement.textContent = word;
    paraElement.style.display = "none";
    divElement.appendChild(paraElement);

    content.appendChild(divElement);
  }

  function reveal(ev) {
    if (ev.target.tagName === "DIV" && ev.target !== content) {
      ev.target.firstChild.style.display = "";
    }
  }
}
