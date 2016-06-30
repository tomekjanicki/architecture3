namespace Architecture3.WebApi.Client.Test
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        private static void Main()
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            try
            {
                var client = new Client(new Uri("http://localhost:2776/"));
                var result = await client.ProductsFilterPaged(10, 5, string.Empty, string.Empty).ConfigureAwait(false);
                Console.WriteLine("{0} {1}", result.Count, result.Items.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
