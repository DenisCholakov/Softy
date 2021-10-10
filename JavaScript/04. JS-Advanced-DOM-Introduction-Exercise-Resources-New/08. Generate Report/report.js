function generateReport() {
  const output = document.getElementById("output");
  const boxes = document.querySelectorAll("thead tr th input");
  const info = document.querySelectorAll("tbody tr");
  const result = [];

  for (let i = 0; i < info.length; i++) {
    let report = {};

    for (let j = 0; j < boxes.length; j++) {
      if (boxes[j].checked) {
        report[boxes[j].name] = info[i].children[j].textContent;
      }
    }

    result.push(report);
  }

  output.textContent = JSON.stringify(result);
}
