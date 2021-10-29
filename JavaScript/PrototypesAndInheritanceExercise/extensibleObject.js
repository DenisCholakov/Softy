function extensibleObject() {
  return {
    extend(temp) {
      for (const key in temp) {
        if (typeof temp[key] === "function") {
          const prototype = Object.getPrototypeOf(this);
          prototype[key] = temp[key];
        } else {
          this[key] = temp[key];
        }
      }
    },
  };
}

const myObj = extensibleObject();

const template = {
  extensionMethod: function () {},
  extensionProperty: "someString",
};
myObj.extend(template);

console.log();
