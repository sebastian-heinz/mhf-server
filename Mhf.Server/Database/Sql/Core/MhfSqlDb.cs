using System;
using System.Data.Common;
using Arrowgene.Services.Logging;
using Mhf.Server.Logging;

namespace Mhf.Server.Database.Sql.Core
{
    /// <summary>
    /// Implementation of Mhf database operations.
    /// </summary>
    public abstract partial class MhfSqlDb<TCon, TCom> : SqlDb<TCon, TCom>
        where TCon : DbConnection
        where TCom : DbCommand
    {
        protected readonly MhfLogger Logger;


        public MhfSqlDb()
        {
            Logger = LogProvider.Logger<MhfLogger>(this);
        }

        protected override void Exception(Exception ex)
        {
            Logger.Exception(ex);
        }

    }
}
