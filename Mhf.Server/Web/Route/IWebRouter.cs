using System.Threading.Tasks;

namespace Mhf.Server.Web.Route
{
    public interface IWebRouter
    {
        void AddRoute(IWebRoute route);
        Task<WebResponse> Route(WebRequest request);
    }
}
