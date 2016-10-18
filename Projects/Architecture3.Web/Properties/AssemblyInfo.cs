using System.Reflection;
using System.Web;
using Architecture3.Web;
[assembly: AssemblyTitle("Architecture3.Web")]
[assembly: AssemblyProduct("Architecture3.Web")]
[assembly: PreApplicationStartMethod(typeof(Startup), nameof(Startup.Start))]
