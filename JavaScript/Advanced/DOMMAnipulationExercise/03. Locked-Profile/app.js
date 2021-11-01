function lockedProfile() {
  Array.from(document.querySelectorAll(".profile button")).forEach((b) =>
    b.addEventListener("click", onToggle)
  );

  function onToggle(ev) {
    const profile = ev.target.parentElement;
    const isActive = profile.querySelector(
      'input[type="radio"][value="unlock"]'
    ).checked;

    if (isActive) {
      const infoDiv = Array.from(profile.querySelectorAll("div")).find((d) =>
        d.id.includes("HiddenFields")
      );

      if (ev.target.textContent === "Show more") {
        infoDiv.style.display = "block";
        ev.target.textContent = "Hide it";
      } else {
        infoDiv.style.display = "none";
        ev.target.textContent = "Show more";
      }
    }
  }

  Array.from(document.querySelectorAll('input[type="radio"]')).forEach(
    (r) => r.addEventListener("click", onLockToggle),
    onLockToggle
  );

  function onLockToggle(ev) {
    const button = ev.target.parentElement.querySelector("button");

    if (ev.target.value === "lock") {
      button.disabled = true;
    } else {
      button.disabled = false;
    }
  }
}
