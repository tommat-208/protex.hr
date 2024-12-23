using protex.hr.ViewModels;
using protex.hr.Views.Ruoli;

namespace protex.hr.Views.Ruoli;

public partial class GestioneRuoliPage : ContentPage
{
	public GestioneRuoliPage()
	{
		InitializeComponent();
	}

	public void creaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new InserisciRuoloPage());

	}

	public void listaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new ListaRuoliPage());

	}

	public void visualizzaClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new VisualizzaRuoloPage());
    }
}