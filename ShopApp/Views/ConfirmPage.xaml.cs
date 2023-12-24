using ShopApp.Model;
using System.Collections.ObjectModel;
using ShopApp.Repository;

namespace ShopApp.View;

public partial class ConfirmPage : ContentPage
{
    public IOrderRepository orderServer = new OrderService();
    public IOrderDetailRepository orderDetailServer = new OrderDetailService();
    public IProductRepository productServer = new ProductService();
    public ILogProductRepository logProductServer = new LogProductService();
    public ICustomerRepository customerServer = new CustomerService();
    public static PaymentInformation information = new();
    public static PaymentInformation GetInformation() => information;

    public ConfirmPage()
    {
        InitializeComponent();
        BindingContext = this;

    }
    public static int MainID;
    public static double subtotal;
    public static double dis;
    public static double total;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ObservableCollection<Cart> Items = CartPage.GetCartItems();
        collectionProduct.ItemsSource = Items;
        cartTotal.Text = total.ToString();
    }
    public static ObservableCollection<Cart> Items { get; set; } = new();
    private void cameraView_BarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if ($"{args.Result[0].Text}" == "Confirm order")
            {
                await cameraView.StopCameraAsync();
                Order order = new()
                {
                    OrderDate = Convert.ToDateTime(DateTime.Now.Date),
                    OrderTime = DateTime.Now.ToShortTimeString(),
                    OrderCustomer = MauiProgram.CUS_ID,
                    PaymentMethod = "Bank",
                    OrderStatus = "Confirmed",
                    Subtotal = (decimal)subtotal,
                    Discount = (decimal)dis,
                    Total = (decimal)total
                };
                order = await orderServer.Add(order);
                MainID = order.OrderId;
                PaymentPage.order = order;
                ObservableCollection<Cart> Items = CartPage.GetCartItems();

                foreach (Cart item in Items)
                {
                    OrderDetail detail = new()
                    {
                        OrderId = order.OrderId,
                        ProductId = Convert.ToInt32(item.ID),
                        Qty = item.Qty,
                        Price = (decimal)Convert.ToDouble(item.Price),
                        Sum = (decimal)Convert.ToDouble(item.Sum)
                    };
                    detail = await orderDetailServer.Add(detail);
                    Product product = await productServer.GetById((int)detail.ProductId);
                    LogProduct log = new()
                    {
                        ProductId = detail.ProductId,
                        LogDate = Convert.ToDateTime(DateTime.Now.Date),
                        LogTime = DateTime.Now.ToShortTimeString(),
                        ProductName = item.Name,
                        StartQty = product.ProductQuantity,
                        Delivered = detail.Qty,
                        Received = 0,
                        EndQty = product.ProductQuantity - detail.Qty,
                        ActorId = MauiProgram.USER_ID
                    };
                    log = await logProductServer.Add(log);
                    product.ProductQuantity = (int)log.EndQty;
                    product = await productServer.UpdateQty(product);
                    if(product == null)
                    {
                        await DisplayAlert("Error", "Something is wrong", "Ok");
                        return;
                    }

                }

                if (dis != 0)
                {
                    Customer customerReset = await customerServer.GetById(MauiProgram.USER_ID);
                    customerReset.CustomerPoint = 0;
                    await customerServer.UpdatePoint(customerReset);
                }
                Customer customer = await customerServer.GetById(MauiProgram.USER_ID);
                customer.CustomerPoint += Convert.ToInt64(total / 1000);
                await customerServer.UpdatePoint(customer);

                information.Amount = total;
                information.Name = customer.CustomerName;
                information.OrderDescription = "mua hang";
                information.OrderType = "other";

                Items.Clear();
                await Shell.Current.GoToAsync(nameof(PaymentPage));
            }
            else { return; }
        });
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                await cameraView.StartCameraAsync();
            });
        }
    }

    private async void cancelBtn_Clicked(object sender, EventArgs e)
    {
        await cameraView.StopCameraAsync();
        await Shell.Current.GoToAsync("..");

    }




}