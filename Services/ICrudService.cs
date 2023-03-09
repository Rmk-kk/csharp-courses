namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface ICrudService<TModel, TDto> 
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    public const int DefaultPageSize = 9;
    Task<TModel?> CreateAsync(TDto request);
    Task<TModel?> GetByIdAsync(int id);
    Task<TModel?> UpdateAsync(int id, TDto request);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<TModel>> GetAllAsync(int page = 1, int pageSize = DefaultPageSize);
}