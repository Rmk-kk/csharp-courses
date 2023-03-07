namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using System.Collections.Generic;

public class DbCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    public Task<TModel?> CreateAsync(TDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<TModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TModel?> UpdateAsync(int id, TDto request)
    {
        throw new NotImplementedException();
    }
}