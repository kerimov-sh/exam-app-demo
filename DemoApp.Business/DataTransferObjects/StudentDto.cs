namespace DemoApp.Business.DataTransferObjects;

public class StudentDto
{
    private string _name;
    private string _surname;

    public int Number { get; set; }

    public string Name
    {
        get => _name;
        set => _name = value.Trim();
    }

    public string Surname
    {
        get => _surname;
        set => _surname = value.Trim();
    }

    public int Class { get; set; }
}