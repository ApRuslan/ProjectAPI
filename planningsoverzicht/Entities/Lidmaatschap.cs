namespace planningsoverzicht.Entities
{
    public class Lidmaatschap
    {
        public int UserId { get; set; }
        public string Rol { get; set; }
        public Boolean GevoeligeInfoBekijken { get; set; }
        public Boolean GereserveerdeInfoBekijken { get; set; }

        public Lidmaatschap(int userId, string rol, bool gevoeligeInfoBekijken, bool gereserveerdeInfoBekijken)
        {
            UserId = userId;
            Rol = rol;
            GevoeligeInfoBekijken = gevoeligeInfoBekijken;
            GereserveerdeInfoBekijken = gereserveerdeInfoBekijken;
        }
    }
}
