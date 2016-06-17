namespace Architecture3.WebApi
{
    using FluentValidation;

    public static class RegisterValidation
    {
        public static void Execute()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}