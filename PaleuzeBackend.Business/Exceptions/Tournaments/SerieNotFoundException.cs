namespace PaleuzeBackend.Business.Exceptions.Tournaments
{
    public class SerieNotFoundException : PaleuzeException
    {
        public SerieNotFoundException(Guid serieId)
            : base($"Serie with ID \"{serieId}\" does not exist.")
        { }
    }
}
