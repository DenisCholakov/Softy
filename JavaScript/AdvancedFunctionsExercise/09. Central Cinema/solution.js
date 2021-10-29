function solve() {
  const [name, hall, ticketPrice] =
    document.querySelectorAll("#container input");
  const movieSection = document.querySelector("#movies ul");
  const archiveSection = document.querySelector("#archive ul");
  const clearButton = document.querySelector("#archive button");
  const onScreenBtn = document.querySelector("#container button");
  onScreenBtn.addEventListener("click", addMovie);

  clearButton.addEventListener("click", (ev) => {
    Array.from(archiveSection.children).forEach((x) => x.remove());
  });

  function addMovie(ev) {
    ev.preventDefault();
    if (
      name.value !== "" &&
      hall.value !== "" &&
      !isNaN(Number(ticketPrice.value))
    ) {
      const movie = document.createElement("li");
      movie.innerHTML = `<span>${name.value}</span>
                <strong>Hall: ${hall.value}</strong>
                <div>
                    <strong>${Number(ticketPrice.value).toFixed(2)}</strong>
                    <input placeholder="Tickets Sold"></input>
                    <button>Archive</button>
                </div>`;
      movieSection.appendChild(movie);

      const button = movie.querySelector("div button");
      button.addEventListener("click", addToArchive);

      name.value = "";
      hall.value = "";
      ticketPrice.value = "";
    }
  }

  function addToArchive(ev) {
    const inputValue = ev.target.parentElement.querySelector("input").value;
    const ticketPrice =
      ev.target.parentElement.querySelector("strong").textContent;

    if (!isNaN(Number(inputValue))) {
      const movieName =
        ev.target.parentElement.parentElement.querySelector("span").textContent;

      const income = Number(inputValue) * Number(ticketPrice);
      const liEl = document.createElement("li");

      liEl.innerHTML = `<span>${movieName}</span><strong>Total amount: ${income.toFixed(
        2
      )}</strong><button>Delete</button>`;

      const button = liEl.querySelector("button");
      button.addEventListener("click", (ev) =>
        ev.target.parentElement.parentElement.remove()
      );

      ev.target.parentElement.parentElement.remove();
      archiveSection.appendChild(liEl);
    }
  }
}
