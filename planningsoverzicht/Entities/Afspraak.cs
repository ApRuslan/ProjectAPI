namespace planningsoverzicht.Entities
{
    public class Afspraak
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Date { get; set; }
        public string Titel { get; set; }
        public Boolean IsGevoelig { get; set; }
        public Boolean IsGereserveerd{ get; set; }
        public Boolean WijzigingGevraagd{ get; set; }
        public string? VervangWoord{ get; set; }

        public Afspraak(DateTime date, string titel, bool isGevoelig, bool isGereserveerd, bool wijzigingGevraagd, string? vervangWoord = null)
        {
            DayOfWeek = date.DayOfWeek;
            Date = date;
            Titel = titel;
            IsGevoelig = isGevoelig;
            IsGereserveerd = isGereserveerd;
            WijzigingGevraagd = wijzigingGevraagd;
            VervangWoord = vervangWoord;
        }
    }
}
