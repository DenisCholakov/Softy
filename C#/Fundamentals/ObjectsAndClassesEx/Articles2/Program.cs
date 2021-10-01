using System;
using System.Collections.Generic;
using System.Linq;

namespace Articles2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Article> articles = new List<Article>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] article = Console.ReadLine().Split(", ").ToArray();
                articles.Add(new Article(article[0], article[1], article[2]));
            }

            string criteria = Console.ReadLine();

            if (criteria == "title")
            {
                System.Console.WriteLine(String.Join(Environment.NewLine, articles.OrderBy(x => x.Title)));
            }
            else if (criteria == "content")
            {
                System.Console.WriteLine(String.Join(Environment.NewLine, articles.OrderBy(x => x.Content)));
            }
            else
            {
                System.Console.WriteLine(String.Join(Environment.NewLine, articles.OrderBy(x => x.Author)));
            }
        }
    }

    public class Article
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }

        public Article(string title, string content, string author)
        {
            this.Title = title;
            this.Content = content;
            this.Author = author;
        }

        public override string ToString() => $"{this.Title} - {this.Content}: {this.Author}";
    }
}
