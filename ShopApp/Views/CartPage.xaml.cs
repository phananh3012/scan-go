using ShopApp.Model;
using ShopApp.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShopApp.View;
public partial class CartPage : ContentPage
{
    public static ObservableCollection<Cart> CartItems { get; set; } = new();
    public ICommand AddItem => new Command<Cart>(Additem);
    public ICommand RemoveItem => new Command<Cart>(Removeitem);
    public ICustomerRepository customerServer = new CustomerService();

    public int count = 1;

    public CartPage()
    {
        InitializeComponent();
        BindingContext = this;

    }
    protected override void OnAppearing()
    {
        getTotal();
        base.OnAppearing();
        count = 1;
        discount.Text = "0";
        ObservableCollection<Cart> Items = GetCartItems();
        collectionProduct.ItemsSource = Items;
        getTotal();
    }
    public static ObservableCollection<Cart> GetCartItems() => CartItems;
    public static void Add(Cart item)
    {
        CartItems.Add(item);
    }
    private void Additem(Cart item)
    {
        item.Qty += 1;
        if (item.Qty > 1)
        {
            item.Sum = (Convert.ToDouble(item.Price) * item.Qty).ToString();
        }
        item.OnPropertyChanged(nameof(Cart.Qty));
        item.OnPropertyChanged(nameof(Cart.Sum));
        getTotal();

    }
    private void Removeitem(Cart item)
    {
        item.Qty += -1;
        if (item.Qty >= 1)
        {
            item.Sum = (Convert.ToDouble(item.Price) * item.Qty).ToString();
        }
        if (item.Qty == 0)
        {
            CartItems.Remove(item);
        }
        item.OnPropertyChanged(nameof(Cart.Qty));
        item.OnPropertyChanged(nameof(Cart.Sum));
        getTotal();

    }

    private void goBack_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void getTotal()
    {
        double total = 0;
        foreach (Cart item in CartItems)
        {
            total += Convert.ToDouble(item.Sum);
        }
        subTotal.Text = total.ToString();
        if (double.Parse(subTotal.Text) < double.Parse(discount.Text))
        {
            cartTotal.Text = "0";
        }
        else
        {
            cartTotal.Text = (double.Parse(subTotal.Text) - double.Parse(discount.Text)).ToString();
        }
        points.Text = (int.Parse(cartTotal.Text) / 1000).ToString() ?? "0";
    }
    private async void checkOut_Clicked(object sender, EventArgs e)
    {
        if (cartTotal.Text == "0")
        {
            await DisplayAlert("Alert", "There are no items in cart", "OK");
        }
        else
        {
            ConfirmPage.subtotal = Convert.ToDouble(subTotal.Text);
            ConfirmPage.dis = Convert.ToDouble(discount.Text);
            ConfirmPage.total = Convert.ToDouble(cartTotal.Text);
            await Shell.Current.GoToAsync(nameof(ConfirmPage));
        }
    }

    private async void applyDis_Clicked(object sender, EventArgs e)
    {
        count++;
        Customer customer = await customerServer.GetById(MauiProgram.USER_ID);
        if(customer == null)
        {
            await DisplayAlert("Error", "Customer not found", "Ok");
            return;
        }
        if (count % 2 == 0)
        {
            discount.Text = (Convert.ToInt32(customer.CustomerPoint) * 100).ToString();
            getTotal();
        }
        else
        {
            discount.Text = "0";
            getTotal();
        }

    }
}