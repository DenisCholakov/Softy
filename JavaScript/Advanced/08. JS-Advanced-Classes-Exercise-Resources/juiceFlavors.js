function produceBottles(juices) {
  const juicesObj = {};
  const bottles = {};
  juices.forEach((x) => {
    const currJuice = x.split(" => ");

    if (!juicesObj[currJuice[0]]) {
      juicesObj[currJuice[0]] = 0;
    }

    juicesObj[currJuice[0]] += Number(currJuice[1]);

    if (juicesObj[currJuice[0]] >= 1000) {
      if (!bottles[currJuice[0]]) {
        bottles[currJuice[0]] = 0;
      }

      bottles[currJuice[0]] += Math.floor(juicesObj[currJuice[0]] / 1000);
      juicesObj[currJuice[0]] %= 1000;
    }
  });

  const result = [];

  for (const key in bottles) {
    result.push(`${key} => ${bottles[key]}`);
  }

  return result.join("\n");
}

console.log(
  produceBottles([
    "Orange => 2000",
    "Peach => 1432",
    "Banana => 450",
    "Peach => 600",
    "Strawberry => 549",
  ])
);

console.log(
  produceBottles([
    "Kiwi => 234",
    "Pear => 2345",
    "Watermelon => 3456",
    "Kiwi => 4567",
    "Pear => 5678",
    "Watermelon => 6789",
  ])
);
