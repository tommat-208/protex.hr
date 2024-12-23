using protex.hr.ViewModels.Ruoli;

namespace protex.hr.Views.Ruoli;

public partial class VisualizzaRuoloPage : ContentPage
{
	public VisualizzaRuoloPage()
	{
		InitializeComponent();

		BindingContext = new VisualizzaRuoloViewModel();
	}
}