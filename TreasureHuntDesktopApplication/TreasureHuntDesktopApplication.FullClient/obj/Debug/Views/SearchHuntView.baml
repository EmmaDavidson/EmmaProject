   M S B A M L   `   `   `  ����-  )TreasureHuntDesktopApplication.FullClientD    >TreasureHuntDesktopApplication.FullClient.Views.SearchHuntView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      a ]DevExpress.Xpf.Core.v13.1, Version=13.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a;dx2http://schemas.devexpress.com/winfx/2008/xaml/core  5      <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  6L   $	��400��5      $	��750��5	      .��5
      � 
�������.+�5   
   � ���	#FFBDA78A$
��Left=�6&   $	��380��6A   $��	10,10,0,0q�6N   $	��Top=�6a   $	��730��6y     J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5      .+�5      �� $��Tw Cen MT Condensed)�6   $��20(�68    �� DisplayMemberPath$ HuntName��6l   $
��Left=�6�   $��173,185,0,0q�6�   $	��Top=�6�   $	��339��6  $��43��6  
����������	#FF0F4E72��6F   �� *TreasureHunts+ �� SelectedItem 6�   �� *CurrentTreasureHunt+ �� Mode$ TwoWay=�66  �� 5      $��	ExtraBold"�6   $��Stereofidelic)�6+   $��70(�6F   ���	#FF54C8E2$��Search Treasure Hunts��6k   $
��Left=�6�   $��	10,10,0,0q�6�   $	��Top=�6�    � DataContext 5        5      5      �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��
Hunt name:��6^   $
��Left=�6s   $��
49,185,0,0q�6�   $	��Top=�6�   $	��150��6�   �� 5      $��30(�6   $��Stereofidelic)�6$   ���	#FF54C8E2$��Search��6|   $
��Left=�6�   $��548,203,0,0q�6�   $	��Top=�6�   $	��153��6�   $��39��6�   ���	#FF3E3E3E��6V   �� *SearchHuntCommand+ J� BitmapEffect 5      F� 5       F� Color$ SaddleBrown��6-    F� 	Direction$	 320Q�6A    F� ShadowDepth$ 25Q�6Q   	 F� Softness$	 1Q�6b   
 F� Opacity$	
 0.5Q�6p   5      �� 5      $��30(�6   $��Stereofidelic)�6%   ���	#FF54C8E2$��Create New Hunt��6�   $
��Left=�6�   $��548,163,0,0q�6�   $	��Top=�6�   $	��153��6�   $��35��6�   ���	#FF3E3C3C��6W   �� *CreateNewHuntCommand+ 5      F� 5!      $ SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $	 1Q�6b   $	
 0.5Q�6p   5#      �� 5$      $��Stereofidelic)�67   $��20(�6R   ���	#FF54C8E2$��Logout��6w   $
��Left=�6�   $��
645,10,0,0q�6�   $	��Top=�6�   $��75��6�   ��6   �� *LogoutCommand+6�   5&      5)      5*      