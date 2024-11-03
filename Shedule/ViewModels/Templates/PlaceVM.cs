namespace Shedule.ViewModels.Templates
{
    public class PlaceVM
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PlaceName { get; set; } = string.Empty;
    }
}