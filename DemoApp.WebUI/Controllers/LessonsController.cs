using DemoApp.Business;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Services;
using DemoApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.WebUI.Controllers;

public class LessonsController : Controller
{
	private readonly ILessonService _lessonService;

	public LessonsController(
		ILessonService lessonService)
	{
		_lessonService = lessonService;
	}

	public async Task<IActionResult> Index()
	{
		var data = (await _lessonService.GetAllAsync()).Data;

		return View(new ModelBase<ICollection<LessonDto>>
		{
			Data = data
		});
	}

	public IActionResult Add()
	{
		return View("Lesson", new ModelBase<LessonDto>
		{
			Data = new LessonDto()
		});
	}

	public async Task<IActionResult> Edit(string code)
	{
		var result = await _lessonService.GetByCodeAsync(code);

		if (result.ResultState != ResultState.Success)
		{
			return RedirectToAction("Index");
		}

		var studentModel = new ModelBase<LessonDto>
		{
			Data = result.Data!,
			ErrorMessages = result.Messages,
			IsEdit = true
		};

		return View("Lesson", studentModel);
	}

	public async Task<IActionResult> Delete(string code)
	{
		var result = await _lessonService.DeleteAsync(code);
		var data = (await _lessonService.GetAllAsync()).Data;

		if (result.ResultState == ResultState.Success)
		{
			return RedirectToAction("Index");
		}

		return View("Index", new ModelBase<ICollection<LessonDto>>
		{
			Data = data,
			ErrorMessages = result.Messages
		});
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Add(ModelBase<LessonDto> lessonModel)
	{
		var result = await _lessonService.AddAsync(lessonModel.Data);

		if (result.ResultState != ResultState.Success)
		{
			lessonModel.ErrorMessages = result.Messages;

			return View("Lesson", lessonModel);
		}

		return RedirectToAction("Index");
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(ModelBase<LessonDto> lessonModel)
	{
		var result = await _lessonService.UpdateAsync(lessonModel.Data);

		if (result.ResultState != ResultState.Success)
		{
			lessonModel.ErrorMessages = result.Messages;
			lessonModel.IsEdit = true;

			return View("Lesson", lessonModel);
		}

		return RedirectToAction("Index");
	}
}