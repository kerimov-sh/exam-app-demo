using DemoApp.Business.DataTransferObjects;

namespace DemoApp.Business.Services;

public interface ILessonService
{
    Task<ServiceResult> AddAsync(LessonDto dto);

    Task<ServiceResult> UpdateAsync(LessonDto dto);

    Task<ServiceResult> DeleteAsync(string code);

    Task<ServiceResult<LessonDto?>> GetByCodeAsync(string code);

    Task<ServiceResult<ICollection<LessonDto>>> GetAllAsync();
}