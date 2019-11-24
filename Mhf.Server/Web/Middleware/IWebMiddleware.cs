using System.Threading.Tasks;

namespace Mhf.Server.Web.Middleware
{
    /// <summary>
    /// Defines a middleware
    /// </summary>
    public interface IWebMiddleware
    {
        Task<WebResponse> Handle(WebRequest request, WebMiddlewareDelegate next);
    }
}
