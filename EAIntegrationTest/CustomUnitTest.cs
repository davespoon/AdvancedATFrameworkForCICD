using EAIntegrationTest.Library;
using FluentAssertions;
using ProductAPI;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EAIntegrationTest
{
    public class CustomUnitTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> customWebApplicationFactory;

        public CustomUnitTest(CustomWebApplicationFactory<Startup> customWebApplicationFactory)
        {
            this.customWebApplicationFactory = customWebApplicationFactory;
        }

        [Fact]
        public async Task TestCustomWebApplication()
        {
            var webClient = customWebApplicationFactory.CreateDefaultClient();
            var product = new ProductAPI("https://localhost:44334/", webClient);

            var results = await product.GetProductsAsync();

            results.Select(x => x.Name == "Keyboard").Should().NotBeEmpty();
        }
    }
}
