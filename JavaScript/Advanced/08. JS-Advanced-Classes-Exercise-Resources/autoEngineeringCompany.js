function solve(input) {
  const carBrands = new Map();

  input.forEach((x) => {
    let [brand, model, producedCars] = x.split(" | ");
    producedCars = Number(producedCars);

    if (!carBrands.has(brand)) {
      carBrands.set(brand, new Map());
    }

    let carBrand = carBrands.get(brand);

    if (!carBrand.has(model)) {
      carBrand.set(model, 0);
    }

    let carsCount = carBrand.get(model);
    carBrand.set(model, carsCount + producedCars);
  });

  for (const [brandKey, brand] of carBrands) {
    console.log(brandKey);

    for (const [model, count] of brand) {
      console.log(`###${model} -> ${count}`);
    }
  }
}

solve([
  "Audi | Q7 | 1000",
  "Audi | Q6 | 100",
  "BMW | X5 | 1000",
  "BMW | X6 | 100",
  "Citroen | C4 | 123",
  "Volga | GAZ-24 | 1000000",
  "Lada | Niva | 1000000",
  "Lada | Jigula | 1000000",
  "Citroen | C4 | 22",
  "Citroen | C5 | 10",
]);
