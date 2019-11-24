using Mhf.Server.Model;
using Mhf.Server.Packet;
using Mhf.Server.PacketResponses;

namespace Mhf.Server.PacketHandler
{
    public class MsgHeadHandler : ConnectionHandler
    {
        public MsgHeadHandler(MhfServer server) : base(server)
        {
        }

        public override ushort Id => (ushort) PacketId.MSG_HEAD;

        public override void Handle(MhfConnection connection, MhfPacket packet)
        {
            string keySign = packet.Data.ReadCString();
            string userId = packet.Data.ReadCString();
            string sessionKey = packet.Data.ReadCString();

            Logger.Info($"Auth Request: KeySign:{keySign} UserId:{userId} SessionKey:{sessionKey}");

            MsgHeadResponse response = new MsgHeadResponse();
            Router.Send(response, connection);
        }
    };
}
