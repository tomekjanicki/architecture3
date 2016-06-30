namespace Architecture3.WebApi.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Architecture3.WebApi.Client.Interfaces;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;

    public class Client : IClient
    {
        private readonly Uri _baseUri;

        public Client(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<Paged<Product>> ProductsFilterPaged(int top, int skip, string filter, string orderBy)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var parameters = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("top", top.ToString()),
                    new Tuple<string, string>("skip", skip.ToString()),
                    new Tuple<string, string>("filter", filter),
                    new Tuple<string, string>("orderBy", orderBy)
                };
                var uri = new Uri(_baseUri, $"/products/{Helper.GetEncodedParametersString(parameters)}");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Paged<Product>>().ConfigureAwait(false);
            }
        }
    }
}