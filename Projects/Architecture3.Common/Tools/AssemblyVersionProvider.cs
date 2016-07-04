namespace Architecture3.Common.Tools
{
    using System;
    using System.Reflection;
    using Architecture3.Common.Tools.Interfaces;

    public sealed class AssemblyVersionProvider : IAssemblyVersionProvider
    {
        public Version Get(Assembly assembly)
        {
            return assembly.GetName().Version;
        }
    }
}
