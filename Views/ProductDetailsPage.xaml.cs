namespace MauiTask_FlagSoftware.Views;

public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync("..");
    }
}