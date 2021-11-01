function fruitShop(fruit, weight, price) {
    const weightInKg = weight / 1000;
    const moneyNeeded = weightInKg * price;

    console.log(`I need $${moneyNeeded.toFixed(2)} to buy ${weightInKg.toFixed(2)} kilograms ${fruit}.`);
}

function gcd(a, b) {
    while (b != 0) {
        const temp = b;
        b = a % b;
        a = temp;
    }

    return a;
}

function sameNumbers(num) {
    let areSame = true;
    let prevDigit = num % 10;
    let digitsSum = prevDigit;
    num = Math.floor(num / 10);

    while (num != 0) {
        const currDigit = num % 10;

        if (prevDigit !== currDigit) {
            areSame = false;
        }

        digitsSum += currDigit;
        prevDigit = currDigit;
        num = Math.floor(num / 10);
    }

    console.log(areSame);
    console.log(digitsSum);
}

function previousDay(year, month, day) {
    const currentDate = new Date(year, month + 1, day);
    let dayBefore = new Date(currentDate);

    dayBefore.setDate(currentDate.getDate() - 1);

    return `${dayBefore.getFullYear()}-${dayBefore.getMonth() - 1}-${dayBefore.getDate()}`
}

// footprint is in meters
// speed is in km/h
function timeToGetToUni(steps, footprint, speed) {
    const distInMeters = steps * footprint;
    const speedInMetersPerSecond = (speed * 1000) / 3600;
    const breaks = Math.floor(distInMeters / 500);

    let timeToGetInSeconds = Math.ceil(distInMeters / speedInMetersPerSecond);

    let date = new Date(0);
    date.setSeconds(timeToGetInSeconds + breaks * 60);

    console.log(date.toISOString().substr(11,8));
}

function roadRadar(speed, area) {
    const motorwayLimit = 130;
    const interstateLimit = 90;
    const cityLimit = 50;
    const residentialLimit = 20;

    let speedLimit = 0;

    switch (area) {
        case "motorway": speedLimit = motorwayLimit; break;
        case "interstate": speedLimit = interstateLimit; break;
        case "city": speedLimit = cityLimit; break;
        case "residential": speedLimit = residentialLimit; break;
    }

    if (speed < speedLimit) {
        console.log(`Driving ${speed} km/h in a ${speedLimit} zone`);
    } else {
        let difference = speed - speedLimit;
        let status;

        switch (true) {
            case (difference < 20): status = 'speeding'; break;
            case (difference < 40): status = 'excessive speeding'; break;
            default: status = 'reckless driving'; break;
        }

        console.log(`The speed is ${difference} km/h faster than the allowed speed of ${speedLimit} - ${status}`);
    }
}

function cookingByNumbers(numStr, ...operations) {
    let num = Number(numStr);

    let chop = function (n) {
        let result = n / 2;
        console.log(result);
        return result;
    }

    let dice = function (n) {
        let result = Math.sqrt(n);
        console.log(result);
        return result;
    }

    let spice = function (n) {
        let result = n  + 1;
        console.log(result);
        return result;
    }

    let bake = function (n) {
        let result = n * 3;
        console.log(result);
        return result;
    }

    let fillet = function (n) {
        let result = n * 0.8;
        console.log(result);
        return result;
    }

    operations.forEach(op => {
        switch (op) {
            case "chop": num = chop(num); break;
            case "dice": num = dice(num); break;
            case "spice": num = spice(num); break;
            case "bake": num = bake(num); break;
            case "fillet": num = fillet(num); break;
        }
    })
}

function validityChecker(x1, y1, x2, y2) {
    let checkIfValid = function (x1, y1, x2, y2) {
        const distance = Math.sqrt(Math.abs(x1 - x2) ** 2 + Math.abs(y1 - y2) ** 2)

        if (Number.isInteger(distance)) {
            console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is valid`)
        } else {
            console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is invalid`)
        }
    }

    checkIfValid(x1, y1, 0, 0);
    checkIfValid(x2, y2, 0, 0);
    checkIfValid(x1, y1, x2, y2);
}

function wordsUppercase(text) {

    let result = text.toUpperCase()
        .split(/[\W]+/)
        .filter(w => w.length > 0)
        .join(", ");

    console.log(result);
}


wordsUppercase('Hi, how are you?');