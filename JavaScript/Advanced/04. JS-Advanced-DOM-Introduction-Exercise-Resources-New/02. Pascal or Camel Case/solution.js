function solve() {
  const text = document.getElementById("text").value;
  const naming = document.getElementById("naming-convention").value;
  const result = document.getElementById("result");

  const splitted = text.split(" ");

  let resultString = "";

  if (naming === "Pascal Case") {
    for (let i = 0; i < splitted.length; i++) {
      resultString +=
        splitted[i][0].toUpperCase() + splitted[i].slice(1).toLowerCase();
    }
  } else if (naming === "Camel Case") {
    resultString +=
      splitted[0][0].toLowerCase() + splitted[0].slice(1).toLowerCase();
    for (let i = 1; i < splitted.length; i++) {
      resultString +=
        splitted[i][0].toUpperCase() + splitted[i].slice(1).toLowerCase();
    }
  } else {
    resultString = "Error!";
  }

  result.textContent = resultString;
}
