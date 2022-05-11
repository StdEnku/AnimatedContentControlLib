using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimatedContentControlLib.Wpf.Controls;

internal class MobileContentPresenter : ContentPresenter
{
    #region XPosFromWidthRethioPropertyプロパティ
    public static readonly DependencyProperty XPosFromWidthRethioProperty
        = DependencyProperty.Register(
            "XPosFromWidthRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0, onXPosFromWidthRethioChanged)
        );

    private static void onXPosFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._translate.X = mobileContentPresenter.ActualWidth * (double)e.NewValue;
    }

    public double XPosFromWidthRethio
    {
        set => this.SetValue(XPosFromWidthRethioProperty, value);
    }
    #endregion

    #region XPosFromWidthRethioPropertyプロパティ
    public static readonly DependencyProperty YPosFromHeightRethioProperty
        = DependencyProperty.Register(
            "YPosFromHeightRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0, onYPosFromHeightRethioChanged)
        );

    private static void onYPosFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._translate.Y = mobileContentPresenter.ActualHeight * (double)e.NewValue;
    }

    public double YPosFromHeightRethio
    {
        set => this.SetValue(YPosFromHeightRethioProperty, value);
    }
    #endregion

    static MobileContentPresenter()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(MobileContentPresenter),
            new FrameworkPropertyMetadata(typeof(MobileContentPresenter))
        );
    }

    private ScaleTransform _scale = new();
    private RotateTransform _rotate = new();
    private TranslateTransform _translate = new();

    public MobileContentPresenter()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scale);
        transformGroup.Children.Add(this._rotate);
        transformGroup.Children.Add(this._translate);
        this.RenderTransform = transformGroup;
    }
}
