using protex.hr.ViewModels.Dipendenti;

namespace protex.hr.Views.Dipendenti;

public partial class ListaDipendentiPage : ContentPage
{
	public ListaDipendentiPage()
	{
		InitializeComponent();

		BindingContext = new ListaDipendentiViewModel();
	}
}