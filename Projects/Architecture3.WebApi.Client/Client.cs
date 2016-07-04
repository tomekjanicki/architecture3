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

        public async Task<Dtos.Product.Get.Product> ProductsGet(int id)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var uri = new Uri(_baseUri, $"/products/{id}");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<Dtos.Product.Get.Product>().ConfigureAwait(false);
            }
        }

        public async Task ProductsDelete(int id, string version)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var parameters = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>("version", version)
                };
                var uri = new Uri(_baseUri, $"/products/{id}{Helper.GetEncodedParametersString(parameters)}");
                var response = await client.DeleteAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<string> VersionGet()
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var uri = new Uri(_baseUri, "/version");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }
    }
}