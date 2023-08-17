using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductAPI;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace EAIntegrationTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        public UnitTest1(WebApplicationFactory<Startup> webApplicationFactory)
        {
            this.webApplicationFactory = webApplicationFactory;
        }


        ///// <summary>
        ///// Problem with this approach is
        ///// 1. You need to have application running
        ///// 2. Hardcoded request and path
        ///// 3. Very hard to maintain
        ///// 4. Classical
        ///// </summary>
        //[Fact]
        //public void TestWithHttpClient()
        //{
        //    var client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:5000/");

        //    var reponse = client.Send(new HttpRequestMessage(HttpMethod.Get, "Product/GetProducts"));

        //    //reponse.Content.ReadAsStringAsync().Result;

        //    reponse.EnsureSuccessStatusCode();
        //}



        /// <summary>
        /// Problem with this approach is
        /// 2. Hardcoded request and path
        /// 3. Very hard to maintain
        /// 4. Classical
        /// </summary>
        [Fact]
        public async Task TestWithWebAppFactory()
        {
            var webClient = webApplicationFactory.CreateClient();

            var product = await webClient.GetAsync("/Product/GetProducts");

            var result = product.Content.ReadAsStringAsync().Result;

            result.Should().Contain("Keyboard");
        }


        [Fact]
        public async Task TestWithGeneratedCode()
        {
            var webClient = webApplicationFactory.CreateClient();

            var product = new ProductAPI("https://localhost:44334/", webClient);

            var results = await product.GetProductsAsync();

            results.Select(x => x.Name == "Keyboard").Should().NotBeEmpty();
        }
    }
}