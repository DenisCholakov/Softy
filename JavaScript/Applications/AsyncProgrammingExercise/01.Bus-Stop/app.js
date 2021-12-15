async function getInfo() {
  const stopId = document.getElementById("stopId").value;
  const stopNameField = document.getElementById("stopName");
  const busesUl = document.getElementById("buses");
  const checkBtn = document.getElementById("submit");

  try {
    stopNameField.textContent = "Loading";
    busesUl.replaceChildren();
    checkBtn.disabled = true;

    const res = await fetch(
      `http://localhost:3030/jsonstore/bus/businfo/${stopId}`
    );

    checkBtn.disabled = false;

    if (res.status !== 200) {
      throw new Error("Stop Id not found.");
    }

    const data = await res.json();

    stopNameField.textContent = data.name;

    Object.entries(data.buses).forEach((b) => {
      const busLi = document.createElement("li");
      busLi.textContent = `Bus ${b[0]} arrives in ${b[1]} minutes`;
      busesUl.appendChild(busLi);
    });
  } catch (error) {
    stopNameField.textContent = "Error";
  }
}
