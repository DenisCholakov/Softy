function focused() {
  Array.from(document.getElementsByTagName("input")).forEach((f) => {
    f.addEventListener(
      "focus",
      ({ target: { parentNode } }) => (parentNode.className = "focused")
    );
    f.addEventListener(
      "blur",
      ({ target: { parentNode } }) => (parentNode.className = "")
    );
  });
}
