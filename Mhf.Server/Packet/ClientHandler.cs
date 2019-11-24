using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public abstract class ClientHandler : Handler, IClientHandler
    {
        protected ClientHandler(MhfServer server) : base(server)
        {
        }

        public abstract void Handle(MhfClient client, MhfPacket packet);
    }
}
