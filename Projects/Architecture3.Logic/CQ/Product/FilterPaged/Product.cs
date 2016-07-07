namespace Architecture3.Logic.CQ.Product.FilterPaged
{
    using System;
    using Architecture3.Logic.CQ.Product.ValueObjects;
    using Architecture3.Types;

    public class Product
    {
        public Product()
        {
            VersionPrivate = new byte[] { 1 };
        }

        public NonNegativeInt Id { get; set; }

        public Code Code { get; set; }

        public Name Name { get; set; }

        public NonNegativeDecimal Price { get; set; }

        public DateTime? Date { get; set; }

        public bool CanDelete { get; set; }

        public NonEmptyString Version => (NonEmptyString)Convert.ToBase64String(VersionPrivate);

        protected byte[] VersionPrivate { get; set; }
    }
}
