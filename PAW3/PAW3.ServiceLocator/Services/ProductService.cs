using System.Text.Json;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Models.DTOs;
using PAW3.ServiceLocator.Services.Contracts;

namespace PAW3.ServiceLocator.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetDataAsync();
    Task<ProductDTO?> CreateAsync(ProductDTO product);

    Task<bool> UpdateAsync(ProductDTO product);
    Task<bool> DeleteAsync(int id);
}

public class ProductService(IRestProvider restProvider, IConfiguration configuration) : IService<ProductDTO>, IProductService
{
    public async Task<IEnumerable<ProductDTO>> GetDataAsync()
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Product");
        var response = await restProvider.GetAsync(url, null);
        return await JsonProvider.DeserializeAsync<IEnumerable<ProductDTO>>(response);
    }

    public async Task<ProductDTO?> CreateAsync(ProductDTO product)
    {
        var url = configuration.GetStringFromAppSettings("APIS", "Product");

        // Serializamos el objeto a JSON
        var body = JsonSerializer.Serialize(product);

        // Mandamos el POST al RestProvider
        var response = await restProvider.PostAsync(url, body);

        // Deserializamos la respuesta a CategoryDTO
        return await JsonProvider.DeserializeAsync<ProductDTO>(response);
    }


    public async Task<bool> UpdateAsync(ProductDTO product)
    {
        var baseUrl = configuration.GetStringFromAppSettings("APIS", "Product");
        var fullUrl = $"{baseUrl}/{product.ProductId}";

        var body = JsonSerializer.Serialize(product);
        var response = await restProvider.PutAsync(fullUrl, string.Empty, body);

        // Si la respuesta no está vacía, asumimos que fue exitosa
        return !string.IsNullOrWhiteSpace(response);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var baseUrl = configuration.GetStringFromAppSettings("APIS", "Product");
        var fullUrl = $"{baseUrl}/{id}";

        var response = await restProvider.DeleteAsync(fullUrl, string.Empty);

        return !string.IsNullOrWhiteSpace(response);
    }


}
