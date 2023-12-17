using Microsoft.Data.SqlClient;
using planningsoverzicht.Entities;
using System.Data;

namespace planningsoverzicht.DB
{
    public class AfspraakDB
    {
        private static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        static AfspraakDB()
        {
            builder.DataSource = "itcaseserver.database.windows.net";
            builder.UserID = "AppLogin";
            builder.Password = "Naz15093";
            builder.InitialCatalog = "planningsoverzichtDB";
        }
        public void UpdateAgendas(List<Werknemer> werknemers)
        {
            Console.WriteLine("Start update Agendas");

            foreach (Werknemer werknemer in werknemers)
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine(werknemer.Name);
                    connection.Open();
                    foreach (Afspraak agenda in werknemer.Agenda)
                    {
                        String sql = "update Afspraak set " +
                    "Titel = @titel, IsGevoelig= @IsGevoelig, IsGereserveerd = @gereserveerd, wijzigingGevraagd = @wijziging, Vervangwoord = @vervangwoord " +
                    "where UserId = @userId AND Datum = @Datum; ";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("userId", werknemer.UserId);
                            command.Parameters.AddWithValue("Titel", agenda.Titel);
                            command.Parameters.AddWithValue("Datum", agenda.Date);
                            command.Parameters.AddWithValue("IsGevoelig", agenda.IsGevoelig);
                            command.Parameters.AddWithValue("gereserveerd", agenda.IsGereserveerd);
                            command.Parameters.AddWithValue("wijziging", agenda.WijzigingGevraagd);
                            command.Parameters.AddWithValue("vervangwoord", agenda.VervangWoord);
                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
            }
            Console.WriteLine("Finished Update");

        }
        public void UpdateAfspraak(int userId, Afspraak agenda)
        {
            Console.WriteLine("Start update");
            String sql = "update Afspraak set " +
                "Titel = @titel, IsGevoelig= @IsGevoelig, IsGereserveerd = @gereserveerd, WijzigingGevraagd = @wijziging, vervangwoord = @vervangwoord " +
                "where UserId = @userId AND Datum = @Datum; ";
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("Titel", agenda.Titel);
                    command.Parameters.AddWithValue("Datum", agenda.Date);
                    command.Parameters.AddWithValue("IsGevoelig", agenda.IsGevoelig);
                    command.Parameters.AddWithValue("gereserveerd", agenda.IsGereserveerd);
                    command.Parameters.AddWithValue("wijziging", agenda.WijzigingGevraagd);
                    command.Parameters.AddWithValue("vervangwoord", agenda.VervangWoord);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            Console.WriteLine("Finished Update");
        }
        public void ImplementAgenda(List<Werknemer> werknemers)
        {
            Console.WriteLine("Start Implementatie");
            String sql = "insert into Afspraak (UserId, Titel, Datum, IsGevoelig) values (@userId, @Titel, @Datum, @IsGevoelig); ";
            foreach (Werknemer werknemer in werknemers)
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine(werknemer.Name);
                    connection.Open();
                    foreach (Afspraak agenda in werknemer.Agenda)
                    {
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("userId", werknemer.UserId);
                            command.Parameters.AddWithValue("Titel", agenda.Titel);
                            command.Parameters.AddWithValue("Datum", agenda.Date);
                            command.Parameters.AddWithValue("IsGevoelig", agenda.IsGevoelig);
                            command.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
            }
            Console.WriteLine("Finished Implementatie");

        }
        public List<Afspraak> GetAgenda(int userId)
        {
            List<Afspraak> agenda = new List<Afspraak>();
            Console.WriteLine("Start GetAgenda: " + userId);
            String sql = "select * from Afspraak where UserId = @userId order by Datum; ";
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agenda.Add(new Afspraak(reader.GetDateTime(2).Date, reader.GetString(1), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetBoolean(5), reader.GetString(6)));
                        }
                    }
                }
                connection.Close();
            }
            return agenda;
        }
        public List<Afspraak> GetWeekAgenda(int userId, DateTime begineDate)
        {
            List<Afspraak> agenda = new List<Afspraak>();
            Console.WriteLine("Start GetAgenda: " + userId);
            String sql = "select * from Afspraak where UserId = @userId and Datum >= @beginDatum and Datum <= @eindDatum  order by Datum; ";
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("userId", userId);
                    command.Parameters.AddWithValue("beginDatum", begineDate);
                    command.Parameters.AddWithValue("eindDatum", begineDate.AddDays(5));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            agenda.Add(new Afspraak(reader.GetDateTime(2).Date, reader.GetString(1), reader.GetBoolean(3), reader.GetBoolean(4), reader.GetBoolean(5), reader.GetString(6)));
                        }
                    }
                }
                connection.Close();
            }
            return agenda;
        }
    }
}