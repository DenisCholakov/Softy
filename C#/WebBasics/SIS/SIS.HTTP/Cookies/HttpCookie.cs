using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookie
    {
        private const int HttpCookieDefaoultExpirationDays = 3;
        private const string HttpCookieDefaultPath = "/";

        public HttpCookie(string key, string value, 
            int expires = HttpCookieDefaoultExpirationDays, string path = HttpCookieDefaultPath)
        {
            this.Key = key;
            this.Value = value;
            this.Expires = DateTime.Now.AddDays(expires);
            this.Path = path;
        }

        public HttpCookie(string key, string value, bool isNew,
            int expires = HttpCookieDefaoultExpirationDays, string path = HttpCookieDefaultPath)
                :this(key, value, expires, path)
        {
            this.IsNew = isNew;
        }

        public string Key { get; set; }

        public string Value { get; set; }

        public DateTime Expires { get; private set; }

        public string Path { get; set; }

        public bool IsNew { get; set; }

        public bool HttpOnly { get; set; } = true;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"{this.Key}={this.Value}; Expires={this.Expires:R}");

            if (this.HttpOnly)
            {
                sb.Append("; HttpOnly");
            }

            sb.Append($"; Path={this.Path}");

            return sb.ToString();
        }

        public void Delete()
        {
            this.Expires = DateTime.Now;
        }
    }
}
