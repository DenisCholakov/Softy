function cityRecord(name, population, treasury) {
    const result = {
        name: name,
        population: population,
        treasury: treasury
    };

    return result;
}

function townPopulation(townsAsStrings) {
    const towns = {};

    for (const data of townsAsStrings) {
        const tokens = data.split(' <-> ');
        const name = tokens[0];
        let population = Number(tokens[1]);

        if (towns[name] == undefined) {
            towns[name] = population;
        } else {
            towns[name] += population;
        }
    }

    for (const name in towns) {
        console.log(`${name} : ${towns[name]}`)
    }
}

function cityTaxes(name, population, treasury) {
    const result = {
        name: name,
        population: population,
        treasury: treasury,
        taxRate: 10,
        collectTaxes() {
            this.treasury += Math.floor(this.population * this.taxRate);
        },
        applyGrowth(percent) {
            this.population += Math.floor(this.population * (percent / 100));
        },
        applyRecession(percent) {
            this.treasury -= Math.ceil(this.treasury * (percent / 100));
        }
    };

    return result;
}

function objectFactory(library, orders) {
    return orders.map(compose);

    function compose(order) {
        const result = Object.assign({}, order.template);

        for (const part of order.parts) {
            result[part] = library[part];
        }

        return result;
    }
}

const library = {
    print: function () {
        console.log(`${this.name} is printing a page`);
    },
    scan: function () {
        console.log(`${this.name} is scanning a document`);
    },
    play: function (artist, track) {
        console.log(`${this.name} is playing '${track}' by ${artist}`);
    },
};
const orders = [
    {
        template: { name: 'ACME Printer'},
        parts: ['print']
    },
    {
        template: { name: 'Initech Scanner'},
        parts: ['scan']
    },
    {
        template: { name: 'ComTron Copier'},
        parts: ['scan', 'print']
    },
    {
        template: { name: 'BoomBox Stereo'},
        parts: ['play']
    }
];
const products = factory(library, orders);
console.log(products);

