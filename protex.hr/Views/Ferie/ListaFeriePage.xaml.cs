using protex.hr.ViewModels.Ferie;

namespace protex.hr.Views.Ferie;

public partial class ListaFeriePage : ContentPage
{
	public ListaFeriePage()
	{
		InitializeComponent();

		BindingContext = new ListaFerieViewModel();
	}
}