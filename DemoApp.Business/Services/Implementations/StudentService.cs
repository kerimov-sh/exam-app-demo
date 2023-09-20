using AutoMapper;
using DemoApp.Business.DataTransferObjects;
using DemoApp.Business.Extensions;
using DemoApp.Business.Helpers;
using DemoApp.Business.Validators;
using DemoApp.DataAccess;
using DemoApp.DataAccess.Entities;
using FluentValidation;

namespace DemoApp.Business.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly AbstractValidator<StudentDto> _studentDtoValidator;

    public StudentService(
        IUnitOfWork uow,
        IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
        _studentDtoValidator = new StudentDtoValidator();
    }

    public async Task<ServiceResult> AddAsync(StudentDto dto)
    {
        var validationResult = _studentDtoValidator.ValidateAsServiceResult(dto);

        if (validationResult is not null)
        {
            return validationResult;
        }

        if (await _uow.Students.IsExistsAsync(dto.Number))
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataKeyDuplicated);
        }

        var student = _mapper.Map<Student>(dto);

        _uow.Students.Add(student);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> UpdateAsync(StudentDto dto)
    {
        var validationResult = _studentDtoValidator.ValidateAsServiceResult(dto);

        if (validationResult is not null)
        {
            return validationResult;
        }

        if (!await _uow.Students.IsExistsAsync(dto.Number))
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        var student = _mapper.Map<Student>(dto);

        _uow.Students.Update(student);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult> DeleteAsync(int number)
    {
        var student = await _uow.Students.GetAsync(s => s.Number == number);

        if (student is null)
        {
            return new ServiceResult(ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        _uow.Students.Delete(student);

        await _uow.SaveChangesAsync();

        return new ServiceResult();
    }

    public async Task<ServiceResult<StudentDto?>> GetByNumberAsync(int number)
    {
        var student = await _uow.Students.GetAsync(s => s.Number == number);

        if (student is null)
        {
            return new ServiceResult<StudentDto?>(null, ResultState.InvalidOperation, FailMessages.DataNotFound);
        }

        var dto = _mapper.Map<StudentDto>(student);

        return new ServiceResult<StudentDto?>(dto);
    }

    public async Task<ServiceResult<ICollection<StudentDto>>> GetAllAsync()
    {
        var data = await _uow.Students.GetAllAsync();

        return new ServiceResult<ICollection<StudentDto>>(_mapper.Map<ICollection<StudentDto>>(data));
    }
}