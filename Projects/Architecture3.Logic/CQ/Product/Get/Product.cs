// ReSharper disable UnassignedGetOnlyAutoProperty
namespace Architecture3.Logic.CQ.Product.Get
{
    using System;

    public sealed class Product
    {
        public int Id { get;  }

        public string Code { get;  }

        public string Name { get;  }

        public decimal Price { get;  }

        public DateTime? Date { get;  }

        public bool CanDelete { get;  }

        public string Version => Convert.ToBase64String(VersionPrivate);

        private byte[] VersionPrivate { get; }
    }
}
