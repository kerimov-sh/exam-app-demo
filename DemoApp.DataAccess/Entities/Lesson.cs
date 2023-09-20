namespace DemoApp.DataAccess.Entities;

public class Lesson : IEntity
{
    public string Code { get; set; }

    public string Name { get; set; }

    public int Class { get; set; }

    public string TeacherName { get; set; }

    public string TeacherSurname { get; set; }

    public DateTime CreateDate { get; set; }
}