import { showCatalogPage } from "./catalog.js";
import { updateUsernav } from "./dom.js";
import { showAboutPage, showHomePage } from "./home.js";
import { showLoginPage } from "./login.js";
import { showRegisterPage } from "./register.js";

document.getElementById("logoutBtn").addEventListener("click", onLogout);
document.querySelector("nav").addEventListener("click", onNavigate);

const sections = {
  homeBtn: showHomePage,
  catalogBtn: showCatalogPage,
  aboutBtn: showAboutPage,
  loginBtn: showLoginPage,
  registerBtn: showRegisterPage,
};

showHomePage();
updateUsernav();

function onNavigate(ev) {
  if (ev.target.tagName === "A") {
    const view = sections[ev.target.id];
    if (typeof view === "function") {
      ev.preventDefault();
      view();
    }
  }
}

async function onLogout(ev) {
  ev.preventDefault();
  ev.stopImmediatePropagation();

  const { token } = JSON.parse(sessionStorage.getItem("userData"));

  await fetch("http://localhost:3030/users/logout", {
    headers: {
      "X-Authorization": token,
    },
  });

  sessionStorage.removeItem("userData");
  updateUsernav();
  showHomePage();
}
