namespace DemoApp.WebUI.Models;

public class ModelBase<T>
{
	public T Data { get; set; }

	public ICollection<string> ErrorMessages { get; set; } = new List<string>();

	public bool IsEdit { get; set; }
}