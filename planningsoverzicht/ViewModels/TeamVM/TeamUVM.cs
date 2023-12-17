using System.ComponentModel.DataAnnotations;

namespace planningsoverzicht.ViewModels.TeamVM
{
    public class TeamUVM
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public int Aanmaker { get; set; }
    }
}
