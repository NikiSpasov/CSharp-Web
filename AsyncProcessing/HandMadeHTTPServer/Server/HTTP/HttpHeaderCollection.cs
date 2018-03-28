namespace MyCoolWebServer.Server.HTTP
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Common;

    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly IDictionary<string, ICollection<HttpHeader>> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, ICollection<HttpHeader>>();
        }

        public void Add(HttpHeader header)
        {
            CoreValidator.ThrowIfNull(header, nameof(header));

            var headerKey = header.Key;

            if (!this.headers.ContainsKey(headerKey))
            {
                this.headers[headerKey] = new List<HttpHeader>();
            }

            this.headers[headerKey].Add(header);
        }

        public void Add(string key, string value)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));
            CoreValidator.ThrowIfNull(value, nameof(value));


            if (!this.headers.ContainsKey(key))
            {
                this.headers[key] = new List<HttpHeader>();
            }

            this.headers[key].Add(new HttpHeader(key, value));

        }

        public bool ContainsKey(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            return this.headers.ContainsKey(key);
        }

        public IEnumerator<ICollection<HttpHeader>> GetEnumerator()
            => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.headers.Values.GetEnumerator();

        public ICollection<HttpHeader> Get(string key)
        {
            CoreValidator.ThrowIfNull(key, nameof(key));

            if (!this.headers.ContainsKey(key))
            {
                throw new InvalidOperationException($"The given key {key} does not present in the headers collection");
            }

            return this.headers[key];
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var header in this.headers)
            {
                var headerKey = header.Key;

                foreach (var headerValue in header.Value)
                {
                    result.AppendLine($"{headerKey}: {headerValue.Value}");
                }
            }

            return result.ToString().Trim();
        }

    }
}
