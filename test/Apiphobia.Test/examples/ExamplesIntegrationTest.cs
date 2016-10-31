using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Apiphobia.Models;
using Apiphobia.Test.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Apiphobia.Test.examples
{
    public class ExamplesIntegrationTest : IDisposable
    {
        public ExamplesIntegrationTest()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("integration-test");
            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        private TestServer Server { get; }
        private HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }

        [Fact]
        public async Task should_return_ok_when_has_auth_key_in_header()
        {
            Client.DefaultRequestHeaders.Add("X-AuthKey", "xyz");

            var response = await Client.GetAsync("api/examples");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task should_return_unauthorized_when_no_auth_key_in_header()
        {
            var response = await Client.GetAsync("api/examples");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task should_create_example()
        {
            Client.DefaultRequestHeaders.Add("X-AuthKey", "xyz");

            var postResponse = await Client.PostAsync("api/examples", new JsonContent(new Example {Value = "value123"}));

            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            var getResponse = await Client.GetAsync(postResponse.Headers.Location);
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            (await getResponse.Content.ReadAsAsync<Example>()).Value.Should().Be("value123");
        }
    }
}