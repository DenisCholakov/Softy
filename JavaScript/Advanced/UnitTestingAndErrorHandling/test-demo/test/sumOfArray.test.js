const { expect } = require("chai");
const sum = require("../sumOfArray");

describe("Array Sum function Test", () => {
  it("works with number array", () => {
    expect(sum([1, 2, 3])).to.equal(6);
  });

  it("wokrs with string numbers", () => {
    expect(sum(["1", "2", "3"])).to.equal(6);
  });

  it("returns NaN when passed string", () => {
    expect(sum("asdasd")).to.be.NaN;
  });

  it("returns Nan when passigng invalid array", () => {
    expect(sum([1, "two", 3])).to.be.NaN;
  });
});
