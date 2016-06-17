namespace Architecture3.Common.FluentValidation
{
    using System.Collections.Generic;
    using global::FluentValidation;
    using global::FluentValidation.Results;

    public abstract class AbstractClassValidator<T> : AbstractValidator<T>
        where T : class
    {
        public override ValidationResult Validate(T instance)
        {
            return instance == null ? new ValidationResult(new List<ValidationFailure> { new ValidationFailure(string.Empty, "argument is null") }) : base.Validate(instance);
        }
    }
}
