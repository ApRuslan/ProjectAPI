using System.ComponentModel.DataAnnotations;

namespace planningsoverzicht.ViewModels.WerknemerVM
{
    public class WerknemerUVM
    {
        [Required]
        public string Email { get; set; }
        public Boolean EigenGereserveerdeInfo { get; set; }
        public Boolean Beheerder { get; set; }
        public Boolean WeekVerwittiging { get; set; }
        public string KalenderStandaardTeam { get; set; }
    }
}
