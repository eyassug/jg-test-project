using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Jibble
{
    public class HttpApiIntegrationTests : IClassFixture<CustomWebApplicationFactory<HttpApi.Startup>>
    {

        CustomWebApplicationFactory<HttpApi.Startup> Factory { get; }

        public HttpApiIntegrationTests(CustomWebApplicationFactory<HttpApi.Startup> factory)
            => Factory = factory;

        [Theory]
        [InlineData("/swagger")]
        [InlineData("/jobs")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/api")]
        [InlineData("/api/files")]
        public async Task Get_NonExisting_NonGetEndpointsReturnNotFound(string url)
        {
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


    }
}
