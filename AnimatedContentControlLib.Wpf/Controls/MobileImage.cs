using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimatedContentControlLib.Wpf.Controls;

/// <summary>
/// ActualHeightやActualWidthへ比率で
/// Transform系の各種プロパティを変更できるようにした
/// Imageクラス
/// </summary>
/// <remarks>
/// このクラスをStoryboardからではなく
/// 直接使用する事を作者は想定していません。
/// </remarks>
public class MobileImage : Image
{
    #region XPosFromWidthRethioProperty依存関係プロパティ
    /// <summary>
    /// TranslateTransform.XプロパティをActualWidthの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
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

    /// <summary>
    /// XPosFromWidthRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double XPosFromWidthRethio
    {
        set => this.SetValue(XPosFromWidthRethioProperty, value);
    }
    #endregion

    #region YPosFromHeightRethioProperty依存関係プロパティ
    /// <summary>
    /// TranslateTransform.YプロパティをActualHeightの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
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

    /// <summary>
    /// YPosFromHeightRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double YPosFromHeightRethio
    {
        set => this.SetValue(YPosFromHeightRethioProperty, value);
    }
    #endregion

    #region RotateXCenterFromWidthRethioProperty依存関係プロパティ
    /// <summary>
    /// RotateTransform.CenterXプロパティをActualWidthの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
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

    /// <summary>
    /// RotateXCenterFromWidthRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double RotateXCenterFromWidthRethio
    {
        set => this.SetValue(RotateXCenterFromWidthRethioProperty, value);
    }
    #endregion

    #region RotateYCenterFromWidthRethioProperty依存関係プロパティ
    /// <summary>
    /// RotateTransform.CenterYプロパティをActualHeightの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
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
        mobileImager._rotate.CenterY = mobileImager.ActualHeight * (double)e.NewValue;
    }

    /// <summary>
    /// RotateYCenterFromWidthRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double RotateYCenterFromHeightRethio
    {
        set => this.SetValue(RotateYCenterFromHeightRethioProperty, value);
    }
    #endregion

    #region ScaleXCenterFromWidthRethioProperty依存関係プロパティ
    /// <summary>
    /// ScaleTransform.CenterXプロパティをActualWidthの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleXCenterFromWidthRethioProperty
        = DependencyProperty.Register(
            "ScaleXCenterFromWidthRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0)
        );

    private static void onScaleXCenterFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImager = (MobileImage)obj;
        mobileImager._scale.CenterX = mobileImager.ActualWidth * (double)e.NewValue;
    }

    /// <summary>
    /// ScaleXCenterFromWidthRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double ScaleXCenterFromWidthRethio
    {
        set => this.SetValue(ScaleXCenterFromWidthRethioProperty, value);
    }
    #endregion

    #region ScaleYCenterFromHeightRethioProperty依存関係プロパティ
    /// <summary>
    /// ScaleTransform.CenterYプロパティをActualHeightの比率で
    /// 指定できるようにするための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty ScaleYCenterFromHeightRethioProperty
        = DependencyProperty.Register(
            "ScaleYCenterFromHeightRethio",
            typeof(double),
            typeof(MobileImage),
            new PropertyMetadata(0.0)
        );

    private static void onScaleYCenterFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileImager = (MobileImage)obj;
        mobileImager._scale.CenterY = mobileImager.ActualHeight * (double)e.NewValue;
    }

    /// <summary>
    /// ScaleYCenterFromHeightRethioProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public double ScaleYCenterFromHeightRethio
    {
        set => this.SetValue(ScaleYCenterFromHeightRethioProperty, value);
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

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public MobileImage()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scale);
        transformGroup.Children.Add(this._rotate);
        transformGroup.Children.Add(this._translate);
        this.RenderTransform = transformGroup;
    }
}
