using protex.hr.ViewModels.Ruoli;

namespace protex.hr.Views.Ruoli;

public partial class ListaRuoliPage : ContentPage
{
	public ListaRuoliPage()
	{
		InitializeComponent();

		BindingContext = new ListaRuoliViewModel();
	}
}