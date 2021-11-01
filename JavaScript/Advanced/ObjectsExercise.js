function calorieObject(arr) {
    const result = {};

    for (let i = 0; i < arr.length; i += 2) {
        result[arr[i]] = Number(arr[i+1]);
    }

    console.log(result);
}

function constructionCrew(worker) {
    if (worker.dizziness) {
        worker.levelOfHydrated += 0.1 * worker.weight * worker.experience;
        worker.dizziness = false;
    }

    return worker;
}

function carFactory(carOrder) {
    function getEngine(minPower) {
        if (minPower <= 90) {
            return { power: 90, volume: 1800 };
        } else if (minPower <= 120) {
            return { power: 120, volume: 2400 };
        } else {
            return { power: 200, volume: 3500 };
        }
    }

    const wheelSize = carOrder.wheelsize % 2 === 0 ? carOrder.wheelsize - 1 : carOrder.wheelsize;

    const result = {
        model: carOrder.model,
        engine: getEngine(carOrder.power),
        carriage: { type: carOrder.carriage, color: carOrder.color },
        wheels: new Array(4).fill(wheelSize, 0, 4)
    }

    return result;
}

function heroicInventory(arr) {
    const result = [];

    arr.forEach((el) => {
        let [name, level, items] = el.split(" / ");
        result.push({
            name,
            level: Number(level),
            items: items ? items.split(', ') : []
        });
    })

    // JSON.parse()
    return JSON.stringify(result);
}

function lowestPricesInCities(products) {
    let catalogue = {};

    products.forEach((el) => {
        let [town, product, price] = el.split(" | ");
        price = Number(price);

        if (!catalogue[product]) {
            catalogue[product] = {};
        }

        catalogue[product][town] = price;
    });

    for (const product in catalogue) {
        let sorted = Object.entries(catalogue[product]).sort((a, b) => a[1] - b[1]);
        console.log(`${product} -> ${sorted[0][1]} (${sorted[0][0]})`)
    }
}

function storeCatalogue(products) {
    const catalogue = {};

    products.forEach((el) => {
        let [product, price] = el.split(' : ');
        price = Number(price);
        const index = product[0];

        if (!catalogue[index]) {
            catalogue[index] = [];
        }

        catalogue[index].push({ product, price })
    });

    let indexes = Object.keys(catalogue).sort();

    for (const index of indexes) {
        console.log(index);
        let indexProducts = catalogue[index].sort((a, b) => a.product.localeCompare(b.product));
        for (const product of indexProducts) {
            console.log(`  ${product.product}: ${product.price}`)
        }
    }
}

function townsToJSON(towns) {
    const townObjects = [];
    const propertyNames = parsePropertyNames(towns[0]);

    for (let i = 1; i < towns.length; i++) {
        townObjects.push(parseRow(towns[i], propertyNames))
    }

    console.log(JSON.stringify(townObjects));

    function parsePropertyNames(row) {
        let [ town, latitude, longitude ] = row
            .split('|')
            .map(el => el.trim())
            .filter(el => el !== '');

        return {town, latitude, longitude}
    }

    function parseRow(townStr, propertyNames) {
        const { town, latitude, longitude } = propertyNames;
        let [ townVal, latitudeVal, longitudeVal ] = townStr
            .split('|')
            .map(el => el.trim())
            .filter(el => el !== '');

        const result = {};

        result[town] = townVal;
        result[latitude] = Math.round(latitudeVal * 100) / 100;
        result[longitude] = Math.round(longitudeVal * 100) / 100;

        return result;
    }
}

function rectangle(width, height, color) {
    return {
        width,
        height,
        color: color[0].toUpperCase() + color.slice(1),
        calcArea() {
            return this.height * this.width;
        }
    };
}

function createSortedList() {
    const list = [];
    return {
        add(num) {
            if (typeof(num) === 'number') {
                list.push(num);
                list.sort((a, b) => a - b);
            } else {
                console.log('The array takes only numbers.')
            }
        },
        remove(index) {
            if (index >= 0 && index < list.length) {
                list.splice(index, 1);
            } else {
                console.error('The index is outside the bounds of the array');
            }
        },
        get(index) {
            if (index >= 0 && index < list.length) {
                return list[index];
            } else {
                console.error('The index is outside the bounds of the array');
            }
        },
        get size() {
            return list.length
        }
    }
}

function solve() {
    return {
        fighter(name) {
            return {
                name,
                health: 100,
                stamina: 100,
                fight() {
                    console.log(`${this.name} slashes at the foe!`);
                    this.stamina--;
                }
            }
        },
        mage(name) {
            return {
                name,
                health: 100,
                mana: 100,
                cast(spell) {
                    console.log(`${name} cast ${spell}`);
                    this.mana--;
                }
            }
        }
    }
}

function janNotation(operations) {
    const result = [];
    const parser = {};
    let operandsFlag = false;

    parser['+'] = (a, b) => a + b;
    parser['-'] = (a, b) => b - a;
    parser['*'] = (a, b) => a * b;
    parser['/'] = (a, b) => b / a;

    operations.forEach((el) => {
        if (Number.isInteger(el)) {
            result.push(el);
        } else {
            if (result.length >= 2) {
                result.push(parser[el](result.pop(), result.pop()))
            } else {
                console.log("Error: not enough operands!");
                operandsFlag = true;
            }
        }
    });

    if (!operandsFlag) {
        if (result.length === 1) {
            console.log(result[0])
        } else {
            console.log("Error: too many operands!")
        }
    }
}