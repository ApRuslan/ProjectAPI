using System.ComponentModel.DataAnnotations;

namespace planningsoverzicht.ViewModels.LidmaatschapVM
{
    public class LidmaatschapCVM
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Rol { get; set; }
        public bool GevoeligeInfoBekijken { get; set; }
        public bool GereserveerdeInfoBekijken { get; set; }
    }
}
