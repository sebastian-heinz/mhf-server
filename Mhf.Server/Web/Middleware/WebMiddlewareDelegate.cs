using System.Threading.Tasks;

namespace Mhf.Server.Web.Middleware
{
    public delegate Task<WebResponse> WebMiddlewareDelegate(WebRequest request);
}
