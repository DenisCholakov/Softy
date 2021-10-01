function solve() {
  const textInput = document.getElementById("input").value;
  const splittedText = textInput.split(".").filter((x) => x !== "");
  const output = document.getElementById("output");
  let paragraph = "<p>";
  let result = "";

  for (let i = 0; i < splittedText.length; i++) {
    paragraph += splittedText[i] + ".";

    if ((i + 1) % 3 === 0) {
      result += paragraph + "</p>";
      paragraph = "<p>";
    }
  }

  if (paragraph !== "<p>") {
    result += paragraph + "</p>";
  }

  output.innerHTML = result;
}
