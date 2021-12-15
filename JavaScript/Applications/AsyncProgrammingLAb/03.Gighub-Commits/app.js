function loadCommits() {
  const username = document.getElementById("username").value;
  const repository = document.getElementById("repo").value;
  const commits = document.getElementById("commits");
  const url = `https://api.github.com/repos/${username}/${repository}/commits`;

  fetch(url)
    .then((res) => {
      if (!res.ok || res.status !== 200) {
        console.log(res);
        throw new Error(`${res.status} ${res.statusText}`);
      }

      return res.json();
    })
    .then(handleResponse)
    .catch(handleError);

  function handleResponse(data) {
    commits.replaceChildren([]);

    if (data.length === 0) {
      throw new Error("No commits were found.");
    }

    for (const commit of data) {
      const liElement = document.createElement("li");
      liElement.textContent = `${commit.commit.author.name}: ${commit.commit.message}`;
      commits.appendChild(liElement);
    }
  }

  function handleError(error) {
    commits.replaceChildren([]);
    const errorLi = document.createElement("li");
    errorLi.textContent = error.message;

    commits.appendChild(errorLi);
  }
}
