namespace Architecture3.Web.Dtos
{
    using System.Collections.Generic;

    public class Paged<T>
    {
        public Paged(int count, IEnumerable<T> items)
        {
            Count = count;
            Items = items;
        }

        public int Count { get; private set; }

        public IEnumerable<T> Items { get; private set; }
    }
}