   M S B A M L   `   `   `  ����O  KTestProject.Wpf.Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:TestProject.Wpf.ConvertersTestProject.Wpf.Converters  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089    BooleanToVisibilityConverterC    =TestProject.Wpf.Converters.BooleanToHiddenVisibilityConverter & "BooleanToHiddenVisibilityConverter3   -TestProject.Wpf.Converters.CompositeConverter - )InverseBooleanToHiddenVisibilityConverter8   2TestProject.Wpf.Converters.InverseBooleanConverter  	TreeStyle  �� 
TargetType �� Property  ListBackgroundBrush �� Value  TreeItemStyle �� Mode �� 
TargetType  PrimaryForeground1Brush �� Triggers P� Property P� Value & "ListItemBackgroundHighlightedBrush # ListItemBackgroundSelectedBrush 	 TreeItemIdentStyle �� Orientation 
 PrimaryForeground3Brush	 �� 	Converter
 � RowDefinitions A�Name d�Name �� 	ClickMode  TreeToggleButtonStyle d� 	IsChecked�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      7
converters(clr-namespace:TestProject.Wpf.Converters  5      %�  &	        &	      &	 &     &	 Y     &	 �    &		 Q    7  7  7  7  �� 5      6#      5      64     5        5   
      5	   
   5
      �� 5      "  ��6   .�5   
   ��  � ��# �  6'   �� 5   
    � ��# �  6/   �� 5   
    � �� 6(   �� *VisibleItems+$ OneWay=��� 5   
    � �� 5      �� 5      " ��6"   .R�5      � .+�5      �� #��z��6!   ._�5      �� .��5   "   �� 5      5      5      5      5      5      �� 5      "  ��6"   .�5   
   ��  � ��# �  6'   �� 5   
    � �� 5       �� 5!      " ��6"   .R�5"      �� #��z��6   ._�5#      �� 5$      5%      5'       5(   
   P� 5)       �@J�IsMouseOver$
 True��6-   . �5*      ��  � ��# �  6/   5+      P� 5,       �@��
IsSelected$
 True��6,   . �5-      ��  � ��# �  6/   5.      50      �� 51      "  ��6'   .�52   
   ��  � �� 53      �� 54      " ��6"   .R�55      �� $ 
Horizontal=�6!   .+�56      �� ��6'   �� *TreeElements+$ OneTime=���57      �� 58   "   .R�59   &   �� $ 
Horizontal=�61   5:   #   ��5<      �� 5=   "   .R�5>   &   � $��24��6+   .+�5?   *   �� #��� 
 5@   -   $��2,0,0,0q�5A   -   $��11,0,0,0q�5B   -   $��Stretch=�5C   -   y�5D   -   �� $ OneTime=�8	   5E   '   5F   #   5H      � 5I      $��24��6   
 5J      �� 5K   "   �� 5L   "   �� 5M   "   .+�5O      �� $��0��5P   !   #��� 
 5Q   !   $��2,0,0,0q�5R   !   $��11,0,0,0q�5S   !   $��Stretch=�5T   !   �� 5U      $��2��5V   !   #��� 
 5W   !   $��2,0,0,0q�5X   !   $��11,0,0,0q�5Y   !   $��Stretch=�5Z   !   y�5[   !   �� *HasNextSibling+$ OneTime=�8	  A� 5\      $ Ellipse��5^   !   $��1��5]   !   $��6��5_   !   $��6��5`   !   #���  5a   !   y�5b   !   �� *HasChildren+$ OneTime=�8	  d� 5c      $ ToggleButton��5e   !   $��1��5d   !   $|�False��5f   !   $��24��5g   !   $ Press=�5h   !   #���  5k   !    5i   !   �� *
IsExpanded+$ TwoWay=�y�5j   !   �� *HasChildren+$ OneTime=�8	  5k   !   5l      5m      5n      5p      5q      5r      