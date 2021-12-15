import { showSection, updateUsernav } from "./dom.js";
import { showHomePage } from "./home.js";

const loginSection = document.getElementById("loginSection");
loginSection.remove();
const form = loginSection.querySelector("form");
form.addEventListener("submit", onSubmit);

export function showLoginPage() {
  showSection(loginSection);
}

async function onSubmit(ev) {
  ev.preventDefault();

  const formData = new FormData(form);

  const email = formData.get("email");
  const password = formData.get("password");

  try {
    const res = await fetch("http://localhost:3030/users/login", {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password }),
    });

    if (!res.ok) {
      const err = await res.json();
      throw new Error(err.message);
    }

    const data = await res.json();
    const userData = {
      username: data.username,
      id: data._id,
      token: data.accessToken,
    };

    sessionStorage.setItem("userData", JSON.stringify(userData));

    updateUsernav();
    showHomePage();
  } catch (err) {
    alert(err.message);
  }
}
