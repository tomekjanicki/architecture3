namespace Architecture3.WebApi.Dtos
{
    using System.Collections.Generic;

    public class Paged<T>
    {
        public Paged(int count, List<T> items)
        {
            Count = count;
            Items = items;
        }

        public int Count { get; private set; }

        public List<T> Items { get; private set; }
    }
}