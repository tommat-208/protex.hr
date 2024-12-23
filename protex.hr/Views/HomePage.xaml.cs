using protex.hr.Services;
using protex.hr.Views.Dipendenti;
using protex.hr.Views.Ferie;
using protex.hr.Views.Presenze;
using protex.hr.Views.Ruoli;
using protex.hr.Views.Utenze;

namespace protex.hr.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}


	public void impostazioniClicked(object sender, EventArgs e)
	{
        Navigation.PushAsync(new SettingsPage());
    }


    public void ruoliClicked(object sender, EventArgs e)
	{
		Sessione.CheckPermessi(this, new GestioneRuoliPage());
	}

	public void dipendentiClicked(object sender, EventArgs e)
	{
        Sessione.CheckPermessi(this, new GestioneDipendentiPage());
    }

	public void presenzeClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new GestionePresenzePage());
	}

	public void ferieClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new GestioneFeriePage());
	}

}