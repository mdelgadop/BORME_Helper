using Grupo10MDP.Business.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Globalization;

namespace Grupo10MDP.Business.Repositories
{
    public class DayMarkedBORMERepository : IDayMarkedBORMERepository
    {
        private DBContext DBContext { get; set; }

        public DayMarkedBORMERepository()
        {
            DBContext = new DBContext();
            DBContext.Initialize();
        }

        public DateTime[] GetMarkedDates(string SelectedProvincia)
        {
            SQLiteConnection conn = DBContext.GetInstance();

            IList<DateTime> dates = new List<DateTime>();
            string query = string.Format("select date from marked_dates where provincia='{0}'", SelectedProvincia);

            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                try
                {
                    if (!reader.IsDBNull(0))
                    {
                        string val = reader.GetValue(0).ToString();

                        dates.Add(DateTime.ParseExact(val, "dd.MM.yyyy", new CultureInfo("es-ES")));
                    }
                }
                catch (Exception) { }
            }

            conn.Clone();

            return dates.ToArray();
        }

        public bool MarkAsRead(DateTime Date, string SelectedProvincia)
        {
            try
            {
                SQLiteConnection conn = DBContext.GetInstance();

                var query = string.Format("INSERT INTO MARKED_DATES (DATE, PROVINCIA) VALUES ('{0}', '{1}')", Date.ToString("dd.MM.yyyy"), SelectedProvincia);

                using (var command = new SQLiteCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }

                conn.Clone();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
