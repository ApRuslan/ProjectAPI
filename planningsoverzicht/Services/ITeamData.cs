using planningsoverzicht.DB;
using planningsoverzicht.Entities;

namespace planningsoverzicht.Services
{
    public interface ITeamData
    {
        IEnumerable<Team> GetAll();
        Team Get(int id);
        Team GetByNaam(string name);
        Team Add(Team team);
        void Update(Team team);
        void Delete(Team team);
    }
    public class InMemoryTeamData : ITeamData, ILidmaatschapData
    {
        static List<Team> Teams;
        private static TeamDB _teamDB = new TeamDB();
        private static LidmaatschapDB _lidmaatschapDB = new LidmaatschapDB();
        static InMemoryTeamData()
        {
            Teams = _lidmaatschapDB.GetAll(_teamDB.GetTeams());
        }
        #region Team
        public IEnumerable<Team> GetAll()
        {
            return Teams;
        }
        public Team Get(int id)
        {
            return Teams.FirstOrDefault(x => x.Id == id);
        }
        public Team Add(Team newTeam)
        {
            Teams.Add(_teamDB.AddTeam(newTeam));
            return newTeam;
        }
        public void Update(Team team)
        {
            Teams.Remove(Teams.FirstOrDefault(t => t.Id == team.Id));
            Teams.Add(team);
            _teamDB.UpdateTeam(team);
        }
        public void Delete(Team team)
        {
            if (team.Ledden.Count == 0)
            {
                Teams.Remove(team);
                _teamDB.DeleteTeam(team.Id);
            }
        }
        public Team GetByNaam(string naam)
        {
            return Teams.FirstOrDefault(t => t.Naam == naam);
        }
        #endregion
        #region Lid
        public List<Lidmaatschap> GetAllLidmaatschappen()
        {
            List<Lidmaatschap> Ledden = new List<Lidmaatschap>();
            foreach (Team team in Teams)
            {
                Ledden.AddRange(team.Ledden);
            }
            return Ledden;
        }
        public Lidmaatschap Get(int teamId, int lidId)
        {
            return Teams.FirstOrDefault(t => t.Id == teamId).Ledden.FirstOrDefault(l => l.UserId == lidId);
        }
        public List<Team> GetByLid(int lidId)
        {
            List<Team> teams = new List<Team>();
            foreach (Team team in Teams)
            {
                if (team.Ledden.FindAll(x => x.UserId == lidId) != null)
                {
                    Team temp = team;
                    temp.Ledden.RemoveAll(x => x.UserId != lidId);

                    teams.Add(temp);
                }
            }
            return teams;
        }
        public void AddLid(int teamId, Lidmaatschap lidmaatschap)
        {
            if (Teams.FirstOrDefault(t => t.Id == teamId).Ledden.FirstOrDefault(l => l.UserId == lidmaatschap.UserId) == null)
            {
                Teams.Find(t => t.Id == teamId).Ledden.Add(lidmaatschap);
                _lidmaatschapDB.Add(teamId, lidmaatschap);
            }
        }
        public void UpdateLid(int teamId, Lidmaatschap lidmaatschap)
        {
            if (Teams.FirstOrDefault(t => t.Id == teamId).Ledden.FirstOrDefault(l => l.UserId == lidmaatschap.UserId) == null)
            {
                Teams.Find(t => t.Id == teamId).Ledden.Remove(Teams.Find(t => t.Id == teamId).Ledden.FirstOrDefault(l => l.UserId == lidmaatschap.UserId));
                _lidmaatschapDB.Update(teamId, lidmaatschap);
                Teams.Find(t => t.Id == teamId).Ledden.Add(lidmaatschap);
            }
        }
        public void DeleteLid(int teamId, int lidId)
        {
            Teams.Find(t => t.Id == teamId).Ledden.Remove(Teams.Find(t => t.Id == teamId).Ledden.FirstOrDefault(l => l.UserId == lidId));
            _lidmaatschapDB.Delete(teamId, lidId);
        }
        #endregion
    }
}
