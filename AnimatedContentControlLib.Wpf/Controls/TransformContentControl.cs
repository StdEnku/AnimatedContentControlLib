namespace AnimatedContentControlLib.Wpf.Controls;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

/// <summary>
/// 0~1までの値で位置変更,回転,サイズ変更を可能にするためのContentControl
/// </summary>
/// <remarks>
/// 本ライブラリのユーザーがAnimatedContentControlクラスの画面遷移時実行される
/// Storyboardを自作できるようにするためにアクセス修飾子をpublicにしてあるが
/// 本ライブラリのユーザーが本クラスを直接利用することは想定していない。
/// </remarks>
public class TransformContentControl : ContentControl
{
    #region コンストラクタ及びprivateフィールド
    private ScaleTransform _scaleTransform = new();
    private RotateTransform _rotateTransform = new();
    private TranslateTransform _translateTransform = new();

    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public TransformContentControl()
    {
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(this._scaleTransform);
        transformGroup.Children.Add(this._translateTransform);
        transformGroup.Children.Add(this._rotateTransform);
        this.RenderTransform = transformGroup;
    }
    #endregion

    #region X方向への位置移動用の依存関係プロパティ
    /// <summary>
    /// X方向への位置移動用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualWidthと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty TranslateXPosRatioProperty
        = DependencyProperty.Register(
            "TranslateXPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onTranslateXPosRelativeChanged)
        );

    /// <summary>
    /// TranslateXPosRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double TranslateXPosRatio
    {
        get => (double)this.GetValue(TranslateXPosRatioProperty);
        set => this.SetValue(TranslateXPosRatioProperty, value);
    }

