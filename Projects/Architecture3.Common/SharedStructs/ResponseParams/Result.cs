namespace Architecture3.Common.SharedStructs.ResponseParams
{
    public class Result<TItem>
    {
        public Result(TItem results)
        {
            Results = results;
        }

        public TItem Results { get; }
    }
}