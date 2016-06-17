namespace Architecture3.WebApi.Dtos.Product
{
    using System;

    public class ProductItem
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime? Date { get; set; }

        public bool CanDelete { get; set; }

        public byte[] Version { get; set; }
    }
}
