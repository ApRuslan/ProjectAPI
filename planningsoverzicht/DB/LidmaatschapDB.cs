using Microsoft.Data.SqlClient;
using planningsoverzicht.Entities;

namespace planningsoverzicht.DB
{
    public class LidmaatschapDB
    {
        private static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        static LidmaatschapDB()
        {
            builder.DataSource = "itcaseserver.database.windows.net";
            builder.UserID = "AppLogin";
            builder.Password = "Naz15093";
            builder.InitialCatalog = "planningsoverzichtDB";
        }
        public void Add(int teamId, Lidmaatschap lidmaatschap)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "insert into TeamLidmaatschap (UserId, TeamId, Rol, KanGevoeligeInfoVanTeamLedenBekijken, KanGereserveerdeInfoVanTeamledenBekijken) " +
                    "values (@userId, @teamId, @rol, @gevoelig, @gereserveerd);";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("userId", lidmaatschap.UserId);
                    cmd.Parameters.AddWithValue("teamId", teamId);
                    cmd.Parameters.AddWithValue("rol", lidmaatschap.Rol);
                    cmd.Parameters.AddWithValue("gevoelig", lidmaatschap.GevoeligeInfoBekijken);
                    cmd.Parameters.AddWithValue("gereserveerd", lidmaatschap.GereserveerdeInfoBekijken);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public List<Team> GetAll(List<Team> teams)
        {
            List<Lidmaatschap> lidmaatschapen = new List<Lidmaatschap>();
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "select * from TeamLidmaatschap;";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader =  cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.FirstOrDefault(t=> t.Id == reader.GetInt32(1)).Ledden.Add(new Lidmaatschap(reader.GetInt32(0), reader.GetString(2), reader.GetBoolean(3), reader.GetBoolean(4)));
                        }
                    }
                }
                conn.Close();
            }
            return teams;
        }
        public void Delete(int teamId, int lidId)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "delete from TeamLidmaatschap where UserId = @userId and TeamId = @teamid;";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("userId", lidId);
                    cmd.Parameters.AddWithValue("teamid", teamId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Close();
                }
            }
        }
        public void Update(int teamId, Lidmaatschap lidmaatschap)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "update Lidmaatschap Rol = @Rol, KanGevoeligeInfoVanTeamLedenBekijken = @Gevoelig, KanGereserveerdeInfoVanTeamledenBekijken = @Gereserveerd where UserId = @userId and TeamId = @teamid;";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Rol", lidmaatschap.UserId);
                    cmd.Parameters.AddWithValue("Gevoelig", lidmaatschap.UserId);
                    cmd.Parameters.AddWithValue("Gereserveerd", lidmaatschap.UserId);
                    cmd.Parameters.AddWithValue("userId", lidmaatschap.UserId);
                    cmd.Parameters.AddWithValue("teamid", teamId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Close();
                }
            }
        }
    }
}
