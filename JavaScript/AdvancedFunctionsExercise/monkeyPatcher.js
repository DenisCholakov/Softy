function result(command) {
  const fuctionInvoker = {
    upvote,
    downvote,
    score,
  };

  return fuctionInvoker[command].apply(this);

  function score() {
    let upvotes = this.upvotes;
    let downvotes = this.downvotes;

    if (downvotes + upvotes > 50) {
      const incrementor = Math.ceil(Math.max(downvotes, upvotes) * 0.25);

      downvotes += incrementor;
      upvotes += incrementor;
    }

    let rating = "new";

    if (upvotes + downvotes > 10) {
      if (upvotes > (downvotes + upvotes) * 0.66) {
        rating = "hot";
      } else if (upvotes - downvotes < 0) {
        rating = "unpopular";
      } else if (upvotes + downvotes > 100) {
        rating = "controversial";
      }
    }

    return [upvotes, downvotes, upvotes - downvotes, rating];
  }

  function upvote() {
    this.upvotes++;
  }

  function downvote() {
    this.downvotes++;
  }
}

let post = {
  id: "3",
  author: "emil",
  content: "wazaaaaa",
  upvotes: 100,
  downvotes: 100,
};

result.call(post, "upvote");
result.call(post, "downvote");
let score = result.call(post, "score"); // [127, 127, 0, 'controversial']
result.call(post, "downvote"); // (executed 50 times)
score = result.call(post, "score"); // [139, 189, -50, 'unpopular']
