function getFibonator() {
  let n1 = 0;
  let n2 = 1;

  function getNumber() {
    let next = n1 + n2;
    n1 = n2;
    n2 = next;
    return next;
  }

  return getNumber;
}
