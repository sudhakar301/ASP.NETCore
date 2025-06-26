namespace SampleASPDotNetCore.Exceptions
{
    public class MValidationAppException : Exception
    {
        public IReadOnlyDictionary<string, string[]> errors { get; }
        public MValidationAppException(IReadOnlyDictionary<string, string[]> errors) : base("Validation error(s) occurred.")
        {
            this.errors = errors;
        }
    }
}
