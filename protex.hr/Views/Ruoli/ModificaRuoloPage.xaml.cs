using protex.hr.ViewModels;
using protex.hr.ViewModels.Ruoli;
using Repository;

namespace protex.hr.Views.Ruoli;

public partial class ModificaRuoloPage : ContentPage
{
	public ModificaRuoloPage(Role r)
	{
		InitializeComponent();

		BindingContext = new ModificaRuoloViewModel(r);
	}
}