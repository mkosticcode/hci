   M S B A M L   `   `   `  ????I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NetworkServiceNetworkService  5   	   ;"clr-namespace:NetworkService.ViewsNetworkService.Views  5   	   C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5   	       NetworkService.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:NetworkService     5   	   4views"clr-namespace:NetworkService.Views     5   	   <	viewModel&clr-namespace:NetworkService.ViewModel     5   	     ? Title$  
MainWindow??5   	   $	??650??6   $	??800??6)   
??????? ? DataContext 5      (   "NetworkService.MainWindowViewModel  5   
    ? 	Resources 5       ?? DataType/  )NetworkService.ViewModel.ParkingViewModel&    NetworkService.Views.ParkingView/   )NetworkService.ViewModel.NetworkViewModel&    NetworkService.Views.NetworkView-   'NetworkService.ViewModel.GraphViewModel$   NetworkService.Views.GraphView??%?   (??       5      *, +)(?? 2     5      *, +)(?? d     5      *, +)?? 5   
   "  6   .R?5        5      ?? 5   
   "  6   .R?5        5      ?? 5   
   "  6   .R?5        5      5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5    
   ]? $??0??6   $??5q?6!   .+?5!      o? $	??Top=?6   .@?5"      m? $??Parking??6    m? CommandParameter$ parking??5#       m? Command 5"   ,   ?? *
NavCommand+5#      m? 5$      $??Prikaz mreze??6   $ network??5%       5$   1   ?? *
NavCommand+5%      m? 5&      $??Prikaz grafa??6   $ graph??5'       5&   1   ?? *
NavCommand+5'      5(      5)      ? 5*   
   $??1??6   .+?5+      ?? ??6   ?? *CurrentViewModel+5,      5-      5.      