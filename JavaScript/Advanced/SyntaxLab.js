function echo(message) {
    console.log(message.length);
    console.log(message);
}

function stringLength(arr1, arr2, arr3) {
    const allStringsLength = arr1.length + arr2.length + arr3.length;
    const averageLength = Math.round(allStringsLength / 3);

    console.log(allStringsLength);
    console.log(averageLength);
}

function largestNumber(...params) {
    console.log(`The largest number is ${Math.max(...params)}.`);
}

function circleArea(r) {
    if (typeof(r) != 'number')
    {
        console.log(`We can not calculate the circle area, because we receive a ${typeof(r)}.`);
        return;
    }

    const area = r ** 2 * Math.PI;
    console.log(area.toFixed(2))

}

function mathOperations(a, b, operator) {
    let result;

    switch (operator) {
        case '+': result = a + b; break;
        case '-': result = a - b; break;
        case '/': result = a / b; break;
        case '*': result = a * b; break;
        case '%': result = a % b; break;
        case '**': result = a ** b; break;
    }

    console.log(result);
}

function sumOfNumbers(strNum1, strNum2) {
    num1 = Number(strNum1);
    num2 = Number(strNum2);

    const numbersSum = ((Math.abs(num1 - num2) + 1) * (num1 + num2)) / 2;

    return numbersSum;
}

function dayOfWeek(dayOfWeek) {
    switch (dayOfWeek) {
        case 'Monday': console.log(1); break;
        case 'Tuesday': console.log(2); break;
        case 'Wednesday': console.log(3); break;
        case 'Thursday': console.log(4); break;
        case 'Friday': console.log(5); break;
        case 'Saturday': console.log(6); break;
        case 'Sunday': console.log(7); break;
        default: console.log('error');
    }
}

function daysInAMonth(month, year) {
    return new Date(year, month, 0).getDate();
}

function squareOfStars(starsCount) {
    if (starsCount === undefined) {
        starsCount = 5;
    }

    for (let i = 0; i < starsCount; i++) {
        console.log('* '.repeat(starsCount));
    }
}

function aggregateElements(params) {
    const sum = params.reduce((a, b) => a + b, 0);
    const invSum = params.reduce((a, b) => a + 1/b, 0);
    const concat = params.join('');

    console.log(sum);
    console.log(invSum);
    console.log(concat);
}

aggregateElements([1, 2, 3]);
aggregateElements([2, 4, 8, 16]);