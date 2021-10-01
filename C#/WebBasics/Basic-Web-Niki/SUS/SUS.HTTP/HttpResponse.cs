﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
        }

        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            this.StatusCode = statusCode;
            this.Body = body ?? throw new ArgumentNullException(nameof(body));

            this.Headers = new List<Header>()
            {
                new Header("Content-Type", contentType),
                new Header("Content-Length", body.Length.ToString())
            };

            this.Cookies = new List<Cookie>();
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int) this.StatusCode} {this.StatusCode}" + HTTPConstants.NewLine);

            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header + HTTPConstants.NewLine);
            }

            foreach (var cookie in Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HTTPConstants.NewLine);
            }

            responseBuilder.Append(HTTPConstants.NewLine);

            return responseBuilder.ToString();
        }
    }
}
