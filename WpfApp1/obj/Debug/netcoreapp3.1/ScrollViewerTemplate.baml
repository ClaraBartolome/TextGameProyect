   M S B A M L   `   `   `  ????J  FTextGameProyect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TextGameProyectTextGameProyect  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarThumb  ?? 
TargetType ?? Property ?? Value ?? 
TargetType ??Name J? SnapsToDevicePixels ?? CornerRadius ?? RelativeSource ?Name	 ?? Padding
 ]?Name J? IsMouseOver ]? DecreaseRepeatButton J? Opacity ?? PageUpCommand ]? IncreaseRepeatButton ?? PageDownCommand ]? Thumb ?? Triggers P? Property P? Value ?? PageLeftCommand ?? PageRightCommand ? ColumnDefinitions ? RowDefinitions ??Name ??Name ?? CanContentScroll ?? CanHorizontallyScroll ?? CanVerticallyScroll ??Name4   .System.Windows.Automation.AutomationProperties    AutomationId  ? Cursor! ?? ScrollableHeight" ? Maximum# ? Minimum*$ ?? #ComputedVerticalScrollBarVisibility% ?? ViewportHeight& ?? ViewportSize' ? Value( ?? Mode) ?? ScrollableWidth* ?? Orientation,+ ?? %ComputedHorizontalScrollBarVisibility, ?? ViewportWidth- ?? Triggers. :? RoutedEvent	/ W? To0 g? Duration1 ?? 
TargetName2 ?? TargetProperty?? 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      'localclr-namespace:TextGameProyect  5      %?  &	        '?? J    7   7   '?? ?    ?? 5      "  n?6#   .?5   
   ??  ?@?OverridesDefaultStyle$
 true??62   ?? 5   
    ? ??$ false??6&   ?? 5   
    ? ?? 5      ?? 5	      " n?6"   .R?5
      ?? $ 	rectangle??6   $
 True??5   !   
???????$ 3??5   !   #??z??5   !   #??z??5   !   5      5      5      ?? 5      "  ??6   .?5   
   ??  ?@??IsPressAndHoldEnabled$ false??69   ?? 5   
    ?@??IsFlicksEnabled$ false??63   ?? 5   
    ? ??$ 14??6"   ?? 5   
    ? ??$ 	-14 0 0 0q?6#   ?? 5   
    ? ?? 6%   ?? *Width+ ?? *Self+?? 5   
    ? ?? 5      ?? 5      " ??6"   .R?5      ? $ Bg??6   $
 true??6'   
?????? .+?5      ?? $		 0 4q?6!   ._?5      ]? $
 
PART_Track??6$   $
??true??5        #{?z 5!       $??6??5"       $??Center=?5#        5$   "   ?? 5%   &   $ 0Q?63   #??Z 6?    5'   "   ?? 5(   &   $ 0Q?63   #??Z 6?    5*   "   n? 5+   &   8??  6,   5-      5.      5/      50      52       53   
   P? 54       ?@??Orientation$ 
Horizontal=?6-   . ?55      ??  ? ??$
 Auto??6*   ?? 56       ? ??$ 0??6-   ?? 57       ? ??$ 14??6+   ?? 58       ? ?? 6.   ?? *Height+ ?? *Self+?? 59       ? ??$ 	0 -14 0 0q?6+   ?? 5:       ? ?? 5;      ?? 5<      " ??6*   .R?5=      ? $ Bg??6#   $
 true??6/   .+?5>   "   ]? $
 
PART_Track??6(   #{?z 5?   %   $??6??5@   %    5B   &   ?? 5C   *   $ 0Q?67   #??Z 6C    5E   &   ?? 5F   *   $ 0Q?67   #??Z 6C    5H   &   n? 5I   *   8?? 60   5K   #   5L      5M      5O      5P      5R      ?? 5S      "  ??6   .?5T   
   ??  ? ?? 5U      ?? 5V      " ??6"   .R?5W      ? $
 Grid??6   #??z??6)    5X      ?? 5Y      $??* ?6/   ?? 5Z      $
??Auto ?6/    5\      ?? 5]      $??* ?6,   ?? 5^      $
??Auto ?6,   .+?5`      ?? $ Corner??6$   $??1??64   #??? ??6D   $??1??6?   ?? 5a      $! PART_ScrollContentPresenter??61   # z??6V   $ False??6?   $ False??6?   #??z??6?   #??z??6?   $??0??6  #??z??6.  $??0??6Q  ?? 5b      $ PART_VerticalScrollBar??6$   $	 0.2Q?5c   $   $ VerticalScrollBar??5d   $   $  Arrow??5e   $   $??1??5f   $   #" z! 5g   $   $# 0Q?5h   $   $??0??5i   $   #y?z$ 5j   $   #& z% 5l   $   ' 5k   $   ?? *VerticalOffset+$( OneWay=? ?? *TemplatedParent+5l   $   ?? 5m      $ PART_HorizontalScrollBar??6$   $	 0.2Q?5n   $   $ HorizontalScrollBar??5o   $   $  Arrow??5p   $   $??0??5q   $   #" z) 5r   $   $# 0Q?5s   $   $* 
Horizontal=?5t   $   $??1??5u   $   #y?z+ 5v   $   #& z, 5x   $   ' 5w   $   ?? *HorizontalOffset+$( OneWay=? ?? *TemplatedParent+5x   $   5y      - 5z      :? 5{      $. 
MouseEnter??6'   .[?5|      ?? .??5}   "   ?? .??5~   &   W? / 166   $0 0:0:0.3D?6=   $1 PART_VerticalScrollBar??6P   $2 Opacity?6   W? 5   &   / 166   $0 0:0:0.3D?6=   $1 PART_HorizontalScrollBar??6P   $2 Opacity?6?   5?   #   5?      5?      :? 5?      $. 
MouseLeave??6'   .[?5?      ?? .??5?   "   ?? .??5?   &   W? / 0.266   $0 0:0:0.3D?6?   $1 PART_VerticalScrollBar??6R   $2 Opacity?6?   W? 5?   &   / 0.266   $0 0:0:0.3D?6?   $1 PART_HorizontalScrollBar??6R   $2 Opacity?6?   5?   #   5?      5?      5?      5?      5?      5?      