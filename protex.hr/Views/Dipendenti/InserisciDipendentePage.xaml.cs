using protex.hr.ViewModels.Dipendenti;

namespace protex.hr.Views.Dipendenti;

public partial class InserisciDipendentePage : ContentPage
{
	public InserisciDipendentePage()
	{
		InitializeComponent();

		BindingContext = new InserisciDipendenteViewModel();
	}
}