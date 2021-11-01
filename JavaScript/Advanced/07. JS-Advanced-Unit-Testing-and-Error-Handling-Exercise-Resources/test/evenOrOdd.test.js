const { expect } = require("chai");
const isOddOrEven = require("../evenOrOdd");

describe("Odd or Even Test", () => {
  it("return undefined when no string is passed", () => {
    expect(isOddOrEven(2)).to.be.undefined;
    expect(isOddOrEven({})).to.be.undefined;
    expect(isOddOrEven([])).to.be.undefined;
  });

  it("returns even when string length is even", () => {
    expect(isOddOrEven("aabb")).to.equal("even");
  });

  it("returns odd when string length is odd", () => {
    expect(isOddOrEven("aab")).to.equal("odd");
  });

  it("returns even for empty string", () => {
    expect(isOddOrEven("")).to.equal("even");
  });
});
