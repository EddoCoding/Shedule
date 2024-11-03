namespace Shedule.ViewModels.Templates
{
    public class SubjectVM
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SubjectName { get; set; } = string.Empty;
    }
}