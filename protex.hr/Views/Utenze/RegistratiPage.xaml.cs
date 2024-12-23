using protex.hr.ViewModels;
using protex.hr.ViewModels.Utenze;

namespace protex.hr.Views.Utenze;

public partial class RegistratiPage : ContentPage
{
	public RegistratiPage()
	{
		InitializeComponent();

		BindingContext = new RegistratiViewModel();
	}

}