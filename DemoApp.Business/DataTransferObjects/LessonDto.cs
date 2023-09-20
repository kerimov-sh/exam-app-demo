namespace DemoApp.Business.DataTransferObjects;

public class LessonDto
{
    private string _code;
    private string _name;
    private string _teacherName;
    private string _teacherSurname;

    public string Code
    {
        get => _code; 
        set => _code = value.Trim();
    }

    public string Name
    {
        get => _name; 
        set => _name = value.Trim();
    }

    public int Class { get; set; }

    public string TeacherName
    {
        get => _teacherName; 
        set => _teacherName = value.Trim();
    }

    public string TeacherSurname
    {
        get => _teacherSurname;
        set => _teacherSurname = value.Trim();
    }
}