    private static void onTranslateXPosRelativeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._translateTransform.X = 
                transformContentControl.TranslateXPosRatio * 
                transformContentControl.ActualWidth;
        }
    }
    #endregion

    #region Y方向への位置移動用の依存関係プロパティ
    /// <summary>
    /// Y方向への位置移動用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualHeightと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty TranslateYPosRatioProperty
        = DependencyProperty.Register(
            "TranslateYPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onTranslateYPosRelativeChanged)
        );

    /// <summary>
    /// TranslateYPosRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double TranslateYPosRatio
    {
        get => (double)this.GetValue(TranslateYPosRatioProperty);
        set => this.SetValue(TranslateYPosRatioProperty, value);
    }

    private static void onTranslateYPosRelativeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._translateTransform.Y =
                transformContentControl.TranslateYPosRatio *
                transformContentControl.ActualHeight;
        }
    }
    #endregion

    #region 回転の軸となるX方向の位置指定用の依存関係プロパティ
    /// <summary>
    /// 回転の軸となるX方向の位置指定用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualWidthと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty RotateCenterXPosRatioProperty
        = DependencyProperty.Register(
            "RotateCenterXPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onRotateCenterXPosRatioChanged)
        );

    /// <summary>
    /// RotateCenterXPosRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double RotateCenterXPosRatio
    {
        get => (double)this.GetValue(RotateCenterXPosRatioProperty);
        set => this.SetValue(RotateCenterXPosRatioProperty, value);
    }

    private static void onRotateCenterXPosRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._rotateTransform.CenterX =
                transformContentControl.RotateCenterXPosRatio *
                transformContentControl.ActualWidth;
        }
    }
    #endregion

    #region 回転の軸となるY方向の位置指定用の依存関係プロパティ
    /// <summary>
    /// 回転の軸となるY方向の位置指定用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualHeightと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty RotateCenterYPosRatioProperty
        = DependencyProperty.Register(
            "RotateCenterYPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onRotateCenterYPosRatioChanged)
        );

    /// <summary>
    /// RotateCenterYPosRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double RotateCenterYPosRatio
    {
        get => (double)this.GetValue(RotateCenterYPosRatioProperty);
        set => this.SetValue(RotateCenterYPosRatioProperty, value);
    }

    private static void onRotateCenterYPosRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._rotateTransform.CenterY =
                transformContentControl.RotateCenterYPosRatio *
                transformContentControl.ActualHeight;
        }
    }
    #endregion

    #region 回転する角度指定用の依存関係プロパティ
    /// <summary>
    /// 回転する角度指定用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力すると360と同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty RotateAngleRatioProperty
        = DependencyProperty.Register(
            "RotateAngleRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onRotateAngleRatioChanged)
        );

    /// <summary>
    /// RotateAngleRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double RotateAngleRatio
    {
        get => (double)this.GetValue(RotateAngleRatioProperty);
        set => this.SetValue(RotateAngleRatioProperty, value);
    }

    private static void onRotateAngleRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._rotateTransform.Angle =
                360.0 *
                transformContentControl.RotateAngleRatio;
        }
    }
    #endregion

    #region サイズ変更の軸となるX方向の位置指定用の依存関係プロパティ
    /// <summary>
    /// サイズ変更の軸となるX方向の位置指定用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualWidthと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty ScaleCenterXPosRatioProperty
        = DependencyProperty.Register(
            "ScaleCenterXPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onScaleCenterXPosRatioChanged)
        );

    /// <summary>
    /// SxaleCenterXPosRatioに対応するCLRプロパティ
    /// </summary>
    public double ScaleCenterXPosRatio
    {
        get => (double)this.GetValue(ScaleCenterXPosRatioProperty);
        set => this.SetValue(ScaleCenterXPosRatioProperty, value);
    }

    private static void onScaleCenterXPosRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._scaleTransform.CenterX =
                transformContentControl.ScaleCenterXPosRatio *
                transformContentControl.ActualWidth;
        }
    }
    #endregion

    #region サイズ変更の軸となるY方向の位置指定用の依存関係プロパティ
    /// <summary>
    /// サイズ変更の軸となるY方向の位置指定用の依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとActualHeightと同じ位置となる
    /// </remarks>
    public static readonly DependencyProperty ScaleCenterYPosRatioProperty
        = DependencyProperty.Register(
            "ScaleCenterYPosRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(0.0, onScaleCenterYPosRatioChanged)
        );

    /// <summary>
    /// SxaleCenterXPosRatioに対応するCLRプロパティ
    /// </summary>
    public double ScaleCenterYPosRatio
    {
        get => (double)this.GetValue(ScaleCenterYPosRatioProperty);
        set => this.SetValue(ScaleCenterYPosRatioProperty, value);
    }

    private static void onScaleCenterYPosRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._scaleTransform.CenterY =
                transformContentControl.ScaleCenterYPosRatio *
                transformContentControl.ActualHeight;
        }
    }
    #endregion

    #region サイズ変更時のX軸の倍率指定用依存関係プロパティ
    /// <summary>
    /// サイズ変更時のX軸の倍率指定用依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとX軸の元サイズと同じ大きさとなる
    /// </remarks>
    public static readonly DependencyProperty ScaleXRatioProperty
        = DependencyProperty.Register(
            "ScaleXRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(1.0, onScaleXRatioChanged)
        );

    /// <summary>
    /// ScaleXRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double ScaleXRatio
    {
        get => (double)this.GetValue(ScaleXRatioProperty);
        set => this.SetValue(ScaleXRatioProperty, value);
    }

    private static void onScaleXRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._scaleTransform.ScaleX =
                transformContentControl.ScaleXRatio;
        }
    }
    #endregion

    #region サイズ変更時のY軸の倍率指定用依存関係プロパティ
    /// <summary>
    /// サイズ変更時のY軸の倍率指定用依存関係プロパティ
    /// </summary>
    /// <remarks>
    /// 数値は0~1までの入力を想定している
    /// 1を入力するとY軸の元サイズと同じ大きさとなる
    /// </remarks>
    public static readonly DependencyProperty ScaleYRatioProperty
        = DependencyProperty.Register(
            "ScaleYRatio",
            typeof(double),
            typeof(TransformContentControl),
            new PropertyMetadata(1.0, onScaleYRatioChanged)
        );

    /// <summary>
    /// ScaleYRatioPropertyに対応するCLRプロパティ
    /// </summary>
    public double ScaleYRatio
    {
        get => (double)this.GetValue(ScaleYRatioProperty);
        set => this.SetValue(ScaleYRatioProperty, value);
    }

    private static void onScaleYRatioChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TransformContentControl transformContentControl)
        {
            transformContentControl._scaleTransform.ScaleY =
                transformContentControl.ScaleYRatio;
        }
    }
    #endregion
}
