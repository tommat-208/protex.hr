using protex.hr.ViewModels.Utenze;

namespace protex.hr.Views.Utenze;

public partial class ChangelogPage : ContentPage
{
	public ChangelogPage()
	{
		InitializeComponent();

		BindingContext = new ChangelogViewModel();
	}
}