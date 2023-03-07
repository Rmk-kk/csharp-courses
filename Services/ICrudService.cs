namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface ICrudService<TModel, TDto> 
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    Task<TModel?> CreateAsync(TDto request);
    Task<TModel?> GetAsync(int id);
    Task<TModel?> UpdateAsync(int id, TDto request);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<TModel>> GetAllAsync();
}