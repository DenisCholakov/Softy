function solve(arr, command) {
  if (command === "desc") {
    return arr.sort((a, b) => b - a);
  } else {
    return arr.sort((a, b) => a - b);
  }
}
