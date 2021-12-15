import { clearUserData, getUserData, setUserData } from "../util.js";

const hostName = "http://localhost:3030";
async function request(url, options) {
  try {
    const res = await fetch(hostName + url, options);

    if (res.ok == false) {
      if (res.status == 403) {
        clearUserData();
      }

      const error = await res.json();
      throw new Error(error.message);
    }

    if (res.status == 204) {
      return res;
    }

    return res.json();
  } catch (error) {
    alert(error.message);
    throw error;
  }
}

function createOptions(method = "get", data) {
  const options = {
    method,
    headers: {},
  };

  if (data !== undefined) {
    options.headers["Content-Type"] = "application/json";
    options.body = JSON.stringify(data);
  }

  const userData = getUserData();

  if (userData != null) {
    options.headers["X-Authorization"] = userData.token;
  }

  return options;
}

export async function get(url) {
  return request(url, createOptions());
}

export async function post(url, data) {
  return request(url, createOptions("post", data));
}

export async function put(url, data) {
  return request(url, createOptions("put", data));
}

export async function del(url) {
  return request(url, createOptions("delete"));
}

export async function login(email, password) {
  const result = await post("/users/login", { email, password });

  setUserData({
    email: result.email,
    id: result._id,
    token: result.accessToken,
  });

  return result;
}

export async function register(email, password) {
  const result = await post("/users/register", { email, password });

  setUserData({
    email: result.email,
    id: result._id,
    token: result.accessToken,
  });

  return result;
}

export async function logout() {
  await get("/users/logout");
  clearUserData();
}
