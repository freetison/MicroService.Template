using MicroService.Template.Core.Data.Entities;

namespace MicroService.Template.ApiService.Infrastructure.Data.Entities;

public class Producto : IProducto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoriaId { get; set; }
}
