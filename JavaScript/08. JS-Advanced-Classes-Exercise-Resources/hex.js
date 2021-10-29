class Hex {
  constructor(value) {
    this.value = value;
  }

  valueOf() {
    return this.value;
  }

  toString() {
    return "0x" + this.value.toString(16).toUpperCase();
  }

  plus(input) {
    if (typeof input === "number") {
      return new Hex(this.value + input);
    } else {
      return new Hex(this.value + input.value);
    }
  }

  minus(input) {
    if (typeof input === "number") {
      return new Hex(this.value - input);
    } else {
      return new Hex(this.value - input.value);
    }
  }

  parse(hexNum) {
    return parseInt(hexNum, 16);
  }
}

let FF = new Hex(255);

console.log(FF.toString());
let exp = "0xFF";

console.log(FF.valueOf() - 1 == 254);
let a = new Hex(10);
let b = new Hex(5);
let c = new Hex(155);
console.log(a.plus(c).toString());
let exp2 = "0xA5";

console.log(a.minus(b).toString());
let exp3 = "0x5";
