namespace Vianditas.Application.Categorias.Contract;

public interface ICategoriaRepository
{
    
    Task<Categoria> GetByIdAsync(Guid id);
    Task<List<Categoria>> GetAllAsync();
    Task AddAsync(Categoria categoria);
}
