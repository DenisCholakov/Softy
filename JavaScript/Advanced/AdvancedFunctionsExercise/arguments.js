function solve(...args) {
  const types = {};

  for (const arg of args) {
    const type = typeof arg;

    if (!types[type]) {
      types[type] = 0;
    }

    types[type]++;

    console.log(`${type}: ${arg}`);
  }

  const keys = Object.keys(types).sort((a, b) => types[b] - types[a]);

  for (const key of keys) {
    console.log(`${key} = ${types[key]}`);
  }
}

solve({ name: "bob" }, 3.333, 9.999);
