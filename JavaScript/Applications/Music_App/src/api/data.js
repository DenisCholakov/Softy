import * as api from "./api.js";

export const login = api.login;
export const logout = api.logout;
export const register = api.register;

export async function getAllAlbums() {
  return api.get("/data/albums?sortBy=_createdOn%20desc&distinct=name");
}

export async function getAlbumsByName(name) {
  return api.get(`/data/albums?where=name%20LIKE%20%22${name}%22`);
}

export async function getAlbumDetails(id) {
  return api.get("/data/albums/" + id);
}

export async function editAlbum(id, album) {
  return api.put("/data/albums/" + id, album);
}

export async function createAlbum(album) {
  return api.post("/data/albums", album);
}

export async function deleteAlbum(id) {
  return api.del("/data/albums/" + id);
}
