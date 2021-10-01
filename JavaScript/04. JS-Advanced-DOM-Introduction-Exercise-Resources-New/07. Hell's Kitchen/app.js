function solve() {
  document.querySelector("#btnSend").addEventListener("click", onClick);
   const restaurants = {};
  let bestRestaurant = { averageSalary: 0 };
  let bestEmployees = [];

  function onClick() {
    const input = JSON.parse(document.querySelector("#inputs textarea").value);
    input.forEach((value) => {
      const employees = [];
      let [restaurantName, employeesInput] = value.split(" - ");
      employeesInput = employeesInput.split(", ");
      employeesInput.forEach((employee) => {
        const [name, salary] = employee.split(" ");
        employees.push({
          name,
          salary: Number(salary),
        });
      });

       if (!restaurants[restaurantName]) {
          restaurants[restaurantName].push({
             name: restaurantName,
             employees: employees.sort((a, b) => b.salary - a.salary),
             
          })
       }
      console.log(bestRestaurant);
    });
  }
}
