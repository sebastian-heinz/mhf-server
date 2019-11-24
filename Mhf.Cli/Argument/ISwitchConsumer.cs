using System.Collections.Generic;

namespace Mhf.Cli.Argument
{
    public interface ISwitchConsumer
    {
        List<ISwitchProperty> Switches { get; }
    }
}
