import { e, showSection, updateUsernav } from "./dom.js";
import { showHomePage } from "./home.js";

const registerSection = document.getElementById("registerSection");
registerSection.remove();
const form = registerSection.querySelector("form");
form.addEventListener("submit", onSubmit);

export function showRegisterPage() {
  showSection(registerSection);
}

async function onSubmit(ev) {
  ev.preventDefault();

  const formData = new FormData(form);

  const email = formData.get("email").trim();
  const password = formData.get("password").trim();
  const repass = formData.get("repass").trim();

  if (password !== repass) {
    return alert("Passwords don't match");
  }

  try {
    const res = await fetch("http://localhost:3030/users/register", {
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
