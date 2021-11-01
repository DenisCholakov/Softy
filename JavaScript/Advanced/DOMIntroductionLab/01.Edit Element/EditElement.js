function editElement(el, match, replacer) {
  const content = el.textContent;
  const matcher = new RegExp(match, "g");
  const edited = content.replace(matcher, replacer);
  el.textContent = edited;
}
