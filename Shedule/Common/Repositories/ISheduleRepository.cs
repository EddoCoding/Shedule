using Shedule.Models;

namespace Shedule.Common.Repositories
{
    public interface ISheduleRepository
    {
        IEnumerable<T> GetAllItems<T>() where T : class;
        Task Create<T>(T model) where T : class;
        Task Delete<T>(Guid id) where T : class;

        //Проверки перед добавлением объектов
        Task<bool> CheckSubjectName(string subjectName);
        Task<bool> CheckTeacherName(string teacherName);
        Task<bool> CheckPlaceName(string placeName);
        Task<bool> CheckNumberPara(int numberPara);
    }
}