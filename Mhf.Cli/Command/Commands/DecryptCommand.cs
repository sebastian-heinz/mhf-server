using Mhf.Cli.Argument;
using Mhf.Server.Packet;
using Mhf.Server.Setting;

namespace Mhf.Cli.Command.Commands
{
    public class DecryptCommand : ConsoleCommand
    {
        public override CommandResultType Handle(ConsoleParameter parameter)
        {
            PacketFactory pf = new PacketFactory(new MhfSetting());
            pf.Test();
            return CommandResultType.Completed;
        }

        public override string Key => "decrypt";
        public override string Description => "Decrypt packet data";
    }
}
