import * as api from "./api/api.js";
import { e, showSection, updateUsernav } from "./dom.js";
import { showLoginPage } from "./login.js";

const catalogSection = document.getElementById("catalogSection");
catalogSection.remove();
const ul = catalogSection.querySelector("ul");

export function showCatalogPage() {
  showSection(catalogSection);

  loadMovies();
}

async function loadMovies() {
  ul.replaceChildren(e("p", {}, "Loading ..."));
  const movies = api.get("/data/movies");
  ul.replaceChildren(...movies.map(createMovieCard));
}

function createMovieCard(movie) {
  return e("li", {}, movie.title);
}
