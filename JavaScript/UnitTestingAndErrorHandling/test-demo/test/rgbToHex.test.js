const { expect } = require("chai");
const rgbToHexColor = require("../rgbToHex");

describe("RGB Converter Tests", () => {
  it("converts white", () => {
    expect(rgbToHexColor(255, 255, 255)).to.equal("#FFFFFF");
  });

  it("converts black", () => {
    expect(rgbToHexColor(0, 0, 0)).to.equal("#000000");
  });

  it("converts softuni dark blue", () => {
    expect(rgbToHexColor(35, 68, 101)).to.equal("#234465");
  });

  it("return undefined for missing parameters", () => {
    expect(rgbToHexColor(35, 68)).to.be.undefined;
  });

  it("return undefined for invalid parameters", () => {
    expect(rgbToHexColor(35, 68, "255")).to.be.undefined;
  });

  it("return undefined for values out of range", () => {
    expect(rgbToHexColor(-1, -1, -1)).to.be.undefined;
    expect(rgbToHexColor(-1, 0, 0)).to.be.undefined;
    expect(rgbToHexColor(0, -1, 0)).to.be.undefined;
    expect(rgbToHexColor(0, 0, -1)).to.be.undefined;
  });

  it("return undefined for values out of range", () => {
    expect(rgbToHexColor(256, 256, 256)).to.be.undefined;
    expect(rgbToHexColor(256, 255, 255)).to.be.undefined;
    expect(rgbToHexColor(255, 256, 255)).to.be.undefined;
    expect(rgbToHexColor(255, 255, 256)).to.be.undefined;
  });
});
