using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class FakeCourseService : ICourseService
{
    private ConcurrentDictionary<int, Course> _items = new();
    private int _itemId;
    
    public ICollection<Course> GetCoursesByStatus(Course.CourseStatus status)
    {
        return _items.Values
                .Where(course => course.Status == status)
                .ToList();
    }

    public Course? Create(CourseDTO request)
    {
        var item = new Course
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

    public Course? Get(int id)
    {
        if(_items.TryGetValue(id, out var item))
        {
            return item;
        }
        return null;
    }

    public ICollection<Course> GetAll()
    {
        return _items.Values;
    }

    public Course? Update(int id, CourseDTO request)
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