using planningsoverzicht.DB;
using planningsoverzicht.Entities;

namespace planningsoverzicht.Services
{
    public interface ILidmaatschapData
    {
        List<Lidmaatschap> GetAllLidmaatschappen();
        Lidmaatschap Get(int teamId, int LidId);
        List<Team> GetByLid(int lidId);
        void AddLid(int teamId, Lidmaatschap lidmaatschap);
        void UpdateLid(int teamId, Lidmaatschap lidmaatschap);
        void DeleteLid(int teamId, int lidId);
    }
}
