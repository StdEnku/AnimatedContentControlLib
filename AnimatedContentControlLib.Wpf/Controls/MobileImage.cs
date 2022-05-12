using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimatedContentControlLib.Wpf.Controls;

internal class MobileImage : Image
{
    #region XPosFromWidthRethioProperty依存関係プロパティ
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

    #region YPosFromHeightRethioProperty依存関係プロパティ
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

    #region RotateXCenterFrmWidthRethioProperty依存関係プロパティ
    public static readonly DependencyProperty RotateXCenterFromWidthRethioProperty
        = DependencyProperty.Register(
            "RotateXCenterFromWidthRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0, onRotateXCenterFromWidthRethioChanged)
        );

    private static void onRotateXCenterFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImager = (MobileImage)obj;
        mobileImager._rotate.CenterX = mobileImager.ActualWidth * (double)e.NewValue;
    }

    public double RotateXCenterFromWidthRethio
    {
        set => this.SetValue(RotateXCenterFromWidthRethioProperty, value);
    }
    #endregion

    #region RotateYCenterFrmWidthRethioProperty依存関係プロパティ
    public static readonly DependencyProperty RotateYCenterFromHeightRethioProperty
        = DependencyProperty.Register(
            "RotateYCenterFromHeightRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0, onRotateYCenterFromHeightRethioChanged)
        );

    private static void onRotateYCenterFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImager = (MobileImage)obj;
        mobileImager._rotate.CenterY = mobileImager.Height * (double)e.NewValue;
    }

    public double RotateYCenterFromHeightRethio
    {
        set => this.SetValue(RotateYCenterFromHeightRethioProperty, value);
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
