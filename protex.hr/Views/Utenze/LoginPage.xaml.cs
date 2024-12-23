using protex.hr.Services;
using protex.hr.ViewModels;
using protex.hr.ViewModels.Utenze;

namespace protex.hr.Views.Utenze;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();

		BindingContext = new LoginViewModel();
	}

	public LoginPage(string msg)
	{
        InitializeComponent();

        BindingContext = new LoginViewModel(msg);
    }

	public void registratiClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new RegistratiPage());
	}
}