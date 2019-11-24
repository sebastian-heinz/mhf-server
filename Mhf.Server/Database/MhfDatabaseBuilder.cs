using System;
using System.IO;
using Arrowgene.Services.Logging;
using Mhf.Server.Database.Sql;
using Mhf.Server.Model;
using Mhf.Server.Setting;

namespace Mhf.Server.Database
{
    public class MhfDatabaseBuilder
    {
        private readonly ILogger _logger;

        public MhfDatabaseBuilder()
        {
            _logger = LogProvider.Logger(this);
        }

        public IDatabase Build(DatabaseSetting settings)
        {
            IDatabase database = null;
            switch (settings.Type)
            {
                case DatabaseType.SQLite:
                    database = PrepareSqlLiteDb(settings.SqLiteFolder);
                    break;
            }

            if (database == null)
            {
                _logger.Error("Database could not be created, exiting...");
                Environment.Exit(1);
            }

            return database;
        }

        private MhfSqLiteDb PrepareSqlLiteDb(string sqLiteFolder)
        {
            string sqLitePath = Path.Combine(sqLiteFolder, $"db.v{MhfSqLiteDb.Version}.sqlite");
            MhfSqLiteDb db = new MhfSqLiteDb(sqLitePath);
            if (db.CreateDatabase())
            {
                ScriptRunner scriptRunner = new ScriptRunner(db);
                scriptRunner.Run(Path.Combine(sqLiteFolder, "Script/schema_sqlite.sql"));
                scriptRunner.Run(Path.Combine(sqLiteFolder, "Script/data_account.sql"));
            }

            return db;
        }
    }
}
