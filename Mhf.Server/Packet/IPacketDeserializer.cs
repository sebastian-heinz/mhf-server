namespace Mhf.Server.Packet
{
    public interface IPacketDeserializer<T>
    {
        T Deserialize(MhfPacket packet);
    }
}
