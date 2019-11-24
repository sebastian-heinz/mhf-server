using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public abstract class ClientHandlerDeserializer<T> : ClientHandler
    {
        private readonly IPacketDeserializer<T> _deserializer;

        protected ClientHandlerDeserializer(MhfServer server, IPacketDeserializer<T> deserializer) : base(server)
        {
            _deserializer = deserializer;
        }

        public override void Handle(MhfClient client, MhfPacket requestPacket)
        {
            T request = _deserializer.Deserialize(requestPacket);
            HandleRequest(client, request);
        }

        public abstract void HandleRequest(MhfClient client, T request);
    }
}
