function solve() {
  document.querySelector("#btnSend").addEventListener("click", onClick);
  const restaurants = [];

  function onClick() {
    const input = JSON.parse(document.querySelector("#inputs textarea").value);
    input.forEach((value) => {
      let employees = [];
      let [restaurantName, employeesInput] = value.split(" - ");
      employeesInput = employeesInput.split(", ");
      employeesInput.forEach((employee) => {
        const [name, salary] = employee.split(" ");
        employees.push({
          name,
          salary: Number(salary),
        });
      });

      employees = employees.sort((a, b) => b.salary - a.salary);

      const restaurant = {
        name: restaurantName,
        employees: employees,
        averageSalary:
          employees.reduce((acc, curr) => acc + curr.salary, 0) /
          employees.length,
        bestSalary: employees[0].salary,
        bestEmployees: employees.slice(0, 3),
      };

      const index = restaurants.indexOf(restaurant);

      if (index !== -1) {
        restaurants[index] = restaurant;
      } else {
        restaurants.push(restaurant);
      }
    });

    const bestRestaurant = restaurants.sort(
      (a, b) => b.averageSalary - a.averageSalary
    )[0];

    document.querySelector("#bestRestaurant p").textContent = `Name: ${
      bestRestaurant.name
    } Average Salary: ${bestRestaurant.averageSalary.toFixed(
      2
    )} Best Salary: ${bestRestaurant.bestSalary.toFixed(2)}`;

    document.querySelector("#workers p").textContent =
      bestRestaurant.bestEmployees
        .map((e) => `Name: ${e.name} With Salary: ${e.salary}`)
        .join(" ");
  }
}
