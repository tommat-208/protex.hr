using protex.hr.ViewModels.Presenze;

namespace protex.hr.Views.Presenze;

public partial class StoricoPresenzePage : ContentPage
{
	public StoricoPresenzePage()
	{
		InitializeComponent();

		BindingContext = new StoricoPresenzeViewModel();
	}
}