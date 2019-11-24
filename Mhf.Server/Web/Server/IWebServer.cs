using System.Threading.Tasks;
using Mhf.Server.Web.Route;

namespace Mhf.Server.Web.Server
{
    /// <summary>
    /// Defines web server
    /// </summary>
    public interface IWebServer
    {
        void SetHandler(IWebServerHandler handler);
        Task Start();
        Task Stop();
    }
}
