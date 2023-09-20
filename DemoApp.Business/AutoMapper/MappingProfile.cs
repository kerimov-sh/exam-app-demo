using AutoMapper;
using DemoApp.Business.DataTransferObjects;
using DemoApp.DataAccess.Entities;

namespace DemoApp.Business.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<StudentDto, Student>().ReverseMap();
        CreateMap<LessonDto, Lesson>().ReverseMap();
        CreateMap<ExamDto, Exam>().ReverseMap()
	        .ForMember(x => x.LessonFullName, opt => opt.MapFrom(e => e.Lesson.Name))
	        .ForMember(x => x.StudentFullName,
		        opt => opt.MapFrom(e => $"{e.Student.Name} {e.Student.Surname}"));
    }
}