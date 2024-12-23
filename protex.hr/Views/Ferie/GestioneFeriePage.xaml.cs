using protex.hr.Services;

namespace protex.hr.Views.Ferie;

public partial class GestioneFeriePage : ContentPage
{
	public GestioneFeriePage()
	{
		InitializeComponent();
	}

    private void creaClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new InserisciRichiestaPage());
    }

	private void approvaClicked(object sender, EventArgs e)
	{
		Sessione.CheckPermessi(this, new ListaFeriePage());
	}
}