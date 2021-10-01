function evenPositions(arr) {
    let result = [];
    for (let i = 0; i < arr.length; i += 2) {
        result.push(arr[i]);
    }

    console.log(result.join(' '));
}

function lastKSequence(n, k) {
    let result = [];
    result.push(1);

    for (let i = 1; i < n; i++) {
        let nextNum = 0;
        for (let j = 1; j <= k; j++) {
            if (result[i - j]) {
                nextNum += result[i - j];
            }
        }
        result.push(nextNum);
    }

    return result;
}

function sumFirstLast(arr) {
    const first = Number(arr.shift());
    const last = arr.length ? Number(arr.pop()) : first;

    return first + last;
}

function negativePositive(nums) {
    const result = [];

    for (const num of nums) {
        if (num < 0) {
            result.unshift(num);
        } else {
            result.push(num);
        }
    }

    console.log(result.join('\n'));
}

function smallestTwo(nums) {
    const sorted = nums.sort((a, b) => a - b);

    console.log(sorted.shift(), sorted.shift());
}

function biggerHalf(nums) {
    const sorted = nums.sort((a, b) => a -b);
    return sorted.slice(Math.floor(nums.length / 2))
}

function pieceOfPie(flavors, first, last) {
    const firstIndex = flavors.indexOf(first);
    const lastIndex = flavors.indexOf(last);

    return flavors.splice(firstIndex, lastIndex + 1);
}

function oddPositions(nums) {
    const result = [];

    for (let i = 1; i < nums.length; i += 2) {
        result.push(nums[i]);
    }

    console.log(result
        .map(el => el * 2)
        .reverse()
        .join());
}

function biggestElement(matrix) {
    let maxNum = matrix[0][0];

    for (let i = 0; i < matrix.length; i++) {
        for (let j = 0; j < matrix[i].length; j++) {
            if (matrix[i][j] > maxNum) {
                maxNum = matrix[i][j];
            }
        }
    }

    return maxNum;
}

function diagonalSums(matrix) {
    let mainSum = 0;
    let secondarySum = 0;

    for (let i = 0; i < matrix.length; i++) {
        mainSum += matrix[i][i];
        secondarySum += matrix[i][matrix.length - i -1];
    }

    console.log(mainSum, secondarySum);
}

function equalNeighbors(matrix) {
    let pairs = 0;
    const rows = matrix.length;
    const cols = matrix[0].length

    for (let i = 0; i < cols - 1; i++) {

        if (matrix[0][i + 1] === matrix[0][i]) {
            pairs++;
        }

        if (matrix[1][i] === matrix[0][i]) {
            pairs++;
        }
    }

    for (let i = 0; i < cols - 1; i++) {
        if (matrix[rows - 1][i] === matrix[rows - 1][i + 1]) {
            pairs++;
        }
    }

    for (let i = 1; i < rows - 1; i++) {
        if (matrix[i][0] === matrix[i][1]) {
            pairs++;
        }

        if (matrix[i][0] === matrix[i + 1][0]) {
            pairs++;
        }
    }

    for (let i = 0; i < rows - 1; i++) {
        if (matrix[i][cols - 1] === matrix[i + 1][cols - 1]) {
            pairs++
        }
    }

    for (let i = 1; i < rows - 1; i++) {
        for (let j = 1; j < cols - 1; j++) {

            if (matrix[i][j] === matrix[i][j + 1]) {
                pairs++;
            }

            if (matrix[i][j] === matrix[i + 1][j]) {
                pairs++;
            }
        }
    }

    return pairs;
}