using System.Collections.Generic;
using Arrowgene.Services.Buffers;
using Mhf.Server.Model;

namespace Mhf.Server.Packet
{
    public abstract class PacketResponse
    {
        private readonly List<ISender> _receiver;
        private MhfPacket _packet;

        public PacketResponse(ushort id)
        {
            _receiver = new List<ISender>();
            Id = id;
        }

        public List<ISender> Receiver => new List<ISender>(_receiver);
        public ushort Id { get; }

        protected abstract IBuffer ToBuffer();

        public MhfPacket ToPacket()
        {
            if (_packet == null)
            {
                _packet = new MhfPacket(Id, ToBuffer());
            }

            return _packet;
        }

        public void AddReceiver(params ISender[] receiver)
        {
            _receiver.AddRange(receiver);
        }

        public void AddReceiver(IEnumerable<ISender> receiver)
        {
            _receiver.AddRange(receiver);
        }

        public void CleatReceivers()
        {
            _receiver.Clear();
        }
    }
}
