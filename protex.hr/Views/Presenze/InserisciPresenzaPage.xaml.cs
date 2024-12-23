using protex.hr.ViewModels.Presenze;

namespace protex.hr.Views.Presenze;

public partial class InserisciPresenzaPage : ContentPage
{
	public InserisciPresenzaPage()
	{
		InitializeComponent();

		BindingContext = new InserisciPresenzaViewModel();
	}
}