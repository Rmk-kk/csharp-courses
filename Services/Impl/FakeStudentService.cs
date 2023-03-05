using NetCoreCourse.DTOs;
using NetCoreCourse.Models;
using System.Collections.Concurrent;

namespace NetCoreCourse.Services;

public class FakeStudentService : IStudentService
{
    private ConcurrentDictionary<int, Student> _students = new();
    private int _studentId;

    public Student? Create(StudentDTO request)
    {
        var student = new Student
        {
            Id = Interlocked.Increment(ref _studentId),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };
        _students[student.Id] = student;
        return student;
    }

    public bool Delete(int id)
    {
        if(!_students.ContainsKey(id))
        {
            return false;
        }
        return _students.Remove(id, out _);
    }

    public Student? Get(int id)
    {
        if(_students.TryGetValue(id, out var student))
        {
            return student;
        }
        return null;
    }

    public ICollection<Student> GetAll()
    {
        return _students.Values;
    }

    public Student? Update(int id, StudentDTO request)
    {
        var student = Get(id);
        if(student is null)
        {
            return null;
        }
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        return student;
    }
}