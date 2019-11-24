using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public interface IClientHandler : IHandler
    {
        void Handle(MhfClient client, MhfPacket packet);
    }
}
