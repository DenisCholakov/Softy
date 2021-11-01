function printWithDelimiter(arr, delimiter) {
    console.log(arr.join(delimiter));
}

function printNthElements(arr, step) {
    const result = [];

    for (let i = 0; i < arr.length; i += step) {
        result.push(arr[i]);
    }

    return result;
}

function addAndRemoveElements(commands) {
    let num = 0;
    const result = [];

    for (const command of commands) {
        num++;

        if (command === "add") {
            result.push(num);
        } else {
            result.pop();
        }
    }

    if (result.length) {
        console.log(result.join('\n'));
    } else {
        console.log('Empty');
    }
}

function rotateArray(arr, rotationsCount) {
    for (let i = 0; i < rotationsCount; i++) {
        arr.unshift(arr.pop());
    }

    console.log(arr.join(' '));
}

function increasingSubsequence(arr) {
    let filter = function (arr) {
        let maxNum = arr[0];

        return arr.filter = arr.filter((el) => {
            if (el >= maxNum) {
                maxNum = el;
                return true;
            }

            return false;
        })
    }

    let reduce = function (arr) {
        let maxNum = arr.shift();

        return arr.reduce((acc, curr) => {
            if (curr >= maxNum) {
                maxNum = curr;
                acc.push(curr);
            }

            return acc;
        }, [maxNum]);
    }

    return reduce(arr);
}

function listOfNames(names) {
    const sortedNames = names.sort((a, b) => a.localeCompare(b));

    for (let i = 0; i < sortedNames.length; i++) {
        console.log(`${i + 1}.${sortedNames[i]}`)
    }
}

function sortingNumbers(nums) {
    const sortedNums = nums.sort((a, b) => a - b);
    const result = [];

    while (sortedNums.length) {
        result.push(sortedNums.shift());

        if (sortedNums.length) {
            result.push(sortedNums.pop());
        }
    }

    return result;
}

function sortByTwoCriteria(arr) {
    const result = arr.sort((a, b) => {
        let lengthCompare = a.length - b.length;

        if (lengthCompare !== 0) {
            return lengthCompare;
        } else {
            return a.localeCompare(b);
        }
    });

    console.log(result.join('\n'))
}

function magicMatrices(matrix) {
    const sum = matrix[0].reduce((acc, val) => acc + val);
    for (let i = 0; i < matrix.length; i++) {
        const sum1 = matrix[i].reduce((acc, val) => acc + val);
        const sum2 = matrix.reduce((acc, val) => acc + val[i], 0);

        if (sum1 !== sum2 || sum1 !== sum) {
            return false;
        }
    }

    return true;
}

function ticTacToe(moves) {
    const playField = [
        [false, false, false],
        [false, false, false],
        [false, false, false]
    ]

    let currPlayer = 'X';
    let movesCounter = 1;

    for (const move of moves) {
        const [row, col] = move.split(' ').map(el => Number(el));

        if (playField[row][col]) {
            console.log("This place is already taken. Please choose another!");
            continue;
        }

        playField[row][col] = currPlayer;

        if (checkForWinner(playField, currPlayer, row, col)) {
            console.log(`Player ${currPlayer} wins!`);
            playField.forEach((el) => console.log(el.join('\t')));
            return;
        }

        if (movesCounter >= 9) {
            console.log("The game ended! Nobody wins :(");
            playField.forEach((el) => console.log(el.join('\t')));
            return;
        }

        currPlayer = currPlayer === 'X' ? 'O' : 'X';
        movesCounter++;
    }

    console.log("The game ended! Nobody wins :(");
    playField.forEach((el) => console.log(el.join('\t')));

    function checkForWinner(playField, player, row, col) {
        if (playField[row].every(p => p === player)) {
            return true;
        }

        if (playField.every(r => r[col] === player)) {
            return true;
        }

        if (row === col && checkMainDiagonal(playField, player)) {
            return true;
        }

        return col === playField.length - row - 1 && checkSecondaryDiagonal(playField, player);



        function checkMainDiagonal(playField, player) {
            for (let i = 0; i < playField.length; i++) {
                if (playField[i][i] !== player) {
                    return false;
                }
            }

            return true;
        }

        function checkSecondaryDiagonal(playField, player) {
            for (let i = 0; i < playField.length; i++) {
                if (playField[i][playField.length - i - 1] !== player) {
                    return false;
                }
            }

            return true;
        }
    }
}

function diagonalAttack(matrix) {
    matrix = parseMatrix(matrix);
    let [ sum1, sum2 ] = [0, 0];
    for (let i = 0; i < matrix.length; i++) {
        sum1 += matrix[i][i];
        sum2 += matrix[i][matrix.length - i -1];
    }

    if (sum1 !== sum2) {
        printMatrix(matrix);
        return;
    }

    for (let i = 0; i < matrix.length; i++) {
        for (let j = 0; j < matrix.length; j++) {
            if (i !== j && j !== matrix.length - i - 1) {
                matrix[i][j] = sum1;
            }
        }
    }

    printMatrix(matrix)

    function parseMatrix(matrix) {
        const result = [];

        for (const row of matrix) {
            result.push(row.split(' ').map(x => Number(x)));
        }

        return result;
    }
    function printMatrix(matrix) {
        matrix.forEach((el) => console.log(el.join(' ')))
    }
}

function orbit(inputArr) {
    const [width, height, x, y] = inputArr;
    const result = [];

    for (let i = x; i < width; i++) {
        result[i] = [];
        for (let j = y; j < height; j++) {
            result[i][j] = calculateCellValue(i, j, x, y);
        }
    }

    for (let i = x - 1; i >= 0; i--) {
        result[i] = [];
        for (let j = y; j < height; j++) {
            result[i][j] = calculateCellValue(i, j, x, y);
        }
    }

    for (let i = x; i >= 0; i--) {
        for (let j = y - 1; j >= 0; j--) {
            result[i][j] = calculateCellValue(i, j, x, y);
        }
    }

    for (let i = x + 1; i < width; i++) {
        for (let j = y - 1; j >= 0; j--) {
            result[i][j] = calculateCellValue(i, j, x, y);
        }
    }

    printMatrix(result);

    function printMatrix(matrix) {
        matrix.forEach((el) => console.log(el.join(' ')))
    }
    function calculateCellValue(row, col, x, y) {
        return Math.max((Math.abs(row - x) + 1), (Math.abs(col - y) + 1))
    }
}

function spiralMatrix(width, height) {
    let top = 0;
    let bottom = height - 1;
    let left = 0;
    let right = width - 1;
    let counter = 0;
    let result = createMatrix(height, width);

    while (true) {
        for (let i = left; i <= right; i++) {
            result[top][i] = ++counter;
        }

        top++;

        if (top > bottom) {
            break;
        }

        for (let i = top; i <= bottom; i++) {
            result[i][right] = ++counter;
        }

        right --;

        if (left > right) {
            break;
        }

        for (let i = right; i >= left; i--) {
            result[bottom][i] = ++counter;
        }

        bottom--;

        if (top > bottom) {
            break;
        }

        for (let i = bottom; i >= top; i--) {
            result[i][left] = ++counter;
        }

        left++;

        if (left > right) {
            break;
        }
    }

    printMatrix(result);

    function printMatrix(matrix) {
        matrix.forEach((el) => console.log(el.join(' ')))
    }
    function createMatrix(rows, cols) {
        const result = new Array(rows);

        for (let i = 0; i < rows; i++) {
            result[i] = new Array(cols);
        }

        return result;
    }
}