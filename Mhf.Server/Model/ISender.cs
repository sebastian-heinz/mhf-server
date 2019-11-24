using Mhf.Server.Packet;

namespace Mhf.Server.Model
{
    public interface ISender
    {
        void Send(MhfPacket packet);
    }
}
