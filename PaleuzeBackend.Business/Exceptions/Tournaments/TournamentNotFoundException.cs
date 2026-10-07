namespace PaleuzeBackend.Business.Exceptions.Tournaments
{
    public class TournamentNotFoundException : PaleuzeException
    {
        public TournamentNotFoundException(Guid tournamentId)
            : base($"Tournament with ID \"{tournamentId}\" does not exist.")
        { }
    }
}
