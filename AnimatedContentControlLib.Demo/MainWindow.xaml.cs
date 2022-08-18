namespace AnimatedContentControlLib.Demo;

using System.Windows;
using System.Windows.Controls;
using AnimatedContentControlLib.BuiltInAnimKeys;

public partial class MainWindow : Window
{
    private readonly Control1 _control1 = new();
    private readonly Control2 _control2 = new();
    private bool _count = false;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void NavigateButton_Click(object sender, RoutedEventArgs e)
    {
        this._count = !this._count;
        object nextControl = this._count ? this._control2 : this._control1;
        this.MainContent.Content = nextControl;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        this.MainContent.Content = this._control1;
    }
}
