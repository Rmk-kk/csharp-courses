using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class FakeCrudService<TModel, TDto> : ICrudService<TModel, TDto> 
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    protected ConcurrentDictionary<int, TModel> _items = new();
    private int _itemId;
 
    public TModel? Create(TDto request)
    {
        var item = new TModel
        {
            Id = Interlocked.Increment(ref _itemId),
        };
        _items[item.Id] = item;
        request.UpdateModel(item);
        return item;
    }

    public bool Delete(int id)
    {
        if(!_items.ContainsKey(id))
        {
            return false;
        }
        return _items.Remove(id, out _);
    }

    public TModel? Get(int id)
    {
        if(_items.TryGetValue(id, out var item))
        {
            return item;
        }
        return null;
    }

    public ICollection<TModel> GetAll()
    {
        return _items.Values;
    }

    public TModel? Update(int id, TDto request)
    {
        var item = Get(id);
        if(item is null)
        {
            return null;
        }
        request.UpdateModel(item);
        return item;
    }
}