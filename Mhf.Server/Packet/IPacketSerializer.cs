namespace Mhf.Server.Packet
{
    public interface IPacketSerializer<T>
    {
        MhfPacket Serialize(T obj);
    }
}
