   M S B A M L   `   `   `  ����C  ?TiliToli, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TiliToliTiliToli  5   	       TiliToli.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:TiliToli     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� $��Fájl��6   .@�5      m� $��Új játék��6    m� Command 6-   �� *NewGameCommand+m� 5      $	��3x3��6    6(   �� *NewGameCommand3+m� 5      $	��4x4��6    6(   �� *NewGameCommand4+m� 5      $	��5x5��6    6(   �� *NewGameCommand5+5      5      �� 5   
   $��1��6   ��6$   �� *Fields+��5      �� 5      .R�5      A�  A� Rows 6&   �� *
GridSize+ A� Columns 6@   �� *
GridSize+5      ��5       �� 5"      .R�5#      �� $|�False��6~    J� RenderTransformOrigin$ 0.5, 0.5&�6�   $��24(�6�   $
��Bold"�6�   ��6!   �� *StepCommand+��6A   �� *Number+��6e   �� *Text+z�5&      �� 5'   "    �� ScaleX$ 1Q�61    �� ScaleY$ 1Q�6<   ��5)      �� 5+   "    �� 
TargetType" ��6(   	 �� Triggers	 5,   &   �� 5.   *   
 �� Binding
 66   �� *Text+ �� Value 06O   .a�5/   .   ��  �� Property � �� �� Value
 �  ��6K   50   +   52   #    � Triggers 54      :� 56   "    :� RoutedEvent$ Button.Loaded��6/   .[�57   &   �� .��58   *   �� .��59   .   W�  g� Duration$ 0:00:02D�6>    W� From 06Q   	 W� To 16Z    �� TargetProperty$ Opacity�6a   5:   +   5;   '   5<   #   :� 5=   "   $ Button.Click��6/   .[�5>   &   �� .��5?   *   �� .��5@   .   �� $ 	0:00:00.2D�6K   $* $(Foreground).(SolidColorBrush.Color)�6`   g�5B   6   p�  �� Value$	 Red��6L    �� KeyTime$ 0:00:00��6X   p� 5C   6   $ Black��6L   $ 	0:00:00.2��6Z   5E   /   W� 5F   .   $ 	0:00:00.1D�6>    16S    0.56\    g� AutoReverse$
 True��6e   $/ )(RenderTransform).(ScaleTransform.ScaleX)�6x   W� 5G   .   $ 	0:00:00.1D�6>    16S    0.56\   $
 True��6e   $/ )(RenderTransform).(ScaleTransform.ScaleY)�6x   5H   +   5I   '   5J   #   5L      5M      ��5O      �� 5Q      .�5R      ��  � �� 65   �� *X+�� 5S       � �� 68   �� *Y+5T      5V      �� 5X   
   $��2��6   .@�5[      �� .��5\      �� $��Játékidő: ��6   5]      �� 5^      .��5_      �� ��6   �� *
GameTime+5`      �� 5a      �� 5b      5c      5d      5e      