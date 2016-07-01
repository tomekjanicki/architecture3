namespace Architecture3.Logic.Product.FilterPaged
{
    using System.Collections.Generic;
    using Architecture3.Common.Handlers.Interfaces;
    using Architecture3.Common.ValueObjects;

    public class QueryHandler : IRequestHandler<Query, Paged<Product>>
    {
        public Paged<Product> Handle(Query message)
        {
            // todo do sth
            return Paged<Product>.Create(10, new List<Product>()).Value;
        }
    }
}
