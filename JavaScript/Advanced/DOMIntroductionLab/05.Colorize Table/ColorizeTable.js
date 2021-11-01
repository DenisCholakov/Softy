function colorize() {
  const rows = Array.from(document.querySelectorAll("table tr"));

  for (let i = 1; i < rows.length; i += 2) {
    rows[i].style.backgroundColor = "Teal";
  }

  console.log(rows);
}
