namespace planningsoverzicht.Entities
{
    public class Team
    {
        public string Naam { get; set; }
        public int Id { get; set; }
        public int Aanmaker { get; set; }
        public List<Lidmaatschap> Ledden { get; set; }
        
        public Team(string naam, int aanmaker)
        {
            Naam = naam;
            Aanmaker = aanmaker;
            Ledden = new List<Lidmaatschap>();
        }
    }
}
