   M S B A M L   `   `   `  ����X  TAnimatedContentControlLib.Wpf, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:AnimatedContentControlLib.Wpf.Controls&AnimatedContentControlLib.Wpf.Controls  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   RotateBigToSmoleToBig  g� 	BeginTime �� 
TargetName �� TargetProperty g� FillBehavior O� KeyTime O� Value9   3System.Windows.Media.Animation.EasingDoubleKeyFrame    EasingFunction2  ,System.Windows.Media.Animation.QuadraticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     <acl4clr-namespace:AnimatedContentControlLib.Wpf.Controls  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %R  &	        �� 5      .��5   
   U� 	  0:0:06(   $ ContentTransform��5   (   $9 3(acl:TransformContentControl.RotateCenterXPosRatio)�5   (   $
 Stop=�5   (   .\�5      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5      $ 0:0:0.8��6%   $	 0.5Q�67   5      U� 5   
   	  0:0:06(   $ ImageTransform��5   (   $9 3(acl:TransformContentControl.RotateCenterXPosRatio)�5   (   $
 Stop=�5   (   .\�5      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5      $ 0:0:0.8��6%   $	 0.5Q�67   5      U� 5!   
   	  0:0:06(   $ ContentTransform��5"   (   $9 3(acl:TransformContentControl.RotateCenterYPosRatio)�5#   (   $
 Stop=�5$   (   .\�5&      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5'      $ 0:0:0.8��6%   $	 0.5Q�67   5(      U� 5*   
   	  0:0:06(   $ ImageTransform��5+   (   $9 3(acl:TransformContentControl.RotateCenterYPosRatio)�5,   (   $
 Stop=�5-   (   .\�5/      n� $ 0:0:0��6%   $	 0.5Q�65   n� 50      $ 0:0:0.8��6%   $	 0.5Q�67   51      U� 53   
   	  0:0:06(   $ ContentTransform��54   (   $8 2(acl:TransformContentControl.ScaleCenterXPosRatio)�55   (   $
 Stop=�56   (   .\�58      n� $ 0:0:0��6%   $	 0.5Q�65   n� 59      $ 0:0:0.8��6%   $	 0.5Q�67   5:      U� 5<   
   	  0:0:06(   $ ContentTransform��5=   (   $8 2(acl:TransformContentControl.ScaleCenterYPosRatio)�5>   (   $
 Stop=�5?   (   .\�5A      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5B      $ 0:0:0.8��6%   $	 0.5Q�67   5C      U� 5E   
   	  0:0:06(   $ ImageTransform��5F   (   $8 2(acl:TransformContentControl.ScaleCenterXPosRatio)�5G   (   $
 Stop=�5H   (   .\�5J      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5K      $ 0:0:0.8��6%   $	 0.5Q�67   5L      U� 5N   
   	  0:0:06(   $ ImageTransform��5O   (   $8 2(acl:TransformContentControl.ScaleCenterYPosRatio)�5P   (   $
 Stop=�5Q   (   .\�5S      n� $ 0:0:0��6%   $	 0.5Q�65   n� 5T      $ 0:0:0.8��6%   $	 0.5Q�67   5U      U� 5W   
   	  0:0:06(   $ ContentTransform��5X   (   $4 .(acl:TransformContentControl.RotateAngleRatio)�5Y   (   $
 Stop=�5Z   (   .\�5\      �� $ 0:0:0��6#   $ 0Q�63   �� 5]      $ 0:0:0.2��6#   $ 1Q�65   �� 5^      $ 0:0:0.4��6#   $ -1Q�65   �� 5_      $ 0:0:0.6��6#   $ 0Q�65   �� 5`      $ 0:0:0.8��6#   $ 1Q�65   5a      U� 5c   
   	  0:0:06(   $ ImageTransform��5d   (   $4 .(acl:TransformContentControl.RotateAngleRatio)�5e   (   $
 Stop=�5f   (   .\�5h      �� $ 0:0:0��6#   $ 0Q�63   �� 5i      $ 0:0:0.2��6#   $ 1Q�65   �� 5j      $ 0:0:0.4��6#   $ -1Q�65   �� 5k      $ 0:0:0.6��6#   $ 0Q�65   �� 5l      $ 0:0:0.8��6#   $ 1Q�65   5m      U� 5o   
   	  0:0:06(   $ ContentTransform��5p   (   $/ )(acl:TransformContentControl.ScaleXRatio)�5q   (   $
 Stop=�5r   (   .\�5t      n� $ 0:0:0��6%   $ 0Q�65   n� 5u      $ 0:0:0.4��6%   $ 0Q�67      5v      $ 0:0:0.8��6#   $ 1Q�65    5w        5x      $ EaseIn=�6$   5z      5{      U� 5}   
   	  0:0:06(   $ ContentTransform��5~   (   $/ )(acl:TransformContentControl.ScaleYRatio)�5   (   $
 Stop=�5�   (   .\�5�      n� $ 0:0:0��6%   $ 0Q�65   n� 5�      $ 0:0:0.4��6%   $ 0Q�67      5�      $ 0:0:0.8��6#   $ 1Q�65    5�        5�      $ EaseIn=�6$   5�      5�      U� 5�   
   	  0:0:06(   $ ImageTransform��5�   (   $/ )(acl:TransformContentControl.ScaleXRatio)�5�   (   $
 Stop=�5�   (   .\�5�      n� $ 0:0:0��6%   $ 1Q�65      5�      $ 0:0:0.4��6#   $ 0Q�65    5�        5�      $ EaseIn=�6$   5�      n� 5�      $ 0:0:0.8��6%   $ 0Q�67   5�      U� 5�   
   	  0:0:06(   $ ImageTransform��5�   (   $/ )(acl:TransformContentControl.ScaleYRatio)�5�   (   $
 Stop=�5�   (   .\�5�      n� $ 0:0:0��6%   $ 1Q�65      5�      $ 0:0:0.4��6#   $ 0Q�65    5�        5�      $ EaseIn=�6$   5�      n� 5�      $ 0:0:0.8��6%   $ 0Q�67   5�      5�      5�      