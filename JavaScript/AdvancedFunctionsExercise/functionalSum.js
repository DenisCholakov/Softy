function add(num) {
  let sum = num;

  function addNums(num2) {
    sum += num2;
    return addNums;
  }

  addNums.toString = () => sum;

  return addNums;
}

console.log(add(3)(4).toString());
