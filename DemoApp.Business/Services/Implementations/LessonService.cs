using AutoMapper;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Extensions;
using DemoApp.Business.Helpers;
using DemoApp.Business.Validators;
using DemoApp.DataAccess;
using DemoApp.DataAccess.Entities;
using FluentValidation;

namespace DemoApp.Business.Services.Implementations;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly AbstractValidator<LessonDto> _lessonDtoValidator;

    public LessonService(
        IUnitOfWork uow,
        IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _lessonDtoValidator = new LessonDtoValidator();
    }

    public async Task<ServiceResult> AddAsync(LessonDto dto)
    {
        var validationResult = _lessonDtoValidator.ValidateAsServiceResult(dto);

        if (validationResult is not null)
        {
            return validationResult;
        }

        if (await _uow.Lessons.IsExistsAsync(dto.Code))
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataKeyDuplicated);
        }

        var lesson = _mapper.Map<Lesson>(dto);

        _uow.Lessons.Add(lesson);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> UpdateAsync(LessonDto dto)
    {
        var validationResult = _lessonDtoValidator.ValidateAsServiceResult(dto);

        if (validationResult is not null)
        {
            return validationResult;
        }

        if (!await _uow.Lessons.IsExistsAsync(dto.Code))
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        var lesson = _mapper.Map<Lesson>(dto);

        _uow.Lessons.Update(lesson);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> DeleteAsync(string code)
    {
        var lesson = await _uow.Lessons.GetAsync(l => l.Code == code);

        if (lesson is null)
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        _uow.Lessons.Delete(lesson);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult<LessonDto?>> GetByCodeAsync(string code)
    {
        var lesson = await _uow.Lessons.GetAsync(l => l.Code == code);

        if (lesson is null)
        {
            return new ServiceResult<LessonDto?>(null, ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        var dto = _mapper.Map<LessonDto>(lesson);

        return new ServiceResult<LessonDto?>(dto);
    }

    public async Task<ServiceResult<ICollection<LessonDto>>> GetAllAsync()
    {
        var data = await _uow.Lessons.GetAllAsync();

        return new ServiceResult<ICollection<LessonDto>>(_mapper.Map<ICollection<LessonDto>>(data));
    }
}