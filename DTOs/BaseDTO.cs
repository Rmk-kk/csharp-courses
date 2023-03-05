namespace NetCoreCourse.DTOs;

using NetCoreCourse.Models;

public abstract class BaseDTO<TModel> where TModel : BaseModel
{
    public abstract void UpdateModel(TModel model);
}