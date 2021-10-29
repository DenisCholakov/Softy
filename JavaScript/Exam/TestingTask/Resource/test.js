const { expect } = require("chai");
const library = require("./library");

describe("Library tets", () => {
  describe("calcPriceOfBook Tests", () => {
    it("standart price is 20 BGN", () => {
      expect(library.calcPriceOfBook("Harry Potter", 2000)).to.equal(
        "Price of Harry Potter is 20.00"
      );
    });

    it("if book is older than 1980 price is half", () => {
      expect(library.calcPriceOfBook("Harry Potter", 1970)).to.equal(
        "Price of Harry Potter is 10.00"
      );
    });

    it("if book is produced in 1980 year price is half", () => {
      expect(library.calcPriceOfBook("Harry Potter", 1980)).to.equal(
        "Price of Harry Potter is 10.00"
      );
    });

    it("if bookName is not a string should throw error", () => {
      expect(() => library.calcPriceOfBook(["h", "a"], 400)).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook(2, 400)).to.throw("Invalid input");
      expect(() => library.calcPriceOfBook(undefined, 400)).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook(null, 400)).to.throw(
        "Invalid input"
      );
    });

    it("if year of production is not integer shoul theo error", () => {
      expect(() => library.calcPriceOfBook("Harry Potter", "2000")).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook("Harry Potter", [1])).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook("Harry Potter", 2.5)).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook("Harry Potter")).to.throw(
        "Invalid input"
      );
      expect(() => library.calcPriceOfBook("Harry Potter", null)).to.throw(
        "Invalid input"
      );
    });
  });

  describe("findBook Test", () => {
    it("should return success messsage if desired book is present", () => {
      expect(
        library.findBook(["Harry Potter", "Eragon"], "Harry Potter")
      ).to.equal("We found the book you want.");
    });

    it("should return not found message if desired book is not present", () => {
      expect(library.findBook(["Eragon"], "Harry Potter")).to.equal(
        "The book you are looking for is not here!"
      );
    });

    it("if books array is empty throws error", () => {
      expect(() => library.findBook([], "My Book")).to.throw(
        "No books currently available"
      );

      expect(() => library.findBook([])).to.throw(
        "No books currently available"
      );
    });
  });

  describe("arrangeTheBooks Test", () => {
    it("shopuld arrange books for calid count of books", () => {
      expect(library.arrangeTheBooks(0)).to.equal(
        "Great job, the books are arranged."
      );
      expect(library.arrangeTheBooks(1)).to.equal(
        "Great job, the books are arranged."
      );
      expect(library.arrangeTheBooks(5 * 8)).to.equal(
        "Great job, the books are arranged."
      );
      expect(library.arrangeTheBooks(5)).to.equal(
        "Great job, the books are arranged."
      );
    });

    it("should throw error if count of books is not integer or is negative number", () => {
      expect(() => library.arrangeTheBooks(-1)).to.throw("Invalid input");
      expect(() => library.arrangeTheBooks(-1.5)).to.throw("Invalid input");
      expect(() => library.arrangeTheBooks(1.5)).to.throw("Invalid input");
      expect(() => library.arrangeTheBooks("1")).to.throw("Invalid input");
      expect(() => library.arrangeTheBooks(undefined)).to.throw(
        "Invalid input"
      );
    });

    it("should return error message if the count of books is bigger than the storage", () => {
      expect(library.arrangeTheBooks(5 * 8 + 1)).to.equal(
        "Insufficient space, more shelves need to be purchased."
      );
    });
  });
});
