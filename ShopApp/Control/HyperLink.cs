using ShopApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Control
{
    public class HyperLink: Label
    {
        public static readonly BindableProperty UrlProperty =
           BindableProperty.Create(nameof(SignupPage), typeof(string), typeof(HyperLink), null);

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public HyperLink()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Colors.Maroon;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                // Launcher.OpenAsync is provided by Essentials.
                Command = new Command(async () => await Shell.Current.GoToAsync(nameof(SignupPage)))
            });
        }
    }
}
