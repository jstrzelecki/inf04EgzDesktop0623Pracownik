using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace ezg2301;

public partial class InfoWindow : Window
{
    public InfoWindow(string content)
    {
        InitializeComponent();
        WindowContetnTextBlock.Text = content; 
    }

    private void OnClickOkButton(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}