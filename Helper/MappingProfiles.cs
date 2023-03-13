namespace NetCoreCourse.Helper;

using AutoMapper;
using NetCoreCourse.Models;
using NetCoreCourse.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Course, CourseDTO>();
    }
}