using protex.hr.ViewModels.Ruoli;

namespace protex.hr.Views.Ruoli;

public partial class InserisciRuoloPage : ContentPage
{
	public InserisciRuoloPage()
	{
		InitializeComponent();

		BindingContext = new InserisciRuoloViewModel();
	}
}