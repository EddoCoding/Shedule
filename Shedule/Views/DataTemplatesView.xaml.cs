using Shedule.ViewModels;

namespace Shedule.Views;

public partial class DataTemplatesView : ContentPage
{
	public DataTemplatesView()
	{
		InitializeComponent();
		BindingContext = new DataTemplatesViewModel();
	}
}