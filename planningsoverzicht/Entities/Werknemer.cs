namespace planningsoverzicht.Entities
{
    public class Werknemer
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Boolean EigenGereserveerdeInfo { get; set; }
        public Boolean Beheerder { get; set; }
        public Boolean WeekVerwittiging { get; set; }
        public string KalenderStandaardTeam { get; set; }
        public List<Afspraak> Agenda { get; set; }

        public Werknemer(string name, string email, bool eigenGereseveerdeInfo, bool beheerder, bool weekVerwittiging, string kalenderStandaardTeam)
        {
            Name = name;
            Email = email;
            EigenGereserveerdeInfo = eigenGereseveerdeInfo;
            Beheerder = beheerder;
            WeekVerwittiging = weekVerwittiging;
            KalenderStandaardTeam = kalenderStandaardTeam;
            Agenda = new List<Afspraak>();
        }
    }
}
