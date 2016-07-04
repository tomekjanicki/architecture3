namespace Architecture3.WebApi.Client.Interfaces
{
    using System.Threading.Tasks;
    using Architecture3.WebApi.Dtos;
    using Architecture3.WebApi.Dtos.Product.FilterPaged;

    public interface IClient
    {
        Task<Paged<Product>> ProductsFilterPaged(int top, int skip, string filter, string orderBy);

        Task<Dtos.Product.Get.Product> ProductsGet(int id);

        Task ProductsDelete(int id, string version);

        Task<string> VersionGet();
    }
}