using protex.hr.Services;

namespace protex.hr.Views.Presenze;

public partial class GestionePresenzePage : ContentPage
{
	public GestionePresenzePage()
	{
		InitializeComponent();
	}

	public void creaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new InserisciPresenzaPage());
	}

	public void storicoClicked(object sender, EventArgs e)
	{
		Sessione.CheckPermessi(this, new StoricoPresenzePage());
	}
}