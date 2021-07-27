using System.Data.SQLite;
using System.IO;

namespace Grupo10MDP.Business.Repositories
{
    public class DBContext
    {
        private const string DBName = "./grupo10.sqlite";

        public void Initialize()
        {
            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);

                #region Initializing Database
                SQLiteConnection conn = GetInstance();

                var query = "CREATE TABLE MARKED_DATES (DATE TEXT NOT NULL, PROVINCIA TEXT NOT NULL)";

                using (var command = new SQLiteCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
                #endregion Initializing Database
            }
        }

        public SQLiteConnection GetInstance()
        {
            var db = new SQLiteConnection(
                string.Format("Data Source={0};Version=3;", DBName)
            );

            db.Open();

            return db;
        }

    }
}
