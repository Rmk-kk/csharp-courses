using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class FakeCourseService : ICourseService
{
    private ConcurrentDictionary<int, Course> _courses = new();
    private int _courseId;

    public Course? Create(CourseDTO request)
    {
        var course = new Course
        {
            Id = Interlocked.Increment(ref _courseId),
            Name = request.Name,
            StartDate = request.StartDate,
            Status = request.Status,
        };
        _courses[course.Id] = course;
        return course;
    }

    public bool Delete(int id)
    {
        if(!_courses.ContainsKey(id))
        {
            return false;
        }
        return _courses.Remove(id, out _);
    }

    public Course? Get(int id)
    {
        if(_courses.TryGetValue(id, out var course))
        {
            return course;
        }
        return null;
    }

    public ICollection<Course> GetAll()
    {
        return _courses.Values;
    }

    public Course? Update(int id, CourseDTO request)
    {
        var course = Get(id);
        if(course is null)
        {
            return null;
        }
        course.Name = request.Name;
        course.StartDate = request.StartDate;
        course.Status = request.Status;
        return course;
    }
}