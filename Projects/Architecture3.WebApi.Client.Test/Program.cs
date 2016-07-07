namespace Architecture3.WebApi.Client.Test
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Architecture3.Types;
    using Architecture3.Types.FunctionalExtensions;

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
                var versionResult = await client.VersionGet().ConfigureAwait(false);
                if (versionResult.IsSuccess)
                {
                    Console.WriteLine("Version {0}", versionResult.Value);
                }
                else
                {
                    PrintError(nameof(Client.VersionGet), versionResult);
                }

                var productsFilterPagedResult = await client.ProductsFilterPaged(10, 5, string.Empty, string.Empty).ConfigureAwait(false);
                if (productsFilterPagedResult.IsSuccess)
                {
                    var value = productsFilterPagedResult.Value;
                    Console.WriteLine("Executed with count {0} and items {1}", value.Count, value.Items.Count());
                }
                else
                {
                    PrintError(nameof(Client.ProductsFilterPaged), productsFilterPagedResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during executing: {0}", ex);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }

        private static void PrintError<T>(string method, Result<T, NonEmptyString> result)
        {
            Console.WriteLine("{0}: {1}", method, result.Error);
        }
    }
}
