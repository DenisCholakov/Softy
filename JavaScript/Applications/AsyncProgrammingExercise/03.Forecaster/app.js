async function attachEvents() {
  const symbols = {
    Sunny: String.fromCodePoint(0x2600),
    "Partly sunny": String.fromCodePoint(0x26c5),
    Overcast: String.fromCodePoint(0x2601),
    Rain: String.fromCodePoint(0x2614),
    Degrees: String.fromCodePoint(176),
  };

  const locationsUrl = "http://localhost:3030/jsonstore/forecaster/locations";
  const locationField = document.getElementById("location");
  const submitBtn = document.getElementById("submit");
  const forecastDiv = document.getElementById("forecast");
  const oneDayDiv = document.getElementById("current");
  const threeDayDiv = document.getElementById("upcoming");

  const oneDayForecastDiv = document.createElement("div");
  oneDayForecastDiv.className = "forecasts";
  oneDayDiv.appendChild(oneDayForecastDiv);
  const threeDayforecastDiv = document.createElement("div");
  threeDayforecastDiv.className = "forecast-info";
  threeDayDiv.appendChild(threeDayforecastDiv);

  submitBtn.addEventListener("click", getWeather);

  let locations = [];

  try {
    const res = await fetch(locationsUrl);

    if (res.status !== 200) {
      throw new Error("right now weather is unavaliable.");
    }
    locations = await res.json();
  } catch (error) {
    showError(error.message);
  }

  async function getWeather() {
    forecastDiv.style.display = "none";
    oneDayForecastDiv.replaceChildren();
    threeDayforecastDiv.replaceChildren();

    try {
      const locationName = locationField.value;

      console.log(locationName);
      const location = locations.find((l) => l.name === locationName);

      if (!location) {
        throw new Error("the given location is invalid");
      }

      const [oneDayForecast, threeDayForecast] = await Promise.all(
        new Array(
          getOneDayWeather(location.code),
          getThreeDayWeather(location.code)
        )
      );

      forecastDiv.style.display = "block";

      oneDayForecastDiv.appendChild(createOneDayForecastSpan(oneDayForecast));
      threeDayForecast.forecast.forEach((info) =>
        threeDayforecastDiv.appendChild(createThreeDayForecastSpan(info))
      );
    } catch (error) {
      showError(error.message);
    }
  }

  async function getOneDayWeather(code) {
    const url = `http://localhost:3030/jsonstore/forecaster/today/${code}`;
    try {
      const res = await fetch(url);

      if (res.status !== 200) {
        throw new error("location was not found");
      }

      return await res.json();
    } catch (error) {
      showError(error.message);
    }
  }

  async function getThreeDayWeather(code) {
    const url = `http://localhost:3030/jsonstore/forecaster/upcoming/${code}`;
    try {
      const res = await fetch(url);

      if (res.status !== 200) {
        throw new error("location was not found");
      }

      return await res.json();
    } catch (error) {
      showError(error.message);
    }
  }

  function createOneDayForecastSpan(forecastInfo) {
    const forecast = forecastInfo.forecast;
    const cityName = forecastInfo.name;
    const temperatures = `${forecast.low}${symbols["Degrees"]}\\${forecast.high}${symbols["Degrees"]}`;

    const forecatsSpan = createSpan("", "forecasts");
    const conditionsSymbolSpan = createSpan(
      symbols[forecast.condition],
      "condition symbol"
    );
    const conditionSpan = createSpan("", "condition");
    conditionSpan.appendChild(createSpan(cityName, "forecast-data"));
    conditionSpan.appendChild(createSpan(temperatures, "forecast-data"));
    conditionSpan.appendChild(createSpan(forecast.condition, "forecast-data"));

    forecatsSpan.appendChild(conditionsSymbolSpan);
    forecatsSpan.appendChild(conditionSpan);

    return forecatsSpan;
  }

  function createThreeDayForecastSpan(forecast) {
    const upcomingSpan = createSpan("", "upcoming");
    const temperatures = `${forecast.low}${symbols["Degrees"]}\\${forecast.high}${symbols["Degrees"]}`;

    const symbolSpan = createSpan(symbols[forecast.condition], "symbols");
    upcomingSpan.appendChild(symbolSpan);

    const temperaturesSpan = createSpan(temperatures, "forecast-data");
    upcomingSpan.appendChild(temperaturesSpan);

    const conditionSpan = createSpan(forecast.condition, "forecast-data");
    upcomingSpan.appendChild(conditionSpan);

    return upcomingSpan;
  }

  function createSpan(textContent, className) {
    const span = document.createElement("span");
    span.className = className;
    span.textContent = textContent;

    return span;
  }

  function showError(message) {
    const errDiv = document.createElement("div");
    errDiv.textContent = message;
    document.body.appendChild(errDiv);
  }
}

attachEvents();
