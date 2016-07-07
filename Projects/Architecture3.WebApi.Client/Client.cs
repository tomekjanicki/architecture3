namespace Architecture3.WebApi.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Architecture3.WebApi.Client.Interfaces;
    using Architecture3.WebApi.Dtos;
    using Types;
    using Types.FunctionalExtensions;

    public class Client : IClient
    {
        private readonly Uri _baseUri;

        public Client(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<Result<Paged<Dtos.Product.FilterPaged.Product>, NonEmptyString>> ProductsFilterPaged(int top, int skip, Maybe<string> filter, Maybe<string> orderBy)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var parameters = new List<Tuple<NonEmptyString, Maybe<string>>>
                {
                    new Tuple<NonEmptyString, Maybe<string>>((NonEmptyString)"top", top.ToString()),
                    new Tuple<NonEmptyString, Maybe<string>>((NonEmptyString)"skip", skip.ToString()),
                    new Tuple<NonEmptyString, Maybe<string>>((NonEmptyString)"filter", filter),
                    new Tuple<NonEmptyString, Maybe<string>>((NonEmptyString)"orderBy", orderBy)
                };
                var uri = new Uri(_baseUri, $"/products/{Helper.GetEncodedParametersString(parameters)}");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return Result<Paged<Dtos.Product.FilterPaged.Product>, NonEmptyString>.Fail(await Helper.GetErrorMessage(response).ConfigureAwait(false));
                }

                var data = await response.Content.ReadAsAsync<Paged<Dtos.Product.FilterPaged.Product>>().ConfigureAwait(false);

                return Result<Paged<Dtos.Product.FilterPaged.Product>, NonEmptyString>.Ok(data);
            }
        }

        public async Task<Result<Dtos.Product.Get.Product, NonEmptyString>> ProductsGet(int id)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var uri = new Uri(_baseUri, $"/products/{id}");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return Result<Dtos.Product.Get.Product, NonEmptyString>.Fail(await Helper.GetErrorMessage(response).ConfigureAwait(false));
                }

                var data = await response.Content.ReadAsAsync<Dtos.Product.Get.Product>().ConfigureAwait(false);

                return Result<Dtos.Product.Get.Product, NonEmptyString>.Ok(data);
            }
        }

        public async Task<Result<NonEmptyString>> ProductsDelete(int id, NonEmptyString version)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var parameters = new List<Tuple<NonEmptyString, Maybe<string>>>
                {
                    new Tuple<NonEmptyString, Maybe<string>>((NonEmptyString)"version", version.Value)
                };
                var uri = new Uri(_baseUri, $"/products/{id}{Helper.GetEncodedParametersString(parameters)}");
                var response = await client.DeleteAsync(uri).ConfigureAwait(false);
                return response.IsSuccessStatusCode ? Result<NonEmptyString>.Ok() : Result<NonEmptyString>.Fail(await Helper.GetErrorMessage(response).ConfigureAwait(false));
            }
        }

        public async Task<Result<NonEmptyString, NonEmptyString>> VersionGet()
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var uri = new Uri(_baseUri, "/version");
                var response = await client.GetAsync(uri).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                {
                    return Result<NonEmptyString, NonEmptyString>.Fail(await Helper.GetErrorMessage(response).ConfigureAwait(false));
                }

                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var dataResult = NonEmptyString.Create(data, (NonEmptyString)"Field");

                if (dataResult.IsFailure)
                {
                    return Result<NonEmptyString, NonEmptyString>.Fail(dataResult.Error);
                }

                return Result<NonEmptyString, NonEmptyString>.Ok(dataResult.Value);
            }
        }

        public async Task<Result<NonEmptyString>> ProductsPut(int id, Dtos.Product.Put.Product product)
        {
            using (var client = Helper.GetConfiguredHttpClient())
            {
                var uri = new Uri(_baseUri, $"/products/{id}");
                var response = await client.PutAsJsonAsync(uri, product).ConfigureAwait(false);
                return response.IsSuccessStatusCode ? Result<NonEmptyString>.Ok() : Result<NonEmptyString>.Fail(await Helper.GetErrorMessage(response).ConfigureAwait(false));
            }
        }
    }
}