using DemoApp.Business;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Services;
using DemoApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebUI.Controllers;

public class ExamsController : Controller
{
	private readonly IExamService _examService;
	private readonly ILessonService _lessonService;
	private readonly IStudentService _studentService;

	public ExamsController(
		IExamService examService,
		ILessonService lessonService,
		IStudentService studentService)
	{
		_examService = examService;
		_lessonService = lessonService;
		_studentService = studentService;
	}

	public async Task<IActionResult> Index()
	{
		var data = (await _examService.GetAllAsync()).Data;

		return View(new ModelBase<ICollection<ExamDto>>
		{
			Data = data
		});
	}

	public async Task<IActionResult> Add()
	{
		var model = await CreateModelAsync();
		model.Data = new ExamDto
		{
			ExamDate = DateTime.Now
		};

		return View("Exam", model);
	}

	public async Task<IActionResult> Edit(int id)
	{
		var result  = await _examService.GetByIdAsync(id);

		if (result.ResultState != ResultState.Success)
		{
			return RedirectToAction("Index");
		}

		var model = await CreateModelAsync();
		model.Data = result.Data!;
		model.IsEdit = true;

		return View("Exam", model);
	}

	public async Task<IActionResult> Delete(int id)
	{
		var result = await _examService.DeleteAsync(id);
		var data = (await _examService.GetAllAsync()).Data;


		if (result.ResultState == ResultState.Success)
		{
			return RedirectToAction("Index");
		}

		return View("Index", new ModelBase<ICollection<ExamDto>>
		{
			Data = data,
			ErrorMessages = result.Messages
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Add(ExamModel model)
	{
		var result = await _examService.AddAsync(model.Data);

		if (result.ResultState != ResultState.Success)
		{
			var data = model.Data;
			model = await CreateModelAsync();
			model.Data = data;
			model.ErrorMessages = result.Messages;

			return View("Exam", model);
		}

		return RedirectToAction("Index");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(ExamModel model)
	{
		var result = await _examService.UpdateAsync(model.Data);

		if (result.ResultState != ResultState.Success)
		{
			var data = model.Data;
			model = await CreateModelAsync();
			model.Data = data;
			model.ErrorMessages = result.Messages;
			model.IsEdit = true;

			return View("Exam", model);
		}

		return RedirectToAction("Index");
	}

	private async Task<ExamModel> CreateModelAsync()
	{
		var students = await _studentService.GetAllAsync();
		var lessons = await _lessonService.GetAllAsync();

		return new ExamModel
		{
			Students = students.Data,
			Lessons = lessons.Data
		};
	}
}