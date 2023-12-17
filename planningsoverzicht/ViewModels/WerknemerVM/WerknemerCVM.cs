using planningsoverzicht.Entities;
using System.ComponentModel.DataAnnotations;

namespace planningsoverzicht.ViewModels.WerknemerVM
{
    public class WerknemerCVM
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EigenGereserveerdeInfo { get; set; }
        public bool Beheerder { get; set; }
        public bool WeekVerwittiging { get; set; }
        public string KalenderStandaardTeam { get; set; }
    }
}
