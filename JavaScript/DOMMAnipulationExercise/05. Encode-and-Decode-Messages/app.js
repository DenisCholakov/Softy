function encodeAndDecodeMessages() {
  const main = document.getElementById("main");
  main.addEventListener("click", processMessage);

  function processMessage(ev) {
    if (ev.target.tagName !== "BUTTON") {
      return;
    }

    const decodedMessageField = document.querySelector("textarea");
    const encodedMessageField = document.querySelector("textarea:disabled");

    if (ev.target.textContent.includes("Encode")) {
      const message = decodedMessageField.value;
      let encoded = [];

      for (let i = 0; i < message.length; i++) {
        const currSymbol = message.charCodeAt(i);
        encoded.push(String.fromCharCode(currSymbol + 1));
      }

      decodedMessageField.value = "";
      encodedMessageField.value = encoded.join("");
    } else if (ev.target.textContent.includes("Decode")) {
      const message = encodedMessageField.value;
      let decoded = [];

      for (let i = 0; i < message.length; i++) {
        const currSymbol = message.charCodeAt(i);
        decoded.push(String.fromCharCode(currSymbol - 1));
      }

      encodedMessageField.value = decoded.join("");
    }
  }
}
