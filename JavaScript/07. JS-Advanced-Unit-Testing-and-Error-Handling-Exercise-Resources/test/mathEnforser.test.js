const { expect } = require("chai");
const mathEnforcer = require("../mathEnforser");

describe("mathEnforser Test", () => {
  it("Test if has all functions", () => {
    expect(mathEnforcer).has.ownProperty("addFive");
    expect(mathEnforcer).has.ownProperty("subtractTen");
    expect(mathEnforcer).has.ownProperty("sum");
  });

  it("Expect addFive to work properly", () => {
    expect(mathEnforcer.addFive(1)).to.equal(6);
    expect(mathEnforcer.addFive(-1)).to.equal(4);
    expect(mathEnforcer.addFive(3.2)).to.equal(3.2 + 5);
    expect(mathEnforcer.addFive(NaN)).to.be.NaN;
  });

  it("Expect subtractTen to work properly", () => {
    expect(mathEnforcer.subtractTen(9)).to.equal(-1);
    expect(mathEnforcer.subtractTen(-1)).to.equal(-11);
    expect(mathEnforcer.subtractTen(1.5)).to.equal(1.5 - 10);
    expect(mathEnforcer.subtractTen(NaN)).to.be.NaN;
  });

  it("Expect sum to work properly", () => {
    expect(mathEnforcer.sum(2, 3)).to.equal(5);
    expect(mathEnforcer.sum(-2, 3)).to.equal(1);
    expect(mathEnforcer.sum(2, -3)).to.equal(-1);
    expect(mathEnforcer.sum(-2, -3)).to.equal(-5);
    expect(mathEnforcer.sum(2.1, 3.2)).to.equal(2.1 + 3.2);
    expect(mathEnforcer.sum(NaN, 10)).to.be.NaN;
  });

  it("Expect undefined on wrong input type on addFive", () => {
    expect(mathEnforcer.addFive("5")).to.be.undefined;
    expect(mathEnforcer.addFive("asdasd")).to.be.undefined;
    expect(mathEnforcer.subtractTen([5])).to.be.undefined;
    expect(mathEnforcer.addFive([])).to.be.undefined;
    expect(mathEnforcer.addFive({})).to.be.undefined;
  });

  it("Expect undefined on wrong input type on subtractTen", () => {
    expect(mathEnforcer.subtractTen("5")).to.be.undefined;
    expect(mathEnforcer.subtractTen("asdasd")).to.be.undefined;
    expect(mathEnforcer.subtractTen([5])).to.be.undefined;
    expect(mathEnforcer.subtractTen([])).to.be.undefined;
    expect(mathEnforcer.subtractTen({})).to.be.undefined;
  });

  it("Expect undefined on wrong input type on sum", () => {
    expect(mathEnforcer.sum("5", 2)).to.be.undefined;
    expect(mathEnforcer.sum("sadasda", "sadasd")).to.be.undefined;
    expect(mathEnforcer.sum(1, [])).to.be.undefined;
    expect(mathEnforcer.sum({})).to.be.undefined;
    expect(mathEnforcer.sum([10, 10])).to.be.undefined;
  });
});
