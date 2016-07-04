namespace Architecture3.Logic.CQ.TemplateMethods.Commands.Interfaces
{
    using Architecture3.Common.ValueObjects;

    public interface IDeleteCommand
    {
        IdVersion IdVersion { get; }
    }
}