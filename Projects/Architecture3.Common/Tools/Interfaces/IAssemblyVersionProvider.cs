namespace Architecture3.Common.Tools.Interfaces
{
    using System;
    using System.Reflection;

    public interface IAssemblyVersionProvider
    {
        Version Get(Assembly assembly);
    }
}