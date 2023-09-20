using DemoApp.Business;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Services;
using DemoApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebUI.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(
        IStudentService studentService)
    {
        _studentService = studentService;
    }

	public async Task<IActionResult> Index()
	{
		var data = (await _studentService.GetAllAsync()).Data;

		return View(new ModelBase<ICollection<StudentDto>>
		{ 
			Data = data
		});
	}

	public IActionResult Add()
	{
		return View("Student", new ModelBase<StudentDto>
		{
			Data = new StudentDto()
		});
	}

	public async Task<IActionResult> Edit(int number)
	{
		var result = await _studentService.GetByNumberAsync(number);

		if (result.ResultState != ResultState.Success)
		{
			return RedirectToAction("Index");
		}

		var studentModel = new ModelBase<StudentDto>
		{
			Data = result.Data!,
			ErrorMessages = result.Messages,
			IsEdit = true
		};

		return View("Student", studentModel);
	}

	public async Task<IActionResult> Delete(int number)
	{
		var result = await _studentService.DeleteAsync(number);
		var data = (await _studentService.GetAllAsync()).Data;

		if (result.ResultState == ResultState.Success)
		{
			return RedirectToAction("Index");
		}


		return View("Index", new ModelBase<ICollection<StudentDto>>
		{
			Data = data,
			ErrorMessages = result.Messages
		});
	}

	[HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ModelBase<StudentDto> studentModel)
    {
        var result = await _studentService.AddAsync(studentModel.Data);

        if (result.ResultState != ResultState.Success)
        {
	        studentModel.ErrorMessages = result.Messages;

	        return View("Student", studentModel);
		}

        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ModelBase<StudentDto> studentModel)
    {
	    var result = await _studentService.UpdateAsync(studentModel.Data);

	    if (result.ResultState != ResultState.Success)
	    {
		    studentModel.ErrorMessages = result.Messages;
		    studentModel.IsEdit = true;

		    return View("Student", studentModel);
	    }

	    return RedirectToAction("Index");
    }
}