using DemoApp.Business.DataTransferObjects;

namespace DemoApp.Business.Services;

public interface IStudentService
{
    Task<ServiceResult> AddAsync(StudentDto dto);

    Task<ServiceResult> UpdateAsync(StudentDto dto);

    Task<ServiceResult> DeleteAsync(int number);

    Task<ServiceResult<StudentDto?>> GetByNumberAsync(int number);

    Task<ServiceResult<ICollection<StudentDto>>> GetAllAsync();
}