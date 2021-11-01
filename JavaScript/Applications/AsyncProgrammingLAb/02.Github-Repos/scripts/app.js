function loadRepos() {
  const name = document.getElementById("username").value;
  const url = `https://api.github.com/users/${name}/repos`;
  const reposList = document.getElementById("repos");

  fetch(url)
    .then((res) => {
      if (!res.ok) {
        throw new Error(`${res.status} ${res.statusText}`);
      }

      return res.json();
    })
    .then(showRepos)
    .catch(handleError);

  function showRepos(data) {
    reposList.replaceChildren([]);

    if (data.length === 0) {
      throw Error("No repos found.");
    }

    for (const repo of data) {
      const li = document.createElement("li");
      const anchor = document.createElement("a");
      anchor.href = repo.html_url;
      anchor.textContent = repo.full_name;

      li.appendChild(anchor);
      reposList.appendChild(li);
    }
  }

  function handleError(error) {
    reposList.replaceChildren([]);
    reposList.textContent = `${error.message}`;
  }
}
