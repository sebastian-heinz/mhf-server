using System.Threading.Tasks;

namespace Mhf.Server.Web.Route
{
    /// <summary>
    /// Defines a handler for a route
    /// </summary>
    public interface IWebRoute
    {
        string Route { get; }
        Task<WebResponse> Get(WebRequest request);
        Task<WebResponse> Post(WebRequest request);
        Task<WebResponse> Put(WebRequest request);
        Task<WebResponse> Delete(WebRequest request);
    }
}
