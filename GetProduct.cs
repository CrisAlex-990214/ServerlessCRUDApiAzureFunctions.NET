using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ServerlessCRUDApi
{
    internal class GetProduct
    {
        private readonly ShopContext shopContext;

        public GetProduct(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Function("GetProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetProduct/{id}")]
        HttpRequest req, string id)
        {
            var product = await shopContext.Product.FindAsync(Guid.Parse(id));
            if (product is null) return new NotFoundResult();

            return new OkObjectResult(product);
        }
    }
}
