using planningsoverzicht.DB;
using planningsoverzicht.Entities;

namespace planningsoverzicht.Services
{
    public interface IWerknemerData
    {
        IEnumerable<Werknemer> GetAll();
        Werknemer Get(int id);
        Werknemer Add(Werknemer werknemer);
        void Update(Werknemer werknemer);
        //void Delete(int id);
    }
    public class InMemoryWerknemerData : IWerknemerData
    {
        static List<Werknemer> Werknemers;
        private static WerknemerDB _werknemerDB = new WerknemerDB();
        private static AfspraakDB _afspraakDB = new AfspraakDB();
        static InMemoryWerknemerData()
        {
            Werknemers = _werknemerDB.GetWerkemers();
            foreach (Werknemer werknemer in Werknemers)
            {
                werknemer.Agenda = _afspraakDB.GetAgenda(werknemer.UserId);
            }
        }
        public IEnumerable<Werknemer> GetAll()
        {
            return Werknemers;
        }
        public Werknemer Get(int id)
        {
            return Werknemers.FirstOrDefault(x => x.UserId == id);
        }
        public Werknemer Add(Werknemer newwerknemer)
        {
            Werknemers.Add(_werknemerDB.AddWerkemer(newwerknemer));
            return newwerknemer;
        }
        public void Update(Werknemer uWerknemer)
        {
            Werknemer werknemer = Werknemers.Find(w => w.UserId == uWerknemer.UserId);
            uWerknemer.Agenda = werknemer.Agenda;
            Werknemers.Remove(werknemer);
            _werknemerDB.UpdateWerkemer(uWerknemer);
            Werknemers.Add(uWerknemer);
            Werknemers.Sort();
        }
        // Is dit nodig?
        //public void Updateafspraak(int userId, Afspraak afspraak)
        //{
        //    Werknemer werknemer = Werknemers.Find(w => w.UserId == userId);
        //    _afspraakDB.UpdateAfspraak(userId, afspraak);
        //    RemoveAfspraak(werknemer, afspraak.Date);
        //    werknemer.Agenda.Add(afspraak);
        //    werknemer.Agenda.Sort();
        //}
        //public void RemoveAfspraak(Werknemer werknemer, DateTime date)
        //{
        //    werknemer.Agenda.Remove(werknemer.Agenda.Find(a => a.Date == date));
        //}
        //public void Delete(int id)
        //{
        //    Werknemer werknemer = Werknemers.Find(w => w.UserId == id);
        //    Werknemers.Remove(werknemer);
        //    _werknemerDB.DeleteWerkemer(werknemer);
        //}
    }
}
