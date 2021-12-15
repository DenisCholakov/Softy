import { getAlbumDetails, editAlbum } from "../api/data.js";
import { html } from "../lib.js";

const editTemplate = (album, onSubmit) => html`<section id="edit-meme">
  <section class="editPage">
    <form @submit=${onSubmit}>
      <fieldset>
        <legend>Edit Album</legend>

        <div class="container">
          <label for="name" class="vhide">Album name</label>
          <input
            id="name"
            name="name"
            class="name"
            type="text"
            value=${album.name}
          />

          <label for="imgUrl" class="vhide">Image Url</label>
          <input
            id="imgUrl"
            name="imgUrl"
            class="imgUrl"
            type="text"
            value=${album.imgUrl}
          />

          <label for="price" class="vhide">Price</label>
          <input
            id="price"
            name="price"
            class="price"
            type="text"
            value=${album.price}
          />

          <label for="releaseDate" class="vhide">Release date</label>
          <input
            id="releaseDate"
            name="releaseDate"
            class="releaseDate"
            type="text"
            value=${album.releaseDate}
          />

          <label for="artist" class="vhide">Artist</label>
          <input
            id="artist"
            name="artist"
            class="artist"
            type="text"
            value=${album.artist}
          />

          <label for="genre" class="vhide">Genre</label>
          <input
            id="genre"
            name="genre"
            class="genre"
            type="text"
            value=${album.genre}
          />

          <label for="description" class="vhide">Description</label>
          <textarea name="description" class="description" rows="10" cols="10">
${album.description}</textarea
          >

          <button class="edit-album" type="submit">Edit Album</button>
        </div>
      </fieldset>
    </form>
  </section>
</section>`;

export async function editPage(ctx) {
  const album = await getAlbumDetails(ctx.params.id);
  ctx.render(editTemplate(album, onSubmit));

  async function onSubmit(ev) {
    ev.preventDefault();

    const formData = new FormData(ev.target);

    const name = formData.get("name");
    const imgUrl = formData.get("imgUrl");
    const price = formData.get("price").trim();
    const releaseDate = formData.get("releaseDate").trim();
    const artist = formData.get("artist").trim();
    const genre = formData.get("genre").trim();
    const description = formData.get("description").trim();

    if ([...formData.values()].some((x) => x === "")) {
      return alert("All fields are required");
    }

    const album = {
      name,
      imgUrl,
      price,
      releaseDate,
      artist,
      genre,
      description,
    };

    await editAlbum(ctx.params.id, album);

    ctx.page.redirect("/catalog");
  }
}
