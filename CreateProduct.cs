using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;

namespace ServerlessCRUDApi
{
    internal class CreateProduct
    {
        private readonly ShopContext shopContext;

        public CreateProduct(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Function("CreateProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(body);

            await shopContext.AddAsync(product);
            await shopContext.SaveChangesAsync();

            return new OkObjectResult(product.Id);
        }
    }
}
