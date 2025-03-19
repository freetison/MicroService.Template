namespace MicroService.Template.Core.Data.Entities;

public interface IProducto
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
    int CategoriaId { get; set; }
}
