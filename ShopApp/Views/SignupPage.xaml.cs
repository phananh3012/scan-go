using ShopApp.Model;
using ShopApp.Repository;

namespace ShopApp.View;

public partial class SignupPage : ContentPage
{
    readonly IUserRepository userServer = new UserService();
    readonly ICustomerRepository customerServer = new CustomerService();
    public SignupPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{
			if(cusName.Text == null || cusPhone.Text == null || cusPassword.Text == null)
			{
                await DisplayAlert("Alert", "Please enter sign up info!!", "OK");
                return;
			}
			else
			{
                User user = new()
                {
                    Username = cusPhone.Text,
                    UserPassword = cusPassword.Text,
                    UserType = "Customer"
                };
                user = await userServer.Add(user);
                if (user == null)
                {
                    await DisplayAlert("Error", "Credentials are incorrect", "Ok");
                    return;
                }
                Customer customer = new()
                {
                    CustomerName = cusName.Text,
                    CustomerPhone = cusPhone.Text,
                    CustomerPoint = 0,
                    Userid = user.Userid
                };
                customer = await customerServer.Add(customer);
                await DisplayAlert("Congrat", "Sign up successful", "OK");
                cusName.Text = "";
                cusPhone.Text = "";
                cusPassword.Text = "";
            }
        }
		catch(Exception ex)
		{
            await DisplayAlert("Alert", ex.Message, "OK");
		}
    }

    private async void goBack_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}