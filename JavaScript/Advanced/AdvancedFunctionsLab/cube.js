const solve = (area, vol, input) =>
  JSON.parse(input).map((c) => ({ area: area.apply(c), volume: vol.apply(c) }));
