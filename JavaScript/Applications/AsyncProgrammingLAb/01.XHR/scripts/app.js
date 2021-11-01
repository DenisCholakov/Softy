function loadRepos() {
  const url = "https://api.github.com/users/testnakov/repos";
  const httpRequest = new XMLHttpRequest();
  const resourses = document.getElementById("res");
  httpRequest.addEventListener("readystatechange", function () {
    if (httpRequest.readyState === 4 && httpRequest.status === 200) {
      resourses.replaceChildren([]);
      const data = JSON.parse(httpRequest.responseText);
      const ul = document.createElement("ul");
      for (const repo of data) {
        const li = document.createElement("li");
        li.innerHTML = `<a href="${repo.html_url}">${repo.name}</a>`;
        ul.appendChild(li);
      }

      resourses.appendChild(ul);
    }
  });

  httpRequest.open("GET", url);
  httpRequest.send();
}
