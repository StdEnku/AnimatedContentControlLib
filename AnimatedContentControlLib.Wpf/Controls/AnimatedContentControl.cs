namespace AnimatedContentControlLib.Wpf.Controls;

using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using AnimatedContentControlLib.BuiltInAnimKeys;

/// <summary>
/// Contentプロパティ変更時にアニメーションを実行するためのContentControl
/// </summary>
public class AnimatedContentControl : ContentControl
{
    #region DPI指定用CLRプロパティ
    /// <summary>
    /// テンプレート内のSubImageで使用するX方向のDpiプロパティ
    /// </summary>
    public double DpiX { get; set; } = 96.0;

    /// <summary>
    /// テンプレート内のSubImageで使用するY方向のDpiプロパティ
    /// </summary>
    public double DpiY { get; set; } = 96.0;
    #endregion

    #region Contentプロパティ変更時のアニメーションが実行中かどうかを表す依存関係プロパティ
    /// <summary>
    /// アニメーションが終了しているかどうかを示す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty IsAnimCompletedProperty
        = DependencyProperty.Register(
            "IsAnimCompleted",
            typeof(bool),
            typeof(AnimatedContentControl),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// IsAnimCompletedPropertyに対応するCLRプロパティ
    /// </summary>
    public bool IsAnimCompleted
    {
        get => (bool)this.GetValue(IsAnimCompletedProperty);
        private set => this.SetValue(IsAnimCompletedProperty, value);
    }
    #endregion

    #region 実行するStoryboardのKeyを指定するための依存関係プロパティ
    /// <summary>
    /// リソースに登録されたStoryboardから次のContentプロパティ変更時に実行する
    /// StoryboardのKeyを指定するための依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty NextAnimKeyProperty
        = DependencyProperty.Register(
            "NextAnimKey",
            typeof(string),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// NextAnimKeyPropertyに対応するCLRプロパティ
    /// </summary>
    public string? NextAnimKey
    {
        get => (string?)this.GetValue(NextAnimKeyProperty);
        set => this.SetValue(NextAnimKeyProperty, value); 
    }
    #endregion

    #region 利用する組み込みアニメーションを指定するための依存関係プロパティ
    /// <summary>
    /// 画面遷移時に組み込みアニメーションを使用する際に
    /// 利用する組み込みアニメーションを指定するための依存関係プロパティ
    /// nullをセットするとアニメーションは行われない
    /// </summary>
    public static readonly DependencyProperty NextBuiltInAnimKeyProperty
        = DependencyProperty.Register(
            "NextBuiltInAnimKey",
            typeof(AnimKeys?),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// NextBuiltInAnimKeyPropertyに対応するCLRプロパティ
    /// </summary>
    public AnimKeys? NextBuiltInAnimKey
    {
        get => (AnimKeys?)this.GetValue(NextBuiltInAnimKeyProperty);
        set => this.SetValue(NextBuiltInAnimKeyProperty, value);
    }
    #endregion

    #region Contentプロパティが変化した際実行されるメソッド
    /// <summary>
    /// Contentプロパティ変更時実行されるメソッド
    /// </summary>
    /// <param name="oldContent">古いContent値</param>
    /// <param name="newContent">新しいContent値</param>
    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);

        if (oldContent is FrameworkElement oldFrameworkElement)
        {
            //下記古いContentをビットマップ化してテンプレート内のOldContentImageで表示する処理
            var oldContentImage = (Image)this.Template.FindName("OldContentImage", this);
            var oldBitmap = new RenderTargetBitmap((int)oldFrameworkElement.ActualWidth,
                                                   (int)oldFrameworkElement.ActualHeight,
                                                   this.DpiX, this.DpiY, PixelFormats.Pbgra32);
            oldBitmap.Render(oldFrameworkElement);
            oldContentImage.Source = oldBitmap;

            //下記アニメーションの検索と実行
            var rootPanel = (Grid)this.Template.FindName("RootPanel", this);
            Storyboard? nextStoryboard = this.NextAnimKey is not null ? this.FindResource(this.NextAnimKey) as Storyboard :
                                         this.NextBuiltInAnimKey is not null ? rootPanel.FindResource(this.NextBuiltInAnimKey.ToString()) as Storyboard :
                                         null;

            if (nextStoryboard is not null)
            {
                
                var imageTransform = (TransformContentControl)this.Template.FindName("ImageTransform", this);

                EventHandler? completedEvent = null;
                completedEvent = (sender, e) =>
                {
                    imageTransform.Visibility = Visibility.Hidden;
                    this.IsAnimCompleted = true;
                    nextStoryboard.Completed -= completedEvent;
                };

                nextStoryboard.Completed += completedEvent;
                imageTransform.Visibility = Visibility.Visible;
                this.IsAnimCompleted = false;
                nextStoryboard.Begin(rootPanel);
            }
        }
    }
    #endregion

    #region カスタムコントロールのStyleを読み込むための静的コンストラクタ
    /// <summary>
    /// カスタムコントロールのStyleを読み込むための静的コンストラクタ
    /// </summary>
    static AnimatedContentControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentControl), new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
    }
    #endregion
}
