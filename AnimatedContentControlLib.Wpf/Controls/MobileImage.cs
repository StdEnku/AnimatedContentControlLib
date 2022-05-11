using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimatedContentControlLib.Wpf.Controls;

internal class MobileImage : Image
{
    #region XPosFromWidthRethioPropertyプロパティ
    public static readonly DependencyProperty XPosFromWidthRethioProperty
        = DependencyProperty.Register(
            "XPosFromWidthRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0, onXPosFromWidthRethioChanged)
        );

    private static void onXPosFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImage = (MobileImage)obj;
        mobileImage._translate.X = mobileImage.ActualWidth * (double)e.NewValue;
    }

    public double XPosFromWidthRethio
    {
        set => this.SetValue(XPosFromWidthRethioProperty, value);
    }
    #endregion

    #region YPosFromHeightRethioPropertyプロパティ
    public static readonly DependencyProperty YPosFromHeightRethioProperty
        = DependencyProperty.Register(
            "YPosFromHeightRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0, onYPosFromHeightRethioChanged)
        );

    private static void onYPosFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImage = (MobileImage)obj;
        mobileImage._translate.Y = mobileImage.ActualHeight * (double)e.NewValue;
    }

    public double YPosFromHeightRethio
    {
        set => this.SetValue(YPosFromHeightRethioProperty, value);
    }
    #endregion

    static MobileImage()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(MobileImage),
            new FrameworkPropertyMetadata(typeof(MobileImage))
        );
    }

    private ScaleTransform _scale = new();
    private RotateTransform _rotate = new();
    private TranslateTransform _translate = new();

    public MobileImage()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scale);
        transformGroup.Children.Add(this._rotate);
        transformGroup.Children.Add(this._translate);
        this.RenderTransform = transformGroup;
    }
}
