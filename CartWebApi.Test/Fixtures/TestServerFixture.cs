using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace CartWebApi.Test.Fixtures
{
    public class TestServerFixture : IDisposable
    {
        public HttpClient Client { get; set; }
        public T GetService<T>() => (T)_services.GetService(typeof(T));

        public DatabaseContext Context { get; private set; }

        private readonly TestServer _server;
        private readonly IServiceProvider _services;
        private bool _disposedValue = false;

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            _server = new TestServer(builder);
            _services = _server.Host.Services;

            Context = GetService<DatabaseContext>();
            Client = _server.CreateClient();
        }

        public async Task<Product> GenerateProduct()
        {
            var product = new Product() { Name = "Test", Description = "Description", Price = 9.99 };
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();
            return product;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                    _server.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }


        [CollectionDefinition("TestServer")]
        public class TestServerCollection : ICollectionFixture<TestServerFixture>
        {
            // This class has no code, and is never created. Its purpose is simply
            // to be the place to apply [CollectionDefinition] and all the
            // ICollectionFixture<> interfaces.
        }
    }
}
