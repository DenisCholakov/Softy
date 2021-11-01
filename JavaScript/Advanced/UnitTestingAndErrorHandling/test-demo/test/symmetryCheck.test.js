const { expect } = require("chai");
const { isSymmetric } = require("../SymmetryCheck");

describe("Symmetry Test", () => {
  it("returns true with symmetric array", () => {
    expect(isSymmetric([2, 1, 1, 2])).to.be.true;
  });

  it("returns truw when given array with not even count of elements", () => {
    expect(isSymmetric([1, 2, 1])).to.be.true;
  });

  it("returns true for symmetric array of strings", () => {
    expect(isSymmetric(["a", "b", "a"])).to.be.true;
  });

  it("returns false for non-summetric array", () => {
    expect(isSymmetric([1, 2, 2])).to.be.false;
  });

  it("returns false if input is single number", () => {
    expect(isSymmetric(1)).to.be.false;
  });

  it("returns false if input is symmetric string", () => {
    expect(isSymmetric("asddsa")).to.be.false;
  });

  it("returns true for non-symmetric array of strings", () => {
    expect(isSymmetric(["a", "b", "c", "a"])).to.be.false;
  });

  it("returns false for type different symmetric array", () => {
    expect(isSymmetric([1, 2, "2", 1])).to.be.false;
  });
});
