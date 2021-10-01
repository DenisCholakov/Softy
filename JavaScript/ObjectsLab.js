function cityRecord(name, population, treasury) {
  const result = {
    name: name,
    population: population,
    treasury: treasury,
  };

  return result;
}

function townPopulation(townsAsStrings) {
  const towns = {};

  for (const data of townsAsStrings) {
    const tokens = data.split(" <-> ");
    const name = tokens[0];
    let population = Number(tokens[1]);

    if (towns[name] == undefined) {
      towns[name] = population;
    } else {
      towns[name] += population;
    }
  }

  for (const name in towns) {
    console.log(`${name} : ${towns[name]}`);
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
    },
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

function createAssemblyLine() {
  return {
    hasClima(object) {
      object["temp"] = 21;
      object["tempSettings"] = 21;
      object["adjustTemp"] = () => {
        if (object.temp < object.tempSettings) {
          object.temp++;
        } else if (object.temp > object.tempSettings) {
          object.temp--;
        }
      };
    },

    hasAudio(object) {
      object["currentTrack"] = null;
      object["nowPlaying"] = () => {
        if (object.currentTrack) {
          console.log(
            `Now playing '${object.currentTrack.name}' by ${object.currentTrack.artist}`
          );
        }
      };
    },

    hasParktronic(object) {
      object["checkDistance"] = (distance) => {
        if (typeof distance === "number") {
          if (distance < 0.1) {
            console.log("Beep! Beep! Beep!");
          } else if (0.1 <= distance && distance < 0.25) {
            console.log("Beep! Beep!");
          } else if (0.25 <= distance && distance < 0.5) {
            console.log("Beep!");
          } else {
            console.log("");
          }
        }
      };
    },
  };
}

function createHTMLTable(input) {
  let entityMap = {
    "&": "&amp;",
    "<": "&lt;",
    ">": "&gt;",
    '"': "&quot;",
    "'": "&#39;",
    "/": "&#x2F;",
  };

  const students = JSON.parse(input);

  let result = "";
  result += "<table>\n    <tr>";

  for (const key in students[0]) {
    let string = `${key}`;
    result += "<th>" + string + "</th>";
  }
  result += "</tr>\n";

  for (const student of students) {
    result += "    <tr>";

    for (const key in student) {
      let string = `${student[key]}`;
      result +=
        "<td>" + string.replace(/[&<>"'\/]/g, (s) => entityMap[s]) + "</td>";
    }

    result += "</tr>\n";
  }

  result += "</table>";

  console.log(result);
}

createHTMLTable(`[{"Name":"Stamat",
"Score":5.5},
{"Name":"Rumen",
"Score":6}]`);
