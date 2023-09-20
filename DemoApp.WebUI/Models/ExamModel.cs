using DemoApp.Business.DataTransferObjects;

namespace DemoApp.WebUI.Models;

public class ExamModel : ModelBase<ExamDto>
{
	public ICollection<LessonDto> Lessons { get; set; }

	public ICollection<StudentDto> Students { get; set; }
}