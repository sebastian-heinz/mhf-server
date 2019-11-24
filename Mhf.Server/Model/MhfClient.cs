using System;
using System.Diagnostics;
using Arrowgene.Services.Logging;
using Mhf.Server.Logging;
using Mhf.Server.Packet;

namespace Mhf.Server.Model
{
    [DebuggerDisplay("{Identity,nq}")]
    public class MhfClient : ISender
    {
        private readonly MhfLogger _logger;

        public MhfClient()
        {
            _logger = LogProvider.Logger<MhfLogger>(this);
            Creation = DateTime.Now;
            Identity = "";
        }

        public DateTime Creation { get; }
        public string Identity { get; private set; }
        public Account Account { get; set; }
        public MhfConnection Connection { get; set; }


        public void Send(MhfPacket packet)
        {
            Connection.Send(packet);
        }

        public void UpdateIdentity()
        {
            Identity = "";
            if (Account != null)
            {
                Identity += $"[Acc:{Account.Id}:{Account.Name}]";
                return;
            }

            if (Connection != null)
            {
                Identity += $"[Con:{Connection.Identity}]";
            }
        }

        public void Close()
        {
            Connection?.Socket.Close();
        }
    }
}
