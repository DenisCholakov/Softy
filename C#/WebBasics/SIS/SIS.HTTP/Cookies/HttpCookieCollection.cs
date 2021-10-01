using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookiesCollection;

        public HttpCookieCollection()
        {
            this.cookiesCollection = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            this.cookiesCollection.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            return this.cookiesCollection.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            if (!this.cookiesCollection.ContainsKey(key))
            {
                throw new ArgumentException("The required cookie is not in the collection.");
            }

            return this.cookiesCollection[key];
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool HasCookies()
        {
            return this.cookiesCollection.Count != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
