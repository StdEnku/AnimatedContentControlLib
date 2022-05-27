using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Messangers = AnimatedContentControlLib.Core.Messengers;

namespace AnimatedContentControlLib.Wpf.Controls;

public class AnimatedContentControl : ContentControl, Messangers.IAnimationMessangerTarget
{
    #region 定数
    private const string IMAGE_IN_TEMPLATE_NAME = "SecondaryImage";
    private const string GRID_IN_TEMPLATE_NAME = "RootPanel";
    #endregion

    #region プロパティ
    public double DpiX { get; set; } = 96.0;
    public double DpiY { get; set; } = 96.0;
    #endregion

    #region CurrentStoryboardKey依存関係プロパティ
    public static readonly DependencyProperty CurrentStoryboardKeyProperty
        = DependencyProperty.Register(
            "CurrentStoryboardKey",
            typeof(string),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

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
    public static readonly DependencyProperty StoryboardsProperty
        = DependencyProperty.Register(
            "Storyboards",
            typeof(StoryBoardList),
            typeof(AnimatedContentControl),
            new PropertyMetadata(new StoryBoardList())
        );

    public StoryBoardList Storyboards
    {
        get => (StoryBoardList)this.GetValue(StoryboardsProperty);
        set => this.SetValue(StoryboardsProperty, value);
    }
    #endregion

    #region IsAnimationCompleted依存関係プロパティ
    public static readonly DependencyProperty IsAnimationCompletedProperty
        = DependencyProperty.Register(
            "IsAnimationCompleted",
            typeof(bool),
            typeof(AnimatedContentControl),
            new PropertyMetadata(true)
        );

    public bool IsAnimationCompleted
    {
        get => (bool)this.GetValue(IsAnimationCompletedProperty);
        private set => this.SetValue(IsAnimationCompletedProperty, value);
    }
    #endregion

    #region 添付プロパティ
    public static readonly DependencyProperty KeyProperty
        = DependencyProperty.RegisterAttached(
            "Key",
            typeof(string),
            typeof(AnimatedContentControl),
            new PropertyMetadata(null)
        );

    public static void SetKey(DependencyObject obj, string value)
        => obj.SetValue(KeyProperty, value);

    public static string GetKey(DependencyObject obj)
        => (string)obj.GetValue(KeyProperty);
    #endregion

    #region IAnimationMessangerTargetの実装
    private string? _animationNameMessangerKey = null;
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
