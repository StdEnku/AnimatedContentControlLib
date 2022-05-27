using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Messangers = AnimatedContentControlLib.Core.Messengers;

namespace AnimatedContentControlLib.Wpf.Controls;

/// <summary>
/// Contentプロパティが変化した際指定したアニメーションを実行可能なコントロール
/// </summary>
public class AnimatedContentControl : ContentControl, Messangers.IAnimationNameMessangerTarget
{
    #region 定数
    private const string IMAGE_IN_TEMPLATE_NAME = "SecondaryImage";
    private const string GRID_IN_TEMPLATE_NAME = "RootPanel";
    #endregion

    #region プロパティ
    /// <summary>
    /// テンプレート内のSubImageで使用するX方向のDpi
    /// </summary>
    public double DpiX { get; set; } = 96.0;

    /// <summary>
    /// テンプレート内のSubImageで使用するY方向のDpi
    /// </summary>
    public double DpiY { get; set; } = 96.0;
    #endregion

    #region CurrentStoryboardKey依存関係プロパティ
    /// <summary>
    /// 次回Contentプロパティが変化した際実行される
    /// アニメーション名用依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty CurrentStoryboardKeyProperty
        = DependencyProperty.Register(
            "CurrentStoryboardKey",
            typeof(string),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// CurrentStoryboardKeyProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public string? CurrentStoryboardKey
    {
        get => (string?)this.GetValue(CurrentStoryboardKeyProperty);
        set => this.SetValue(CurrentStoryboardKeyProperty, value);
    }
    #endregion

    #region EmbededStoryboards依存関係プロパティ
    internal static readonly DependencyProperty EmbededStoryboardsProperty
        = DependencyProperty.Register(
            "EmbededStoryboards",
            typeof(StoryBoardList),
            typeof(AnimatedContentControl),
            new PropertyMetadata(new StoryBoardList())
        );
    

    internal StoryBoardList EmbededStoryboards
    {
        get => (StoryBoardList)this.GetValue(EmbededStoryboardsProperty);
        set => this.SetValue(EmbededStoryboardsProperty, value);
    }
    #endregion

    #region Storyboards依存関係プロパティ
    /// <summary>
    /// 自作アニメーションを登録するためのStoryboardのリスト
    /// </summary>
    public static readonly DependencyProperty StoryboardsProperty
        = DependencyProperty.Register(
            "Storyboards",
            typeof(StoryBoardList),
            typeof(AnimatedContentControl),
            new PropertyMetadata(new StoryBoardList())
        );

    /// <summary>
    /// StoryboardsProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public StoryBoardList Storyboards
    {
        get => (StoryBoardList)this.GetValue(StoryboardsProperty);
        set => this.SetValue(StoryboardsProperty, value);
    }
    #endregion

    #region IsAnimationCompleted依存関係プロパティ
    /// <summary>
    /// アニメーションが終了しているかどうかを示す依存関係プロパティ
    /// </summary>
    public static readonly DependencyProperty IsAnimationCompletedProperty
        = DependencyProperty.Register(
            "IsAnimationCompleted",
            typeof(bool),
            typeof(AnimatedContentControl),
            new PropertyMetadata(true)
        );

    /// <summary>
    /// IsAnimationCompletedProperty依存関係プロパティに対応するClrプロパティ
    /// </summary>
    public bool IsAnimationCompleted
    {
        get => (bool)this.GetValue(IsAnimationCompletedProperty);
        private set => this.SetValue(IsAnimationCompletedProperty, value);
    }
    #endregion

    #region 添付プロパティ
    /// <summary>
    /// Storyboards依存関係プロパティ内のStoryboardに添付して
    /// CurrentStoryboardKeyで特定できるようにするための添付プロパティ
    /// </summary>
    public static readonly DependencyProperty KeyProperty
        = DependencyProperty.RegisterAttached(
            "Key",
            typeof(string),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

    /// <summary>
    /// KeyProperty添付プロパティのセッター
    /// </summary>
    /// <param name="obj">Key添付プロパティが添付されたDependencyObject</param>
    /// <param name="value">キー文字列</param>
    public static void SetKey(DependencyObject obj, string value)
        => obj.SetValue(KeyProperty, value);

    /// <summary>
    /// KeyProperty添付プロパティのゲッター
    /// </summary>
    /// <param name="obj">Key添付プロパティが添付されたDependencyObject</param>
    /// <returns>キー文字列</returns>
    public static string GetKey(DependencyObject obj)
        => (string)obj.GetValue(KeyProperty);
    #endregion

    #region IAnimationMessangerTargetの実装
    private string? _animationNameMessangerKey = null;

    /// <summary>
    /// AnimationNameMessangerで自身を特定するためのキー文字列
    /// </summary>
    public string? AnimationNameMessangerKey
    { 
        get => this._animationNameMessangerKey; 
        set
        {
            this._animationNameMessangerKey = value;
            if (value is not null)
            {
                Messangers.AnimationNameMessanger.RegisterTarget(this);
            }
        }
    }

    /// <summary>
    /// メッセージが届いた際実行されるメソッド
    /// </summary>
    /// <param name="nextAnimationName"></param>
    public void AnimationMessageReceive(string? nextAnimationName)
    {
        this.CurrentStoryboardKey = nextAnimationName;
    }
    #endregion

    static AnimatedContentControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AnimatedContentControl), 
            new FrameworkPropertyMetadata(typeof(AnimatedContentControl))
        );
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);

        if (oldContent == newContent) return;
        if (oldContent is null) return;

        var storyboard = this.Storyboards.Find(x => GetKey(x) == this.CurrentStoryboardKey);
        storyboard ??= this.EmbededStoryboards.Find(x => GetKey(x) == this.CurrentStoryboardKey);
        if (storyboard is null) return;

        var rootPanel = (Grid)this.Template.FindName(GRID_IN_TEMPLATE_NAME, this);
        var secondaryImage = (Image)this.Template.FindName(IMAGE_IN_TEMPLATE_NAME, this);
        var completedEvent = (EventHandler?)null;
        secondaryImage.Source = this.createOldContentBitmap((FrameworkElement)oldContent);

        completedEvent = (sender, e) =>
        {
            secondaryImage.Visibility = Visibility.Hidden;
            this.IsAnimationCompleted = true;
            storyboard.Completed -= completedEvent;
        };

        storyboard.Completed += completedEvent;
        secondaryImage.Visibility = Visibility.Visible;
        this.IsAnimationCompleted = false;
        storyboard.Begin(rootPanel);
    }

    private RenderTargetBitmap createOldContentBitmap(FrameworkElement oldContent)
    {
        var resultBitmap = new RenderTargetBitmap((int)oldContent.ActualWidth,
                                                  (int)oldContent.ActualHeight,
                                                  this.DpiX, this.DpiY, PixelFormats.Pbgra32);
        resultBitmap.Render(oldContent);
        return resultBitmap;
    }
}
