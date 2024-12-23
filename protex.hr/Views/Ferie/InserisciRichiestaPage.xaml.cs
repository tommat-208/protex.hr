using protex.hr.ViewModels.Ferie;

namespace protex.hr.Views.Ferie;

public partial class InserisciRichiestaPage : ContentPage
{
	public InserisciRichiestaPage()
	{
		InitializeComponent();

		BindingContext = new InserisciRichiestaViewModel();
	}
}