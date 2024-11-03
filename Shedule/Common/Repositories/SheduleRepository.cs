using Microsoft.EntityFrameworkCore;
using Shedule.Models;

namespace Shedule.Common.Repositories
{
    public class SheduleRepository : ISheduleRepository
    {
        public IEnumerable<T> GetAllItems<T>() where T : class
        {
            using (var dataContext = new DataContext())
            {
                var items = dataContext.Set<T>().ToArray();

                if (typeof(T) == typeof(Call)) return items.OrderBy(item => ((Call)(object)item).NumberPara).ToArray();

                return items;
            }
        }
        public async Task Create<T>(T model) where T : class
        {
            using (var dataContext = new DataContext())
            {
                await dataContext.Set<T>().AddAsync(model);
                await dataContext.SaveChangesAsync();
            }
        }
        public async Task Delete<T>(Guid id) where T : class
        {
            using (var dataContext = new DataContext())
            {
                var entity = await dataContext.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    dataContext.Set<T>().Remove(entity);
                    await dataContext.SaveChangesAsync();
                }
            }
        }

        //Проверки перед добавлением объектов на похожесть данных
        public async Task<bool> CheckSubjectName(string subjectName)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Subjects
                    .AnyAsync(x => x.SubjectName == subjectName);
            }
        }
        public async Task<bool> CheckTeacherName(string teacherName)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Teachers
                    .AnyAsync(x => x.TeacherName == teacherName);
            }
        }
        public async Task<bool> CheckPlaceName(string placeName)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Places
                    .AnyAsync(x => x.PlaceName == placeName);
            }
        }
        public async Task<bool> CheckNumberPara(int numberPara)
        {
            using (var dataContext = new DataContext())
            {
                return await dataContext.Calls
                    .AnyAsync(x => x.NumberPara == numberPara);
            }
        }
    }
}