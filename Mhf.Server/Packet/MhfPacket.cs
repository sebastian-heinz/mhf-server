using System;
using Arrowgene.Services.Buffers;

namespace Mhf.Server.Packet
{
    public class MhfPacket
    {
        public static string GetPacketIdName(ushort id)
        {
            if (Enum.IsDefined(typeof(PacketId), id))
            {
                PacketId authPacketId = (PacketId) id;
                return authPacketId.ToString();
            }

            return null;
        }

        private string _packetIdName;

        public MhfPacket(ushort id, IBuffer buffer)
        {
            Header = new PacketHeader(id);
            Data = buffer;
        }

        public MhfPacket(PacketHeader header, IBuffer buffer)
        {
            Header = header;
            Data = buffer;
        }

        public IBuffer Data { get; }
        public ushort Id => Header.Id;
        public PacketHeader Header { get; }

        public string PacketIdName
        {
            get
            {
                if (_packetIdName != null)
                {
                    return _packetIdName;
                }

                _packetIdName = GetPacketIdName(Id);
                if (_packetIdName == null)
                {
                    _packetIdName = $"ID_NOT_DEFINED_{Id}";
                }

                return _packetIdName;
            }
        }
    }
}
