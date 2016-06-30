﻿namespace Architecture3.WebApi.Client.Interfaces
{
    using System.Threading.Tasks;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;

    public interface IClient
    {
        Task<Paged<Product>> ProductsFilterPaged(int top, int skip, string filter, string orderBy);
    }
}