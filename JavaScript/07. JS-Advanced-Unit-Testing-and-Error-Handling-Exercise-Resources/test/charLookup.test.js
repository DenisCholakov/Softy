const { expect } = require("chai");
const lookupChar = require("../charLookup");

describe("lookupChar Test", () => {
  let text = "";

  beforeEach(() => {
    text = "Denis";
  });

  it("returns the char that is on the given index in the given string", () => {
    expect(lookupChar(text, 1)).to.equal("e");
  });

  it("returns the char that is on the given index in the given string", () => {
    expect(lookupChar(text, 2)).to.equal("n");
  });

  it("returns Incorrect index if the index is not valid", () => {
    expect(lookupChar(text, -1)).to.equal("Incorrect index");
    expect(lookupChar(text, text.length)).to.equal("Incorrect index");
  });

  it("returns udefined if second parameter is not an integer", () => {
    expect(lookupChar(text, "2")).to.be.undefined;
    expect(lookupChar(text, 2.1)).to.be.undefined;
    expect(lookupChar(text, {})).to.be.undefined;
    expect(lookupChar(text)).to.be.undefined;
  });

  it("returns undefined if the first parameter is not a string", () => {
    expect(lookupChar(2, 2)).to.be.undefined;
    expect(lookupChar({}, 1)).to.be.undefined;
    expect(lookupChar([], 2)).to.be.undefined;
  });
});
