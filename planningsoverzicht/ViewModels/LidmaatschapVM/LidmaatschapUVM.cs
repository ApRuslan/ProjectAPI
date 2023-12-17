using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace planningsoverzicht.ViewModels.LidmaatschapVM
{
    public class LidmaatschapUVM
    {
        public string Rol { get; set; }
        [Required]
        public bool GevoeligeInfoBekijken { get; set; }
        public bool GereserveerdeInfoBekijken { get; set; }
    }
}
