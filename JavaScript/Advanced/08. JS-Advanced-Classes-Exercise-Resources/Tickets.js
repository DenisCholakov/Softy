function solve(tickets, criteria) {
  class Ticket {
    constructor(destination, price, status) {
      this.destination = destination;
      this.price = price;
      this.status = status;
    }
  }

  const ticketObjects = [];

  tickets.forEach((x) => {
    const [dest, price, status] = x.split("|");
    ticketObjects.push(new Ticket(dest, Number(price), status));
  });

  if (criteria == "destination") {
    ticketObjects.sort((a, b) => a.destination.localeCompare(b.destination));
  } else if (criteria == "status") {
    ticketObjects.sort((a, b) => a.status.localeCompare(b.status));
  } else {
    ticketObjects.sort((a, b) => a.price - b.price);
  }

  return ticketObjects;
}
