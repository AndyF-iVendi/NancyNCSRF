using System;
using Microsoft.AspNetCore.Builder;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Cryptography;
using Nancy.Owin;
using Nancy.Security;
using Nancy.TinyIoc;

namespace NancyNCSRF
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var options = new NancyOptions();
            options.Bootstrapper = new ApiBootstrapper();
            app.UseOwin(x => x.UseNancy(options));
        }
    }

    public class ApiBootstrapper : DefaultNancyBootstrapper
    {
        public ApiBootstrapper()
        {
            Console.WriteLine("running");

        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var csfrCryptographyConfiguration = new CryptographyConfiguration(
                new AesEncryptionProvider(new PassphraseKeyGenerator("blahblahblah", new byte[] { 8, 2, 10, 4, 68, 120, 7, 14 })),
                new DefaultHmacProvider(new PassphraseKeyGenerator("blahblahblah", new byte[] { 8, 2, 10, 4, 68, 120, 7, 14 })));

            container.Register<CryptographyConfiguration>(csfrCryptographyConfiguration);

            var csrfAppStart = new CsrfApplicationStartup(csfrCryptographyConfiguration, new DefaultCsrfTokenValidator(csfrCryptographyConfiguration));

            csrfAppStart.Initialize(pipelines);

        }
    }
}
