namespace protex.hr.Views.Dipendenti;

public partial class GestioneDipendentiPage : ContentPage
{
	public GestioneDipendentiPage()
	{
        InitializeComponent();
	}

	public void creaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new InserisciDipendentePage());
	}

	public void listaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ListaDipendentiPage());
	}



}