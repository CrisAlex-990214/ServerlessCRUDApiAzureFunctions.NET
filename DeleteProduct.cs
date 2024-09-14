using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ServerlessCRUDApi
{
    internal class DeleteProduct
    {
        private readonly ShopContext shopContext;

        public DeleteProduct(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        [Function("DeleteProduct")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteProduct/{id}")]
        HttpRequest req, string id)
        {
            var product = await shopContext.Product.FindAsync(Guid.Parse(id));
            if (product is null) return new NotFoundResult();

            shopContext.Product.Remove(product);
            await shopContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
