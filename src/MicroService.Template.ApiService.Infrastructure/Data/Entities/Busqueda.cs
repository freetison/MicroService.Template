using MicroService.Template.Core.Data.Entities;

namespace MicroService.Template.ApiService.Infrastructure.Data.Entities;

public class Busqueda : IBusqueda
{
    public int Id { get; set; }
    public string Query { get; set; }
    public DateTime CreatedAt { get; set; }
}
