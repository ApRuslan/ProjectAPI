using planningsoverzicht.Entities;
using System.Xml;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.Json;
using planningsoverzicht.DB;
using Microsoft.SharePoint.Client;
using System.Security;

namespace planningsoverzicht
{
    public class ConvertPlanning
    {
        public List<Werknemer> ConvertPlanningToDB(List<Werknemer> werknemers)
        {
            var ctx = new ClientContext("https://ventigratedev.sharepoint.com");
            XmlDocument doc = new XmlDocument();
            doc.Load("planning.xml");
            var json = doc.OuterXml;//JsonSerializer.Serialize(doc.OuterXml.ToString());
            var list = json.Split("<ro");
            var temp = list[1].Split("<ce co");
            list = list.Skip(3).ToArray();

            temp = temp.Skip(3).ToArray();
            int[] userId = new int[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                var tempStr = temp[i].Substring(16).Split("<")[0];
                userId[i] = werknemers.Find(x => x.Name == tempStr).UserId;
            }
            for (int i = 0; i < list.Length; i++)
            {
                var row = list[i].Split("<ce co=");
                row = row.Skip(2).ToArray();
                if (row.Length == userId.Length + 1)
                {
                    row[0] = row[0].Substring(27, row[0].Length - 27);
                    row[0] = row[0].Split("<")[0];
                    for (int j = 1; j < row.Length; j++)
                    {
                        row[j] = row[j].Substring(15, row[j].Length - 15);
                        row[j] = row[j].Split("<")[0];
                        //werknemers.Find(w => w.UserId == userId[j - 1]).Agenda.Find(a=>a.Date == Convert.ToDateTime(row[0])).Titel = row[j];
                        werknemers.Find(w => w.UserId == userId[j - 1]).Agenda.Add(new Afspraak(Convert.ToDateTime(row[0]), row[j], false, false, false,""));
                    }
                }
            }
            return werknemers;
        }
    }
}
