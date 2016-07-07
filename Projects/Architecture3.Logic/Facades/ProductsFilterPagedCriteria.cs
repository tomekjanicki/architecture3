namespace Architecture3.Logic.Facades
{
    public class ProductsFilterPagedCriteria
    {
        public ProductsFilterPagedCriteria(string name, string code, string orderBy)
        {
            Name = name;
            Code = code;
            OrderBy = orderBy;
        }

        public string Name { get; }

        public string Code { get; }

        public string OrderBy { get; }
    }
}