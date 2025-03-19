namespace MicroService.Template.Core.Data.Entities;

public interface IBusqueda
{
    int Id { get; set; }
    string Query { get; set; }
    DateTime CreatedAt { get; set; }
}
