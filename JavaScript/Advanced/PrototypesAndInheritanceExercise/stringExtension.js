(function solve() {
  String.prototype.ensureStart = function (str) {
    if (this.startsWith(str)) {
      return this.toString();
    } else {
      return str + this;
    }
  };

  String.prototype.ensureEnd = function (str) {
    if (this.endsWith(str)) {
      return this.toString();
    } else {
      return this + str;
    }
  };

  String.prototype.isEmpty = function () {
    return this.length === 0;
  };

  String.prototype.truncate = function (n) {
    if (this.length < n) {
      return this.toString();
    }

    if (n < 4) {
      return "...".substring(0, n);
    }

    let truncated = this.substring(0, n - 3);
    let spaceIndex = truncated.lastIndexOf(" ");

    if (spaceIndex === -1) {
      return truncated + "...";
    } else {
      return truncated.substring(0, spaceIndex) + "...";
    }
  };

  String.format = function (str, ...params) {
    let result = str;
    for (let i = 0; i < params.length; i++) {
      result = result.replace(`{${i}}`, params[i]);
    }

    return result;
  };
})();

let str = "my string";
str = str.ensureStart("my");
str = str.ensureStart("hello ");
str = str.truncate(16);
str = str.truncate(14);
str = str.truncate(8);
str = str.truncate(4);
str = str.truncate(2);
str = String.format("The {0} {1} fox", "quick", "brown");
str = String.format("jumps {0} {1}", "dog");
