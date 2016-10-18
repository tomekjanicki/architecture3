namespace Architecture3.Db
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using DbUp;
    using log4net;

    internal static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        private static int Main()
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Main"].ConnectionString;

                var upgrader = DeployChanges.To.SqlDatabase(connectionString).WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly()).LogToConsole().Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    Logger.Error(result.Error);
                    DispalyMessage(ConsoleColor.Red, result.Error.ToString());
                    return -1;
                }

                DispalyMessage(ConsoleColor.Green, "Success");
                Logger.Info("Success");
                return 0;
            }
            catch (Exception exception)
            {
                Logger.Error("Unhadled exception", exception);
                return -2;
            }
        }

        private static void DispalyMessage(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
