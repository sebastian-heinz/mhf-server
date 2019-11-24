using Arrowgene.Services.Logging;
using Mhf.Cli.Argument;

namespace Mhf.Cli.Command
{
    public abstract class ConsoleCommand : IConsoleCommand
    {
        protected ConsoleCommand()
        {
            Logger = LogProvider.Logger(this);
        }

        protected readonly ILogger Logger;

        public abstract string Key { get; }
        public abstract string Description { get; }
        public abstract CommandResultType Handle(ConsoleParameter parameter);

        public virtual void Shutdown()
        {
        }
    }
}
