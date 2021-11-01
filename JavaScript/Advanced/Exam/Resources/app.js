window.addEventListener("load", solve);

function solve() {
  const hitsContaner = document.querySelector(
    "#all-hits div.all-hits-container"
  );
  const savedSongsContainer = document.querySelector(
    "#saved-hits div.saved-container"
  );
  const totalLikesField = document.querySelector("#total-likes div.likes p");

  const genreField = document.getElementById("genre");
  const nameField = document.getElementById("name");
  const authorField = document.getElementById("author");
  const dateField = document.getElementById("date");
  const addBtn = document.getElementById("add-btn");

  addBtn.addEventListener("click", addSong);

  function addSong(ev) {
    ev.preventDefault();

    if (
      !genreField.value ||
      !nameField.value ||
      !authorField.value ||
      !dateField.value
    ) {
      return;
    }

    const img = document.createElement("img");
    img.src = "./static/img/img.png";

    const div = document.createElement("div");
    div.className = "hits-info";

    const genre = document.createElement("h2");
    genre.textContent = `Genre: ${genreField.value}`;

    const name = document.createElement("h2");
    name.textContent = `Name: ${nameField.value}`;

    const author = document.createElement("h2");
    author.textContent = `Author: ${authorField.value}`;

    const date = document.createElement("h3");
    date.textContent = `Date: ${dateField.value}`;

    const saveButton = document.createElement("button");
    saveButton.className = "save-btn";
    saveButton.textContent = "Save song";
    saveButton.addEventListener("click", saveSong);

    const likeButton = document.createElement("button");
    likeButton.className = "like-btn";
    likeButton.textContent = "Like song";
    likeButton.addEventListener("click", likeSong);

    const deleteButton = document.createElement("button");
    deleteButton.className = "delete-btn";
    deleteButton.textContent = "Delete";
    deleteButton.addEventListener("click", deleteSong);

    div.appendChild(img);
    div.appendChild(genre);
    div.appendChild(name);
    div.appendChild(author);
    div.appendChild(date);

    div.appendChild(saveButton);
    div.appendChild(likeButton);
    div.appendChild(deleteButton);

    hitsContaner.appendChild(div);

    genreField.value = "";
    nameField.value = "";
    authorField.value = "";
    dateField.value = "";
  }

  function likeSong(ev) {
    ev.target.disabled = true;
    changeTotalLikes(1);
  }

  function saveSong(ev) {
    const song = ev.target.parentElement;
    const saveButton = song.querySelector(".save-btn");
    const likeButton = song.querySelector(".like-btn");

    song.removeChild(saveButton);
    song.removeChild(likeButton);

    savedSongsContainer.appendChild(song);
  }

  function deleteSong(ev) {
    ev.target.parentElement.remove();
  }

  function changeTotalLikes(num) {
    const totalLikes = Number(
      totalLikesField.textContent.replace("Total Likes: ", "")
    );

    totalLikesField.textContent = totalLikesField.textContent.replace(
      totalLikes.toString(),
      totalLikes + num
    );
  }
}
