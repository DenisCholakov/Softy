class Company {
  constructor() {
    this.departments = {};
  }

  addEmployee(name, salary, position, department) {
    if (!name || salary < 0 || !salary || !position || !department) {
      throw new TypeError("Invalid input!");
    }

    if (!this.departments[department]) {
      this.departments[department] = [];
    }

    this.departments[department].push({ name, position, salary });

    return `New employee is hired. Name: ${name}. Position: ${position}`;
  }

  bestDepartment() {
    let currentBest = { department: "", salary: 0 };

    for (const department in this.departments) {
      const averageSalary = this.departments[department]
        .map((x) => x.salary)
        .reduce((curr, acc, index, arr) => curr + acc / arr.length, 0);
      if (averageSalary > currentBest.salary) {
        currentBest.name = department;
        currentBest.salary = averageSalary;
      }
    }

    this.departments[currentBest.name].sort((a, b) => {
      if (a.salary - b.salary === 0) {
        return a.name.localeCompare(b.name);
      } else {
        return b.salary - a.salary;
      }
    });

    let result = "";
    result += `Best Department is: ${currentBest.name}\n`;
    result += `Average salary: ${currentBest.salary.toFixed(2)}\n`;

    this.departments[currentBest.name].forEach(
      (x) => (result += `${x.name} ${x.salary} ${x.position}\n`)
    );

    return result.trim();
  }
}

let c = new Company();
c.addEmployee("Stanimir", 2000, "engineer", "Construction");
c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
c.addEmployee("Slavi", 500, "dyer", "Construction");
c.addEmployee("Stan", 2000, "architect", "Construction");
c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
c.addEmployee("Gosho", 1350, "HR", "Human resources");
console.log(c.bestDepartment());
