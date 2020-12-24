using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace Volvo.Frota.Test.Fixture
{
    public class IntegrationFixture<TStartup> : IDisposable where TStartup : class
    {
        public HttpClient HttpClient;
        private readonly ApiAppFactory<TStartup> _factory;

        public IntegrationFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost:62837")
            };

            _factory = new ApiAppFactory<TStartup>();
            HttpClient = _factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _factory.Dispose();
        }
    }
}
