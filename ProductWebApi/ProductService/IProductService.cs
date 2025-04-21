using System.Text.Json;
using Confluent.Kafka;using ProductWebApi.ProductService;
using Share;

namespace ProductWebApi.ProductService
{
    public interface IProductService
    {
        Task AddProduct(Product product);


        Task DeleteProduct(int id);
    }

}

public class ProductService(IProducer<Null,string> producer ) : IProductService
{
    private readonly List<Product> Products = [];
    
    
    public async Task AddProduct(Product product)
    {
        Products.Add(product);
        var result = await producer.ProduceAsync("product-topic", new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(product)
        });
        if (result.Status != PersistenceStatus.Persisted)
        {
            var lastProduct = Products.Last();
            Products.Remove(lastProduct);
        }
    }
    public async Task DeleteProduct(int id)
    {
        Products.Remove( Products.FirstOrDefault(p => p.Id == id));
        await producer.ProduceAsync("delete-product-topic", new Message<Null, string>
        {
            Value = id.ToString()
        });
    }
}
