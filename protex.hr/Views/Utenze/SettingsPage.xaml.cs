using protex.hr.ViewModels;
using protex.hr.ViewModels.Utenze;

namespace protex.hr.Views.Utenze;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

		BindingContext = new SettingsViewModel();
	}
}