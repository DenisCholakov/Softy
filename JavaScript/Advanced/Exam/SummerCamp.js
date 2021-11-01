class SummerCamp {
  constructor(organizer, location) {
    this.organizer = organizer;
    this.location = location;
    this.priceForTheCamp = {
      child: 150,
      student: 300,
      collegian: 500,
    };
    this.listOfParticipants = [];
  }

  registerParticipant(name, condition, money) {
    if (
      condition !== "child" &&
      condition !== "student" &&
      condition !== "collegian"
    ) {
      throw new Error("Unsuccessful registration at the camp.");
    }

    if (this.listOfParticipants.some((x) => x.name === name)) {
      return `The ${name} is already registered at the camp.`;
    }

    if (money < this.priceForTheCamp[condition]) {
      return `The money is not enough to pay the stay at the camp.`;
    }

    this.listOfParticipants.push({
      name,
      condition,
      power: 100,
      wins: 0,
    });

    return `The ${name} was successfully registered.`;
  }

  unregisterParticipant(name) {
    if (!this.listOfParticipants.some((x) => x.name === name)) {
      throw new Error(`The ${name} is not registered in the camp.`);
    }
    this.listOfParticipants = this.listOfParticipants.filter(
      (x) => x.name !== name
    );

    return `The ${name} removed successfully.`;
  }

  timeToPlay(typeOfGame, participant1, participant2) {
    if (typeOfGame === "WaterBalloonFights") {
      const playerOne = this.listOfParticipants.find(
        (x) => x.name === participant1
      );
      const playerTwo = this.listOfParticipants.find(
        (x) => x.name === participant2
      );

      if (playerOne === undefined || playerTwo === undefined) {
        throw new Error(`Invalid entered name/s.`);
      }

      if (playerOne.condition !== playerTwo.condition) {
        throw new Error(`Choose players with equal condition.`);
      }

      if (playerOne.power > playerTwo.power) {
        playerOne.wins++;
        return `The ${playerOne.name} is winner in the game ${typeOfGame}.`;
      } else if (playerOne.power < playerTwo.power) {
        playerTwo.wins++;
        return `The ${playerTwo.name} is winner in the game ${typeOfGame}.`;
      } else {
        return `There is no winner.`;
      }
    } else if (typeOfGame === "Battleship") {
      const player = this.listOfParticipants.find(
        (x) => x.name === participant1
      );

      if (player === undefined) {
        throw new Error(`Invalid entered name/s.`);
      }

      player.power += 20;

      return `The ${player.name} successfully completed the game ${typeOfGame}.`;
    }
  }

  toString() {
    let result = "";

    result += `${this.organizer} will take ${this.listOfParticipants.length} participants on camping to ${this.location}\n`;

    this.listOfParticipants = this.listOfParticipants.sort(
      (a, b) => b.wins - a.wins
    );

    for (const participant of this.listOfParticipants) {
      result += `${participant.name} - ${participant.condition} - ${participant.power} - ${participant.wins}\n`;
    }

    return result.trim();
  }
}

let camp = new SummerCamp("Jane Austen", "Pancharevo Sofia 1137, Bulgaria");
console.log(camp.registerParticipant("Petar Petarson", "child", 300));
console.log(camp.registerParticipant("Sara Dickinson", "child", 200));
console.log(camp.timeToPlay("Battleship", "Sara Dickinson"));
console.log(
  camp.timeToPlay("WaterBalloonFights", "Sara Dickinson", "Petar Petarson")
);
console.log(camp.toString());
