namespace PaleuzeBackend.Business.Exceptions
{
    public abstract class PaleuzeException : Exception
    {
        protected PaleuzeException(string message)
            : base(message) { }
    }
}
