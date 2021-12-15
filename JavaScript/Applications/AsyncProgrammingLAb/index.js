function selectText() {
  const input = document.getElementById("text-box");
  input.focus();
  input.setSelectionRange(0, 100);
}
