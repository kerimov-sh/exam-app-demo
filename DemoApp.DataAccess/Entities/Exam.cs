namespace DemoApp.DataAccess.Entities;

public class Exam : IEntity
{
    public int Id { get; set; }

    public string LessonCode { get; set; }

    public Lesson Lesson { get; set; }

    public int StudentNumber { get; set; }

    public Student Student { get; set; }

    public DateTime ExamDate { get; set; }

    public int Score { get; set; }

    public DateTime CreateDate { get; set; }
}