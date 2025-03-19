using MicroService.Template.Core.Data.Entities;

namespace MicroService.Template.ApiService.Infrastructure.Data.Entities;

public class Categoria : ICategoria
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
