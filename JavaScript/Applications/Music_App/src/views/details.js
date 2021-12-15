import { deleteAlbum, getAlbumDetails } from "../api/data.js";
import { html } from "../lib.js";
import { getUserData } from "../util.js";

const detailsTemplate = (album, isOwner, onDelete) => html`<section
  id="detailsPage"
>
  <div class="wrapper">
    <div class="albumCover">
      <img src=${album.imgUrl} />
    </div>
    <div class="albumInfo">
      <div class="albumText">
        <h1>Name: ${album.name}</h1>
        <h3>Artist: ${album.artist}</h3>
        <h4>Genre: ${album.genre}</h4>
        <h4>Price: $${album.price}</h4>
        <h4>Date: ${album.releaseDate}</h4>
        <p>${album.description}</p>
      </div>

      ${isOwner
        ? html` <div class="actionBtn">
            <a href="/edit/${album._id}" class="edit">Edit</a>
            <a href="/delete/${album._id}" @click=${onDelete} class="remove"
              >Delete</a
            >
          </div>`
        : null}
    </div>
  </div>
</section>`;

export async function detailsPage(ctx) {
  const meme = await getAlbumDetails(ctx.params.id);

  const userData = getUserData();
  const isOwner = userData && userData.id === meme._ownerId;
  ctx.render(detailsTemplate(meme, isOwner, onDelete));

  async function onDelete(ev) {
    ev.preventDefault();

    const choise = confirm(
      "Are you sure you want to delete this album forever?"
    );

    if (choise) {
      await deleteAlbum(ctx.params.id);
      ctx.page.redirect("/catalog");
    }
  }
}
