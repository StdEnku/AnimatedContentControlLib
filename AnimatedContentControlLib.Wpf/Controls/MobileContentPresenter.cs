﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimatedContentControlLib.Wpf.Controls;

internal class MobileContentPresenter : ContentPresenter
{
    #region XPosFromWidthRethioProperty依存関係プロパティ
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

    #region YPosFromHeightRethioProperty依存関係プロパティ
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

    #region RotateXCenterFromWidthRethioProperty依存関係プロパティ
    public static readonly DependencyProperty RotateXCenterFromWidthRethioProperty
        = DependencyProperty.Register(
            "RotateXCenterFromWidthRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0, onRotateXCenterFromWidthRethioChanged)
        );

    private static void onRotateXCenterFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._rotate.CenterX = mobileContentPresenter.ActualWidth * (double)e.NewValue;
    }

    public double RotateXCenterFromWidthRethio
    {
        set => this.SetValue(RotateXCenterFromWidthRethioProperty, value);
    }
    #endregion

    #region RotateYCenterFromWidthRethioProperty依存関係プロパティ
    public static readonly DependencyProperty RotateYCenterFromHeightRethioProperty
        = DependencyProperty.Register(
            "RotateYCenterFromHeightRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0, onRotateYCenterFromHeightRethioChanged)
        );

    private static void onRotateYCenterFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._rotate.CenterY = mobileContentPresenter.ActualHeight * (double)e.NewValue;
    }

    public double RotateYCenterFromHeightRethio
    {
        set=> this.SetValue(RotateYCenterFromHeightRethioProperty, value);
    }
    #endregion

    #region ScaleXCenterFromWidthRethioProperty依存関係プロパティ
    public static readonly DependencyProperty ScaleXCenterFromWidthRethioProperty
        = DependencyProperty.Register(
            "ScaleXCenterFromWidthRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0)
        );

    private static void onScaleXCenterFromWidthRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._scale.CenterX = mobileContentPresenter.ActualWidth * (double)e.NewValue;
    }

    public double ScaleXCenterFromWidthRethio
    {
        set => this.SetValue(ScaleXCenterFromWidthRethioProperty, value);
    }
    #endregion

    #region ScaleYCenterFromHeightRethioProperty依存関係プロパティ
    public static readonly DependencyProperty ScaleYCenterFromHeightRethioProperty
        = DependencyProperty.Register(
            "ScaleYCenterFromHeightRethio",
            typeof(double),
            typeof(MobileContentPresenter),
            new PropertyMetadata(0.0)
        );

    private static void onScaleYCenterFromHeightRethioChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        var mobileContentPresenter = (MobileContentPresenter)obj;
        mobileContentPresenter._scale.CenterY = mobileContentPresenter.ActualHeight * (double)e.NewValue;
    }

    public double ScaleYCenterFromHeightRethio
    {
        set => this.SetValue(ScaleYCenterFromHeightRethioProperty, value);
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
