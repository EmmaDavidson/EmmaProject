   M S B A M L   `   `   `  ����d  `TreasureHuntDesktopApplication.FullClient, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null�Iclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities;TreasureHuntDesktopApplication.FullClient.Project_Utilities  5      D    >TreasureHuntDesktopApplication.FullClient.Views.CreateHuntView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      `
validationIclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities     5      $	��400��5      $	��750��5	      .��5      � 
�������.+�5   
   � ���	#FFBDA78A$
��Left=�6&   $	��380��6A   $��	10,10,0,0q�6N   $	��Top=�6a   $	��730��6y     J� Opacity$	  100Q�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      .+�5      �� $��Stereofidelic)�6   ���	#FF54C8E2$��30(�6G   $
��Save��6}   $
��Left=�6�   $��532,140,0,0q�6�   $	��Top=�6�   $	��162��6�   $��44��6�    J� RenderTransformOrigin$ 1.853,0.308&�6�   ��6U   �� *SaveHuntNameCommand+ J� BitmapEffect 5      F� 5       F� Color$ SaddleBrown��6-    F� 	Direction$	 320Q�6A    F� ShadowDepth$ 25Q�6Q    F� Softness$ 1Q�6b    F� Opacity$	 0.5Q�6p   5      �� 5      $��Stereofidelic)�67   $��20(�6R   ���	#FF54C8E2$��Logout��6w   $
��Left=�6�   $��
645,10,0,0q�6�   $	��Top=�6�   $��75��6�   ��6   �� *LogoutCommand+6�   �� 5      ���	#FF54C8E2$��Stereofidelic)�6,   $��30(�6G   $
��Back��6u   $
��Left=�6�   $��532,189,0,0q�6�   $	��Top=�6�   $	��162��6�   $��46��6�   $ 1.853,0.308&�6�   ��6U   �� *BackCommand+ 5      F� 5      $ SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   5      �� 5       -   5        $��HuntNameTextBox��5    y   $��Tw Cen MT Condensed)�6�   $��20(�6�   $
��Left=�6  $��40��65  $��
214,90,0,0q�6A  	 �� TextWrapping$
	 Wrap=�6U  $	��Top=�6i  $	��279��6�  $ 0.547,0.478&�6�  
 � ToolTip
 6   ��  �� ElementName$ HuntNameTextBox�� �� Path$2 ,(Validation.Errors).CurrentItem.ErrorContent���6�   �� *
HuntName+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . .��6�  5!      �� 5"      ���	#FF54C8E2$��	ExtraBold"�6+   $��Stereofidelic)�6B   $��70(�6]   $��Create a new hunt��6k   $
��Left=�6�   $��	10,10,0,0q�6�   $	��Top=�6�   $	��351��6�   �� 5#      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��
Hunt name:��6^   $
��Left=�6s   $��	88,87,0,0q�6�   $	��Top=�6�   $	��121��6�   �� 5(      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Set Password:��6^   $
��Left=�6v   $��
74,135,0,0q�6�   $	��Top=�6�   $ -4.763,0.192&�6�   $��36��6�   G� 5*      -   5        $��PasswordTextBox��5*   �   $��Tw Cen MT Condensed)�6   $��20(�6;   V   PTreasureHuntDesktopApplication.FullClient.Project_Utilities.PasswordBoxAssistant   BindPassword . $
��Left=�6�  $��215,136,0,0q�6�  $	��Top=�6�  $	��278��6�  $��44��6�  
 6I   �� $ PasswordTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent�   BoundPassword 6�   �� *
Password+ �� Mode$ TwoWay=�$ PropertyChanged=� . 6�  �� 5+      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Retype Password:��6^   $
��Left=�6y   $��
40,185,0,0q�6�   $	��Top=�6�   $��50��6�   G� 5-      -   5        $��RetypePasswordTextBox��5-   �   $��Tw Cen MT Condensed)�6   $��20(�6;    . $
��Left=�6�  $��215,185,0,0q�6�  $	��Top=�6�  $	��278��6�  $��46��6�  
 6I   �� $ RetypePasswordTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent� 6  �� *RetypedPassword+$ TwoWay=�$ PropertyChanged=� . 6�  �� 5.      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Hunt description:��6^   $
��Left=�6z   $��
48,240,0,0q�6�   $	��Top=�6�   $��51��6�   �� 5/      -   5        $��DescriptionBox��5/     $��Tw Cen MT Condensed)�6   $��20(�67   $
��Left=�6  $	��123��66  $��215,236,0,0q�6C  $
	 Wrap=�6X  $	��Top=�6l  $	��278��6�  $ 0.491,0.125&�6�  
 6E   �� $ DescriptionBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent���6�   �� *Description+$ PropertyChanged=� . 6�  52      56      57      