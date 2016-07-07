namespace Architecture3.WebApi.Client.Interfaces
{
    using System.Threading.Tasks;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;

    public interface IClient
    {
        Task<Result<Paged<Product>, NonEmptyString>> ProductsFilterPaged(int top, int skip, string filter, string orderBy);

        Task<Result<Dtos.Product.Get.Product, NonEmptyString>> ProductsGet(int id);

        Task<Result<NonEmptyString>> ProductsDelete(int id, NonEmptyString version);

        Task<Result<NonEmptyString, NonEmptyString>> VersionGet();

        Task<Result<NonEmptyString>> ProductsPut(int id, Dtos.Product.Put.Product product);
    }
}