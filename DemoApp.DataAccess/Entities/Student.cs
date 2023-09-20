namespace DemoApp.DataAccess.Entities;

public class Student : IEntity
{
    public int Number { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public int Class { get; set; }

    public DateTime CreateDate { get; set; }
}