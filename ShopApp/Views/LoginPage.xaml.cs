using ShopApp.Model;
using ShopApp.Repository;

namespace ShopApp;

public partial class LoginPage : ContentPage
{
    readonly IUserRepository userServer = new UserService();
    public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            string user = username.Text;
            string pass = password.Text;
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                await Shell.Current.DisplayAlert("Error", "All fields required", "Ok");
                return;
            }
            User login = await userServer.Login(user, pass);
            if (login == null)
            {
                await DisplayAlert("Error", "Credentials are incorrect", "Ok");
                return;
            }
            await Shell.Current.GoToAsync(nameof(MainPage));
            await DisplayAlert("Login", "Successfully logged in", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Login", ex.Message, "Ok");
        }
    }

}