   M S B A M L   `   `   `  ????H  DArchiBeadando, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ArchiBeadandoArchiBeadando  5   	       ArchiBeadando.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ArchiBeadando     5   	     ? Title$  
MainWindow??5   	   $	??450??6   $	??800??6)   .??5	      ?  ? ColumnDefinitions 5
   
   ?? 5      $??3* ?6   ?? 5      $??1* ?6   .+?5   
   ?? -   5         ??Name$ 
zoomSlider??5   "   $??1??6    ? Minimum$	 0.1Q?66    ? Maximum$ 5Q?6D    ? Value$ 1Q?6P   ?? 5   
   $
??Auto=?6   .??5      ?? -   5         ??Name$ myCanvas??5   %   $??0??6   $
??2000??67   $
??2000??6E    ? LayoutTransform 5      ?? 5       ?? ScaleX 6%   ?? 	 ?? Path$	 Value?
 ?? ElementName$
 
zoomSlider?? ?? ScaleY 5      ?? $	 Value?$
 
zoomSlider??5      5      5      5      