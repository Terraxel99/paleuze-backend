using System.ComponentModel.DataAnnotations;

namespace PaleuzeBackend.Api.Models
{
    public record TournamentRequest
    {
        [Required]
        public required string Name { get; set; }
    }
}