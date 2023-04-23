using Nancy;
using Nancy.Security;

namespace NancyNCSRF.Controllers
{
    public class SubmitControllercs : NancyModule
    {
        public SubmitControllercs()
        {
            Post("submit/", parameters =>
            {
                try
                {
                    this.ValidateCsrfToken();
                }
                catch (CsrfValidationException ex)
                {
                    return new Response { StatusCode = HttpStatusCode.Forbidden, ReasonPhrase = ex.Message };
                }

                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            });
        }
    }
}
