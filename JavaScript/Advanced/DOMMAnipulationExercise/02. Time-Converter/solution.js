function attachEventsListeners() {
  const ratios = {
    days: 1,
    hours: 24,
    minutes: 1440,
    seconds: 86400,
  };

  const inputs = Object.keys(ratios)
    .map((unit) => document.getElementById(unit))
    .reduce((acc, curr) => Object.assign(acc, { [curr.id]: curr }), {});

  document.querySelector("main").addEventListener("click", onConvert);

  function onConvert(ev) {
    if ((ev.target.tagName = "INPUT" && ev.target.type == "button")) {
      const input = ev.target.parentElement.querySelector('input[type="text"]');

      const time = convert(Number(input.value), input.id);
      Object.keys(time).forEach((k) => (inputs[k].value = time[k]));
    }
  }

  function convert(value, unit) {
    const inDays = value / ratios[unit];

    return {
      days: inDays,
      hours: inDays * ratios.hours,
      minutes: inDays * ratios.minutes,
      seconds: inDays * ratios.seconds,
    };
  }
}
