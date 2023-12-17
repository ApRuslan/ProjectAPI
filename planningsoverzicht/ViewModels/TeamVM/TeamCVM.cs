using System.ComponentModel.DataAnnotations;

namespace planningsoverzicht.ViewModels.TeamVM
{
    public class TeamCVM
    {
        [Required]
        public string Naam { get; set; }
        [Required]
        public int Aanmaker { get; set; }
    }
}
