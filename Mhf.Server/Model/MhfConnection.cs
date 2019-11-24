using System;
using System.Collections.Generic;
using Arrowgene.Services.Logging;
using Arrowgene.Services.Networking.Tcp;
using Mhf.Server.Logging;
using Mhf.Server.Packet;

namespace Mhf.Server.Model
{
    public class MhfConnection : ISender
    {
        private readonly MhfLogger _logger;

        public MhfConnection(ITcpSocket clientSocket, PacketFactory packetFactory)
        {
            _logger = LogProvider.Logger<MhfLogger>(this);
            Socket = clientSocket;
            PacketFactory = packetFactory;
            Client = null;
        }

        public string Identity => Socket.Identity;
        public ITcpSocket Socket { get; }
        public PacketFactory PacketFactory { get; }
        public MhfClient Client { get; set; }

        public List<MhfPacket> Receive(byte[] data)
        {
            List<MhfPacket> packets;
            try
            {
                packets = PacketFactory.Read(data);
            }
            catch (Exception ex)
            {
                _logger.Exception(this, ex);
                packets = new List<MhfPacket>();
            }

            return packets;
        }

        public void Send(MhfPacket packet)
        {
            byte[] data;
            try
            {
                data = PacketFactory.Write(packet);
            }
            catch (Exception ex)
            {
                _logger.Exception(this, ex);
                return;
            }

          //  _logger.LogOutgoingPacket(this, packet);
            Socket.Send(data);
        }
    }
}
