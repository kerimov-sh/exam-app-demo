using AutoMapper;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Extensions;
using DemoApp.Business.Helpers;
using DemoApp.Business.Validators;
using DemoApp.DataAccess;
using DemoApp.DataAccess.Entities;
using FluentValidation;

namespace DemoApp.Business.Services.Implementations;

public class ExamService : IExamService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly AbstractValidator<ExamDto> _examDtoValidator;

    public ExamService(
        IUnitOfWork uow,
        IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _examDtoValidator = new ExamDtoValidator();
    }

    public async Task<ServiceResult> AddAsync(ExamDto dto)
    {
        var validationResult = _examDtoValidator.ValidateAsServiceResult(dto);

        if (validationResult is not null)
        {
            return validationResult;
        }

        dto.Id = 0;

        var exam = _mapper.Map<Exam>(dto);

        _uow.Exams.Add(exam);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> UpdateAsync(ExamDto dto)
    {
	    var validationResult = _examDtoValidator.ValidateAsServiceResult(dto);

	    if (validationResult is not null)
	    {
		    return validationResult;
	    }

		var exam = await _uow.Exams.GetAsync(x => x.Id == dto.Id);

        if (exam is null)
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        _mapper.Map(dto, exam);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var exam = await _uow.Exams.GetAsync(x => x.Id == id);

        if (exam is null)
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        _uow.Exams.Delete(exam);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult<ExamDto?>> GetByIdAsync(int id)
    {
        var exam = await _uow.Exams.GetAsync(e => e.Id == id);

        if (exam is null)
        {
            return new ServiceResult<ExamDto?>(null, ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        var dto = _mapper.Map<ExamDto>(exam);

        return new ServiceResult<ExamDto?>(dto);
    }

    public async Task<ServiceResult<ICollection<ExamDto>>> GetAllAsync()
    {
        var data = await _uow.Exams.GetAllAsync();

        return new ServiceResult<ICollection<ExamDto>>(_mapper.Map<ICollection<ExamDto>>(data));
    }
}