class List {
  constructor() {
    this._arr = [];
    this.size = 0;
  }

  add(element) {
    this._arr.push(element);
    this._arr.sort((a, b) => a - b);
    this.size++;
  }

  remove(index) {
    if (index < 0 || index > this._arr.length - 1) {
      throw new RangeError("index is out of range");
    }

    this._arr.splice(index, 1);
    this.size--;
  }

  get(index) {
    if (index < 0 || index > this._arr.length - 1) {
      throw new RangeError("index is out of range");
    }

    return this._arr[index];
  }
}
