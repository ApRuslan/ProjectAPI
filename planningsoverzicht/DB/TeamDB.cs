using Microsoft.Data.SqlClient;
using planningsoverzicht.Entities;

namespace planningsoverzicht.DB
{
    public class TeamDB
    {
        private static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public TeamDB()
        {
            builder.DataSource = "itcaseserver.database.windows.net";
            builder.UserID = "AppLogin";
            builder.Password = "Naz15093";
            builder.InitialCatalog = "planningsoverzichtDB";
        }
        public Team AddTeam(Team team)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "insert into Team (Naam, Aanmaker) values (@naam, @aanmaker);";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("naam", team.Naam);
                    cmd.Parameters.AddWithValue("aanmaker", team.Aanmaker);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                conn.Open();
                using (var cmd = new SqlCommand("select * from Team where Naam = @naam and Aanmaker = @aanmaker;", conn))
                {
                    cmd.Parameters.AddWithValue("naam", team.Naam);
                    cmd.Parameters.AddWithValue("aanmaker", team.Aanmaker);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            team.Id = reader.GetInt32(0);
                        }
                    }
                }
                conn.Close();
            }
            return team;
        }
        public void DeleteTeam(int id)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                
                string sql = "delete from Team where Team_Id = @id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }

        }
        public List<Team> GetTeams()
        {
            List<Team> teams = new List<Team>();
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "select * from Team";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.Add(new Team(reader.GetString(1), reader.GetInt32(2)) { Id = reader.GetInt32(0) });
                        }
                    }
                }
                conn.Close();
            }
            return teams;
        }
        public List<Team> UpdateTeam(Team team)
        {
            List<Team> teams = new List<Team>();
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                string sql = "update Team Naam = @naam where Team_Id = @teamId;";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("naam", team.Naam);
                    cmd.Parameters.AddWithValue("teamId", team.Id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            teams.Add(new Team(reader.GetString(1), reader.GetInt32(2)) { Id = reader.GetInt32(0) });
                        }
                    }
                }
                conn.Close();
            }
            return teams;
        }
    }
}
