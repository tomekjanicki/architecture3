namespace Architecture3.Logic.CQ.Apis.Product.FilterPaged
{
    using System.Collections.Generic;
    using System.Linq;
    using Architecture3.Types;

    public static class Columns
    {
        public static IReadOnlyDictionary<NonEmptyString, NonEmptyString> GetMappings()
        {
            return new Dictionary<NonEmptyString, NonEmptyString>
            {
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Id), (NonEmptyString)"ID" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Code), (NonEmptyString)"CODE" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Name), (NonEmptyString)"NAME" },
                { (NonEmptyString)nameof(Web.Dtos.Apis.Product.FilterPaged.Product.Price), (NonEmptyString)"PRICE" }
            };
        }

        public static IReadOnlyCollection<NonEmptyString> GetAllowedColumns()
        {
            return GetMappings().Keys.ToList();
        }
    }
}
