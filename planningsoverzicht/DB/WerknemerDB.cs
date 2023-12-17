using Microsoft.Data.SqlClient;
using planningsoverzicht.Entities;
using planningsoverzicht.ViewModels;
using System.Data;

namespace planningsoverzicht.DB
{
    public class WerknemerDB
    {
        private static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public WerknemerDB()
        {
            builder.DataSource = "itcaseserver.database.windows.net";
            builder.UserID = "AppLogin";
            builder.Password = "Naz15093";
            builder.InitialCatalog = "planningsoverzichtDB";
        }
        public Werknemer AddWerkemer(Werknemer werknemer)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("AddGebruiker", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define and set the parameters for the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@Naam", SqlDbType.VarChar, 50)).Value = werknemer.Name;
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50)).Value = werknemer.Email;
                    cmd.Parameters.Add(new SqlParameter("@EigenGereserveerdeInfo", SqlDbType.Bit)).Value = werknemer.EigenGereserveerdeInfo;
                    cmd.Parameters.Add(new SqlParameter("@Beheerder", SqlDbType.Bit)).Value = werknemer.Beheerder;
                    cmd.Parameters.Add(new SqlParameter("@WeekVerwittiging", SqlDbType.Int)).Value = werknemer.WeekVerwittiging;
                    cmd.Parameters.Add(new SqlParameter("@KalenderStandaardTeam", SqlDbType.VarChar, 100)).Value = werknemer.KalenderStandaardTeam;

                    // Execute the stored procedure
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            werknemer.UserId = reader.GetInt32(0);
                        }
                    }
                }
            }
            return werknemer;
        }
        public List<Werknemer> GetWerkemers()
        {
            List<Werknemer> werknemers = new List<Werknemer>();
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                string sql = "select * from Gebruiker;";
                
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            werknemers.Add(new Werknemer(reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetBoolean(5), reader.GetString(6)) { UserId = reader.GetInt32(0)});
                        }
                    }
                }
                conn.Close();
            }
            return werknemers;

        }
        public void UpdateWerkemer(Werknemer werknemer)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                string sql = "update Gebruiker set Email = @email, EigenGereserveerdeInfo=@EigenGereserveerdeInfo, " +
                    "Beheerder=@Beheerder, WeekVerwittiging= @WeekVerwittiging, KalenderStandaardTeam=@KalenderStandaardTeam where User_Id = @User_Id;";
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("email", werknemer.Email);
                    cmd.Parameters.AddWithValue("EigenGereserveerdeInfo", werknemer.EigenGereserveerdeInfo);
                    cmd.Parameters.AddWithValue("Beheerder", werknemer.Beheerder);
                    cmd.Parameters.AddWithValue("WeekVerwittiging", werknemer.WeekVerwittiging);
                    cmd.Parameters.AddWithValue("KalenderStandaardTeam", werknemer.KalenderStandaardTeam);
                    cmd.Parameters.AddWithValue("User_Id", werknemer.UserId);
                }
                conn.Close();
            }
        }
        public void DeleteWerkemer(Werknemer werknemer)
        {
            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                string sql = "Delete from Gebruiker where User_Id = @User_Id;";
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("email", werknemer.Email);
                    cmd.Parameters.AddWithValue("EigenGereserveerdeInfo", werknemer.EigenGereserveerdeInfo);
                    cmd.Parameters.AddWithValue("Beheerder", werknemer.Beheerder);
                    cmd.Parameters.AddWithValue("WeekVerwittiging", werknemer.WeekVerwittiging);
                    cmd.Parameters.AddWithValue("KalenderStandaardTeam", werknemer.KalenderStandaardTeam);
                    cmd.Parameters.AddWithValue("User_Id", werknemer.UserId);
                }
                conn.Close();
            }
        }
    }
}
