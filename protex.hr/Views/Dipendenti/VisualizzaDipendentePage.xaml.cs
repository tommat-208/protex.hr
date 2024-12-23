using protex.hr.ViewModels.Dipendenti;
using Repository;

namespace protex.hr.Views.Dipendenti;

public partial class VisualizzaDipendentePage : ContentPage
{
	public VisualizzaDipendentePage(Employee e)
	{
		InitializeComponent();

		BindingContext = new VisualizzaDipendenteViewModel(e);
	}
}