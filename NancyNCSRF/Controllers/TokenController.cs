using Nancy;
using Nancy.Security;

namespace NancyNCSRF.Controllers
{
    public class TokenController : NancyModule
    {
        public TokenController()
        {
            Get("token/", parameters =>
            {
                this.CreateNewCsrfToken();
                return Context.Items[CsrfToken.DEFAULT_CSRF_KEY].ToString();
            });
        }
    }
}
