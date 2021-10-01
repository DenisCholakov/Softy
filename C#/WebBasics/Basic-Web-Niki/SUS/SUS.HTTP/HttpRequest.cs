using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();

            var lines = requestString
                .Split(HTTPConstants.NewLine, StringSplitOptions.None);

            var headerLine = lines[0];
            var headerLineParts = headerLine.Split(' ');
            string line = "";
            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), headerLineParts[0], true);
            this.Path = headerLineParts[1];

            int lineIndex = 1;

            while (lineIndex < lines.Length)
            {
                line = lines[lineIndex];

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                this.Headers.Add(new Header(line));
                
                lineIndex++;
            }

            var sb = new StringBuilder();

            while (lineIndex < lines.Length)
            {
                line = lines[lineIndex];

                sb.AppendLine(line);
                lineIndex++;
            }

            this.Body = sb.ToString().TrimEnd();

            if (this.Headers.Any(x => x.Name == HTTPConstants.CookieHeader))
            {
                var cookiesAsString =
                    this.Headers.FirstOrDefault(x => x.Name == HTTPConstants.CookieHeader).Value;
                var cookies = cookiesAsString.Split(new string[] {"; "}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    this.Cookies.Add(new Cookie(cookie));
                }
            }
        }

        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public string Body { get; set; }
    }
}
