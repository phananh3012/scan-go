using ShopApp.Model;
using ShopApp.Repository;
using ShopApp.View;

namespace ShopApp;

public partial class MainPage : ContentPage
{
    readonly ICustomerRepository customerServer = new CustomerService();
    readonly IProductRepository productServer = new ProductService();

    public MainPage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadInfo();
        badgeCart.BadgeText = CartPage.GetCartItems().Count.ToString();
    }
    int count = 1;
    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                await cameraView.StopCameraAsync();
                count++;
                Product product = await productServer.GetByBarcode($"{args.Result[0].Text}");
                if(product == null)
                {
                    return;
                }
                var answer = await DisplayAlert("Confirm", "Do you want to add " + product.ProductName + " to cart", "Yes", "No");
                if (answer == true)
                {
                    await DisplayAlert("Add item", "Add successfully", "OK");
                    Cart cartItem = new()
                    {
                        ID = product.ProductId.ToString(),
                        Name = product.ProductName,
                        Price = Convert.ToInt32(product.ProductPrice).ToString(),
                        Sum = Convert.ToInt32(product.ProductPrice).ToString(),
                        Qty = 1
                    };
                    CartPage.Add(cartItem);
                    badgeCart.BadgeText = CartPage.GetCartItems().Count.ToString();
                    return;
                }       
            }
            catch(Exception ex)
            {
                await DisplayAlert("Scan Barcode", ex.Message, "Ok");
            }
        });
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        count++;
        if (count % 2 == 0)
        {
            if (cameraView.Cameras.Count > 0)
            {
                cameraView.Camera = cameraView.Cameras.First();
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await cameraView.StopCameraAsync();
                    cameraView.BarCodeDetectionEnabled = false;
                    await cameraView.StartCameraAsync();
                    await Task.Delay(2000);
                    cameraView.BarCodeDetectionEnabled = true;

                });
            }
        }
        else await cameraView.StopCameraAsync();
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
         await Shell.Current.GoToAsync(nameof(CartPage));
    }

    private void logOut_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
        CartPage.CartItems.Clear();
        cameraView.StopCameraAsync();
    }
    private async void LoadInfo()
    {
        try
        {
            Customer getById = await customerServer.GetById(MauiProgram.USER_ID);
            if (getById == null)
            {
                await DisplayAlert("Error", "Infos are incorrect", "Ok");
                return;
            }
            name.Text = "Name: " + getById.CustomerName;
            phoneNumber.Text = "Phone: " + getById.CustomerPhone;
            points.Text = "Points: " + getById.CustomerPoint.ToString();
        }
        catch (Exception e)
        {
            await DisplayAlert("Info", e.Message, "Ok");
        }
    }
}