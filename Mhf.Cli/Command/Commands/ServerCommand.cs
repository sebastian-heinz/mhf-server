using System;
using Mhf.Cli.Argument;
using Mhf.Server;
using Mhf.Server.Setting;

namespace Mhf.Cli.Command.Commands
{
    public class ServerCommand : ConsoleCommand
    {
        private MhfServer _server;
        private readonly LogWriter _logWriter;

        public ServerCommand(LogWriter logWriter)
        {
            _logWriter = logWriter;
        }

        public override void Shutdown()
        {
            if (_server != null)
            {
                _server.Stop();
            }
        }

        public override CommandResultType Handle(ConsoleParameter parameter)
        {
            if (_server == null)
            {
                MhfSetting setting = new MhfSetting();
                _server = new MhfServer(setting);
            }

            if (parameter.Arguments.Contains("start"))
            {
                _server.Start();
                return CommandResultType.Completed;
            }

            if (parameter.Arguments.Contains("stop"))
            {
                _server.Stop();
                return CommandResultType.Completed;
            }

            return CommandResultType.Continue;
        }

        public override string Key => "server";


        public override string Description =>
            $"Monster Hunter Frontier Z Online Server. Ex.:{Environment.NewLine}server start{Environment.NewLine}server stop";
    }
}
