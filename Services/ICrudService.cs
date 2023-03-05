namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public interface ICrudService<T, D>
{
    T? Create(D request);
    T? Get(int id);
    T? Update(int id, D request);
    bool Delete(int id);
    ICollection<T> GetAll();
}