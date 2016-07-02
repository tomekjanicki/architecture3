namespace Architecture3.WebApi.Client.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        private static void Main()
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Press any key to start");
            Console.ReadLine();
            try
            {
                Console.WriteLine("Executing ...");
                var client = new Client(new Uri("http://localhost:2776/"));
                var result = await client.ProductsFilterPaged(10, 5, string.Empty, string.Empty).ConfigureAwait(false);
                Console.WriteLine("Executed with count {0} and items {1}", result.Count, result.Items.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during executing: {0}", ex);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
