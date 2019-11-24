using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public interface IConnectionHandler : IHandler
    {
        void Handle(MhfConnection connection, MhfPacket packet);
    }
}
