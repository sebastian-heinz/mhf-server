namespace Mhf.Server.Packet
{
    public class PacketHeader
    {
        public PacketHeader(ushort id) : this(id, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        public PacketHeader(ushort id, byte pf0, byte keyRot, ushort dataSize, ushort combinedCheck, ushort check0,
            ushort check1,
            ushort check2)
        {
            Id = id;
            Pf0 = pf0;
            KeyRotDelta = keyRot;
            DataSize = dataSize;
            CombinedCheck = combinedCheck;
            Check0 = check0;
            Check1 = check1;
            Check2 = check2;
        }

        public ushort Id { get; set; }
        public byte Pf0 { get; set; }
        public byte KeyRotDelta { get; set; }
        public ushort DataSize { get; set; }
        public ushort CombinedCheck { get; set; }
        public ushort Check0 { get; set; }
        public ushort Check1 { get; set; }
        public ushort Check2 { get; set; }


        public string ToLogText()
        {
            return
                $"[Pf0:0x{Pf0:X2}|KeyRotDelta:0x{KeyRotDelta:X2}|Id:0x{Id:X2}|DataSize:0x{DataSize:X2}|CombinedCheck:0x{CombinedCheck:X2}|Chk0:0x{Check0:X2}|Chk1:0x{Check1:X2}|Chk02:0x{Check2:X2}]";
        }
    }
}
