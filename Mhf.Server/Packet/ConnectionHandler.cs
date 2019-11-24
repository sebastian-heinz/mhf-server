using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public abstract class ConnectionHandler : Handler, IConnectionHandler
    {
        protected ConnectionHandler(MhfServer server) : base(server)
        {
        }
        
        public abstract void Handle(MhfConnection client, MhfPacket packet);
    }
}
