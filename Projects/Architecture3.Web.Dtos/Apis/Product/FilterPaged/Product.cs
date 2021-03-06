﻿namespace Architecture3.Web.Dtos.Apis.Product.FilterPaged
{
    using System;

    public class Product
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime? Date { get; set; }

        public bool CanDelete { get; set; }

        public string Version { get; set; }
    }
}
