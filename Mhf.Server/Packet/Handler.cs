using Arrowgene.Services.Logging;
using Mhf.Server.Database;
using Mhf.Server.Logging;
using Mhf.Server.Model;
using Mhf.Server.Setting;

namespace Mhf.Server.Packet
{
    public abstract class Handler : IHandler
    {
        protected Handler(MhfServer server)
        {
            Logger = LogProvider.Logger<MhfLogger>(this);
            Server = server;
            Router = server.Router;
            Database = server.Database;
            Settings = server.Setting;
            Clients = server.Clients;
        }

        public abstract ushort Id { get; }
        public virtual int ExpectedSize => QueueConsumer.NoExpectedSize;
        protected MhfServer Server { get; }
        protected MhfSetting Settings { get; }
        protected MhfLogger Logger { get; }
        protected PacketRouter Router { get; }
        protected ClientLookup Clients { get; }
        protected IDatabase Database { get; }
    }
}
