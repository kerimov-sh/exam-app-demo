namespace DemoApp.Business.DataTransferObjects;

public class ExamDto
{
    private string _lessonCode;

    public int Id { get; set; }

    public string LessonCode
    {
        get => _lessonCode;
        set => _lessonCode = value.Trim();
    }

    public string LessonFullName { get; set; }

    public int StudentNumber { get; set; }

    public string StudentFullName { get; set; }

    public DateTime ExamDate { get; set; }

    public int Score { get; set; }
}