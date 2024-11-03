using Shedule.ViewModels;

namespace Shedule.Views;

public partial class AddItemPage : ContentPage
{
	public AddItemPage(AddItemViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}