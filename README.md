# ![Header](./Img/Header.png)

[日本語版README.md](https://github.com/StdEnku/AnimatedContentControlLib/blob/main/README-Ja.md)

## Summary

This library enables the execution of home-made animations and built-in standard animations when the Content property is changed.
This library provides an AnimatedContentControl control that can be used with WPF.
This library provides AnimatedContentControl controls that can be used with WPF.

This library consists of two assemblies.

| assembly names                 | remarks                                                      |
| ------------------------------ | ------------------------------------------------------------ |
| AnimatedContentControlLib.Core | Assembly that provides constants for built-in animation names that can be used in WPF-independent ViewModel |
| AnimatedContentControlLib.Wpf  | Assembly that provides the AnimatedContentControl body       |

## Installation

Right-click on [Dependencies] in the Solution Explorer of VisualStudio
[Nuget Package Management] and in the search field of the [Browse] tab, type "AnimatedContentControlLib
in the search field of the [Browse] tab and install the same package as shown in the picture below.

![ForReadme1.png](./Img/ForReadme1.png)

If you want to install the software using commands, the necessary commands are described at the following URL.

[NuGet Gallery | AnimatedContentControlLib.Wpf 1.0.0](https://www.nuget.org/packages/AnimatedContentControlLib.Wpf/)
[NuGet Gallery | AnimatedContentControlLib.Core 1.0.0](https://www.nuget.org/packages/AnimatedContentControlLib.Core/)

### Which assembly should I install?

When managing View and ViewModel in the same project, only AnimatedContentControlLib.
Wpf only needs to be installed.
Wpf depends on AnimatedContentControlLib.
Wpf installation only, and AnimatedContentControlLib.Core is also included.

However, if the View and ViewModel are in separate projects
Install AnimatedContentControlLib.Wpf in the View side project, and
Wpf and then install AnimatedContentControlLib.Core in the ViewModel project.

## Sample of using predefined built-in animations

MainWindow.xaml

``` xaml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:acl="http://AnimatedContentControlLib/Wpf/"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <acl:AnimatedContentControl Name="MainContent" 
                                    CurrentStoryboardKey="ModernSlideinRight" />

        <Button Content="Navigate" 
                IsEnabled="{Binding ElementName=MainContent, Path=IsAnimationCompleted}" 
                Click="Button_Click" 
                Grid.Row="1" Margin="5" />
    </Grid>
</Window>
```

MainWindow.xaml.cs

```c#
using System;
using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    Type _currentContent = typeof(Control1);

    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += (s, e) =>
        {
            this.MainContent.Content = Activator.CreateInstance(this._currentContent);
        };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this._currentContent = 
            this._currentContent == typeof(Control1) ? typeof(Control2) : typeof(Control1);
        this.MainContent.Content = Activator.CreateInstance(this._currentContent);
    }
}
```



![ForReadme2.gif](./Img/ForReadme2.gif)

Specify the name of the built-in animation you want to use for the CurrentStoryboardKey property of the AnimatedContentControl class.
The animation can be executed by specifying the name of the built-in animation you want to use in the CurrentStoryboardKey property of the
Content property of the AnimatedContentControl class to execute the animation.
The list of available animations is shown below.
The CurrentStoryboardKey property is assumed to be bound to a ViewModel property.
The CurrentStoryboardKey property is intended to be used in conjunction with a ViewModel property, so it is also possible to use it in combination with the
EmbededAnimations class that holds the Static constant corresponding to each animation name.
Core assembly that holds Static constants for each animation name.

| gif                                     | animateion name    | Static constants defined in AnimatedContentControlLib.Core   |
| --------------------------------------- | ------------------ | ------------------------------------------------------------ |
| ![ForReadme2.gif](./Img/ForReadme6.gif) | SlideinRight       | AnimatedContentControlLib.Core.Constants.EmbededAnimations.SlideinRight |
| ![ForReadme2.gif](./Img/ForReadme7.gif) | SlideinLeft        | AnimatedContentControlLib.Core.Constants.EmbededAnimations.SlideinLeft |
| ![ForReadme2.gif](./Img/ForReadme8.gif) | SlideinUp          | AnimatedContentControlLib.Core.Constants.EmbededAnimations.SlideinUp |
| ![ForReadme2.gif](./Img/ForReadme9.gif) | SlideinDown        | AnimatedContentControlLib.Core.Constants.EmbededAnimations.SlideinDown |
| ![ForReadme2.gif](./Img/ForReadme2.gif) | ModernSlideinRight | AnimatedContentControlLib.Core.Constants.EmbededAnimations.ModernSlideinRight |
| ![ForReadme2.gif](./Img/ForReadme3.gif) | ModernSlideinLeft  | AnimatedContentControlLib.Core.Constants.EmbededAnimations.ModernSlideinLeft |
| ![ForReadme2.gif](./Img/ForReadme4.gif) | ModernSlideinUp    | AnimatedContentControlLib.Core.Constants.EmbededAnimations.ModernSlideinUp |
| ![ForReadme2.gif](./Img/ForReadme5.gif) | ModernSlideinDown  | AnimatedContentControlLib.Core.Constants.EmbededAnimations.ModernSlideinDown |

The above sample uses code-behind for simplicity.
However, it can be used in combination with Prism's RegionNavigation.
RegionManager.RegionName attachment property as shown below.
RegionManager.RegionName property as shown below, it can be executed in the same way as the screen transition using normal ContentControl.

```xaml
<acl:AnimatedContentControl Name="ContentRegion" 
                            prism:RegionManager.RegionName="ContentRegion" 
                            CurrentStoryboardKey="SlideinRight" />
```

## Samples of using homemade animations

MainWindow.xaml

```xaml
<Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:acl="http://AnimatedContentControlLib/Wpf/"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <acl:AnimatedContentControl Name="MainContent" 
                                    CurrentStoryboardKey="MyAnimation">

            <acl:AnimatedContentControl.Storyboards>
            	<Storyboard acl:AnimatedContentControl.Key="MyAnimation">
                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                                   Storyboard.TargetName="PrimaryContent"
                                                   Storyboard.TargetProperty="(UIElement.Opacity)"
                                                   FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="0" />
                        <LinearDoubleKeyFrame KeyTime="0:0:1" Value="1" />
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                                   Storyboard.TargetName="SecondaryImage"
                                                   Storyboard.TargetProperty="(UIElement.Opacity)"
                                                   FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="1" />
                        <LinearDoubleKeyFrame KeyTime="0:0:1" Value="0" />
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                                   Storyboard.TargetName="PrimaryContent"
                                                   Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"
                                                   FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="0" />
                        <LinearDoubleKeyFrame KeyTime="0:0:1" Value="360" />
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                                   Storyboard.TargetName="PrimaryContent"
                                                   Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"
                                                   FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="-45" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="0">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase Bounciness="5" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>

                    <DoubleAnimationUsingKeyFrames BeginTime="0:0:0"
                                                   Storyboard.TargetName="SecondaryImage"
                                                   Storyboard.TargetProperty="(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"
                                                   FillBehavior="Stop">

                        <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="0" />
                        <EasingDoubleKeyFrame KeyTime="0:0:1" Value="45">
                            <EasingDoubleKeyFrame.EasingFunction>
                                <BounceEase Bounciness="5" />
                            </EasingDoubleKeyFrame.EasingFunction>
                        </EasingDoubleKeyFrame>
                    </DoubleAnimationUsingKeyFrames>
                </Storyboard>
            </acl:AnimatedContentControl.Storyboards>
        </acl:AnimatedContentControl>

        <Button Content="Navigate" 
                IsEnabled="{Binding ElementName=MainContent, Path=IsAnimationCompleted}" 
                Click="Button_Click" 
                Grid.Row="1" Margin="5" />
    </Grid>
</Window>

```

MainWindow.xaml.cs

```
using System;
using System.Windows;

namespace WpfApp1;

public partial class MainWindow : Window
{
    Type _currentContent = typeof(Control1);

    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += (s, e) =>
        {
            this.MainContent.Content = Activator.CreateInstance(this._currentContent);
        };
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this._currentContent = 
            this._currentContent == typeof(Control1) ? typeof(Control2) : typeof(Control1);
        this.MainContent.Content = Activator.CreateInstance(this._currentContent);
    }
}
```

![ForReadme10](./Img/ForReadme10.gif)
