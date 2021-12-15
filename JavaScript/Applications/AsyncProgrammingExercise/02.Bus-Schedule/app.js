function solve() {
  const label = document.querySelector("#info span");
  const departBtn = document.getElementById("depart");
  const arriveBtn = document.getElementById("arrive");

  let stop = {
    next: "depot",
  };

  async function depart() {
    try {
      const url = `http://localhost:3030/jsonstore/bus/schedule/${stop.next}`;

      departBtn.disabled = true;

      const res = await fetch(url);

      if (res.status !== 200) {
        throw new Error("Nex stop is not avaliable");
      }

      stop = await res.json();

      label.textContent = `Next stop ${stop.name}`;

      arriveBtn.disabled = false;
    } catch (error) {
      label.textContent = error.message;
    }
  }

  function arrive() {
    arriveBtn.disabled = true;

    label.textContent = `Arriving at ${stop.name}`;

    departBtn.disabled = false;
  }

  return {
    depart,
    arrive,
  };
}

let result = solve();
