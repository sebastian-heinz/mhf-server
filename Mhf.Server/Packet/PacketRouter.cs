using System.Collections.Generic;
using Arrowgene.Services.Buffers;
using Arrowgene.Services.Logging;
using Mhf.Server.Logging;
using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public class PacketRouter
    {
        private readonly MhfLogger _logger;

        public PacketRouter()
        {
            _logger = LogProvider.Logger<MhfLogger>(this);
        }

        /// <summary>
        /// Send a packet to a client.
        /// </summary>
        public void Send(ISender client, MhfPacket packet)
        {
            client.Send(packet);
        }

        /// <summary>
        /// Send a packet to a client.
        /// </summary>
        public void Send(ISender client, ushort id, IBuffer data)
        {
            MhfPacket packet = new MhfPacket(id, data);
            Send(client, packet);
        }

        /// <summary>
        /// Send a packet to multiple clients.
        /// </summary>
        /// <param name="excepts">clients to exclude</param>
        public void Send(List<ISender> clients, MhfPacket packet, params ISender[] excepts)
        {
            clients = GetClients(clients, excepts);
            foreach (MhfClient client in clients)
            {
                Send(client, packet);
            }
        }

        /// <summary>
        /// Send a packet to multiple clients.
        /// </summary>
        /// <param name="excepts">clients to exclude</param>
        public void Send(List<ISender> clients, ushort id, IBuffer data, params ISender[] excepts)
        {
            Send(clients, new MhfPacket(id, data), excepts);
        }

        /// <summary>
        /// Send a specific packet response.
        /// </summary>
        public void Send(PacketResponse response)
        {
            foreach (ISender client in response.Receiver)
            {
                Send(client, response.ToPacket());
            }
        }

        /// <summary>
        /// Send a specific packet response.
        /// </summary>
        public void Send(PacketResponse response, params ISender[] clients)
        {
            response.AddReceiver(clients);
            Send(response);
        }

        private List<ISender> GetClients(List<ISender> clients, params ISender[] excepts)
        {
            if (excepts.Length == 0)
            {
                return clients;
            }

            foreach (ISender except in excepts)
            {
                clients.Remove(except);
            }

            return clients;
        }
    }
}
