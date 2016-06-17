namespace Architecture3.Common.SharedStructs.ResponseParams
{
    using System.Collections.Generic;

    public class CollectionResult<TItem> : Result<IReadOnlyCollection<TItem>>
    {
        public CollectionResult(IReadOnlyCollection<TItem> results)
            : base(results)
        {
        }
    }
}