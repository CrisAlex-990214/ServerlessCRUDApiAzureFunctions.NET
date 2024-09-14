using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;

namespace ServerlessCRUDApi
{
    internal class UpdateProduct
    {
        private readonly ShopContext shopContext;

        public UpdateProduct(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Function("UpdateProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(body);

            var oldProduct = await shopContext.Product.FindAsync(product.Id);
            if (oldProduct is null) return new NotFoundResult();

            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;

            await shopContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
