namespace Shedule.ViewModels.Templates
{
    public class TeacherVM
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TeacherName { get; set; } = string.Empty;
    }
}