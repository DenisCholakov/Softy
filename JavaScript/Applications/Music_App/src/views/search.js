import { getAlbumsByName } from "../api/data.js";
import { html } from "../lib.js";
import { getUserData } from "../util.js";

const searchTemplate = (albums, onSearchByName, userData) => html`<section
  id="searchPage"
>
  <h1>Search by Name</h1>

  <div class="search">
    <input
      id="search-input"
      type="text"
      name="search"
      placeholder="Enter desired albums's name"
    />
    <button class="button-list" @click=${onSearchByName}>Search</button>
  </div>

  <h2>Results:</h2>
  ${albums === undefined ? null : searchResultsTemplate(albums, userData)}
</section>`;

const searchResultsTemplate = (albums, userData) => {
  return html` <div class="search-result">
    ${albums.length == 0
      ? html` <p class="no-result">No result.</p>`
      : albums.map((a) => albumCard(a, userData))}
  </div>`;
};

const albumCard = (album, userData) => html`<div class="card-box">
  <img src=${album.imgUrl} />
  <div>
    <div class="text-center">
      <p class="name">Name: ${album.name}</p>
      <p class="artist">Artist: ${album.artist}</p>
      <p class="genre">Genre: ${album.genre}</p>
      <p class="price">Price: $${album.price}</p>
      <p class="date">Release Date: ${album.releaseDate}</p>
    </div>
    <div class="btn-group">
      ${userData
        ? html`<a href="/details/${album._id}" id="details">Details</a>`
        : null}
    </div>
  </div>
</div>`;

export function searchPage(ctx) {
  const userData = getUserData();
  ctx.render(searchTemplate(undefined, onSearchByName, userData));

  async function onSearchByName(ev) {
    const searchTerm = document.getElementById("search-input").value;

    if (searchTerm.trim() == "") {
      return alert("You need to fill name to serach for.");
    }

    const albums = await getAlbumsByName(searchTerm);

    ctx.render(searchTemplate(albums, onSearchByName, userData));
  }
}
