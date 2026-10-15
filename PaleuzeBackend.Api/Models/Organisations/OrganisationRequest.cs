using System.ComponentModel.DataAnnotations;

namespace PaleuzeBackend.Api.Models.Organisations
{
    public record OrganisationRequest
    {
        [MinLength(5)]
        [MaxLength(50)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Description { get; set; }
    }
}
