using DemoApp.Business.DataTransferObjects;

namespace DemoApp.Business.Services;

public interface IExamService
{
    Task<ServiceResult> AddAsync(ExamDto dto);

    Task<ServiceResult> UpdateAsync(ExamDto dto);

    Task<ServiceResult> DeleteAsync(int id);

    Task<ServiceResult<ExamDto?>> GetByIdAsync(int id);

    Task<ServiceResult<ICollection<ExamDto>>> GetAllAsync();
}