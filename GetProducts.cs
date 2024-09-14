using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;

namespace ServerlessCRUDApi
{
    public class GetProducts
    {
        private readonly ShopContext shopContext;

        public GetProducts(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Function("GetProducts")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            var products = await shopContext.Product.ToListAsync();

            return new OkObjectResult(products);
        }
    }
}
