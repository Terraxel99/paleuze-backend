namespace PaleuzeBackend.Business.Exceptions.Authentication
{
    public class TournamentNotFoundException : PaleuzeException
    {
        public TournamentNotFoundException(Guid tournamentId)
            : base($"Tournament with ID \"{tournamentId}\" does not exist.")
        { }
    }
}
