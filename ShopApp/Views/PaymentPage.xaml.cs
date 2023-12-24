using ShopApp.Model;
using ShopApp.Repository;
using ShopApp.View;

namespace ShopApp;
public partial class PaymentPage : ContentPage
{
    readonly IVnPayService vnPay = new VnPayService();
    readonly IOrderRepository orderServer = new OrderService();

    public static Order order;
    public PaymentPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();       
        PaymentInformation information = ConfirmPage.GetInformation();
        int id = ConfirmPage.MainID;
        webView.Source = await vnPay.CreatePaymentUrl(information, id);
       

    }

    private async void webView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        if (e.Url[8] == '1')
        {
            PaymentResponse paymentResponse = await vnPay.PaymentExecute(e.Url);
            if (paymentResponse.Success == true)
            {
              
                PaymentInformation information = ConfirmPage.GetInformation();
                int id = ConfirmPage.MainID;
                webView.Source = await vnPay.CreatePaymentUrl(information, id);
                order.OrderStatus = "Done";
                order.Received = (decimal)information.Amount;
                order.Change = 0;
                await orderServer.Update(order);
          
                await DisplayAlert("Payment", "Payment Successful", "Ok");
                await Shell.Current.GoToAsync("../../..");

           
            }
            else
            {
                await DisplayAlert("Payment", "Payment failed, please try again", "Ok");
                PaymentInformation information = ConfirmPage.GetInformation();
                int id = ConfirmPage.MainID;
                webView.Source = await vnPay.CreatePaymentUrl(information, id);

            }
        }
        else
        {
            return;
        }


    }
}



