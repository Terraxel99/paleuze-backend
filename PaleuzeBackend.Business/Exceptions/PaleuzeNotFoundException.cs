namespace PaleuzeBackend.Business.Exceptions
{
    public class PaleuzeNotFoundException : PaleuzeException
    {
        public PaleuzeNotFoundException(string message) : base(message)
        { }
    }
}
