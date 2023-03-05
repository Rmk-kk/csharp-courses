namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface ICrudService<TModel, TDto>
{
    TModel? Create(TDto request);
    TModel? Get(int id);
    TModel? Update(int id, TDto request);
    bool Delete(int id);
    ICollection<TModel> GetAll();
}