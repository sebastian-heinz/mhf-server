using System.Threading.Tasks;

namespace Mhf.Server.Web.Server
{
    public interface IWebServerHandler
    {
        Task<WebResponse> Handle(WebRequest request);
    }
}
