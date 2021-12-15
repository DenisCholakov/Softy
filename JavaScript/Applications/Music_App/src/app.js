import { page, render } from "./lib.js";
import { homePage } from "./views/home.js";
import { catalogPage } from "./views/catalog.js";
import { loginPage } from "./views/login.js";
import { registerPage } from "./views/register.js";
import { logout } from "./api/data.js";
import { getUserData } from "./util.js";
import { createPage } from "./views/create.js";
import { detailsPage } from "./views/details.js";
import { editPage } from "./views/edit.js";
import { searchPage } from "./views/search.js";

const root = document.getElementById("main-content");
document.getElementById("logoutBtn").addEventListener("click", onLogout);

page(decorateContext);
page("/", homePage);
page("/home", homePage);
page("/catalog", catalogPage);
page("/login", loginPage);
page("/register", registerPage);
page("/create", createPage);
page("/details/:id", detailsPage);
page("/edit/:id", editPage);
page("/search", searchPage);

updateUserNav();
page.start();

function decorateContext(ctx, next) {
  ctx.render = (context) => render(context, root);
  ctx.updateUserNav = updateUserNav;

  next();
}

function onLogout() {
  logout();
  updateUserNav();
  page.redirect("/");
}

function updateUserNav() {
  const userData = getUserData();

  if (userData) {
    document.getElementById("user-nav").style.display = "inline-block";
    document.getElementById("guest-nav").style.display = "none";
  } else {
    document.getElementById("user-nav").style.display = "none";
    document.getElementById("guest-nav").style.display = "inline-block";
  }
}
