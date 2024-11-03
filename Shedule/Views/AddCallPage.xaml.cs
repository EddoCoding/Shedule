using Shedule.ViewModels;

namespace Shedule.Views;

public partial class AddCallPage : ContentPage
{
	public AddCallPage(AddCallViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}