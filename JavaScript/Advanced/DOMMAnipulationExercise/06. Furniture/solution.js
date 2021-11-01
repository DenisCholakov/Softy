function solve() {
  const [input, output] = Array.from(document.querySelectorAll("textarea"));
  const [generateBtn, buyBtn] = Array.from(document.querySelectorAll("button"));
  const table = document.querySelector("table.table tbody");

  generateBtn.addEventListener("click", generate);
  buyBtn.addEventListener("click", buy);

  function generate(ev) {
    const data = JSON.parse(input.value);

    for (const item of data) {
      const row = document.createElement("tr");

      row.appendChild(createCell("img", { src: item.img }));
      row.appendChild(createCell("p", {}, item.name));
      row.appendChild(createCell("p", {}, item.price));
      row.appendChild(createCell("p", {}, item.decFactor));
      row.appendChild(createCell("input", { type: "checkbox" }));

      table.appendChild(row);
    }
  }

  function createCell(nestedTag, props, content) {
    const cell = document.createElement("td");
    const nested = document.createElement(nestedTag);

    for (const prop in props) {
      nested[prop] = props[prop];
    }

    if (content) {
      nested.textContent = content;
    }

    cell.appendChild(nested);

    return cell;
  }

  function buy(ev) {
    const furniture = Array.from(
      document.querySelectorAll('input[type="checkbox"]:checked')
    )
      .map((b) => b.parentElement.parentElement)
      .map((r) => ({
        name: r.children[1].textContent,
        price: Number(r.children[2].textContent),
        decFactor: Number(r.children[3].textContent),
      }))
      .reduce(
        (acc, curr, i, arr) => {
          acc.names.push(curr.name);
          acc.total += curr.price;
          acc.decFactor += curr.decFactor / arr.length;
          return acc;
        },
        { names: [], total: 0, decFactor: 0 }
      );

    const result = `Bought furniture: ${furniture.names.join(", ")}
Total price: ${furniture.total.toFixed(2)}
Average decoration factor: ${furniture.decFactor}`;

    output.value = result;
  }
}
