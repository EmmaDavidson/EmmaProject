   M S B A M L   `   `   `  ����d  `TreasureHuntDesktopApplication.FullClient, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null�Iclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities;TreasureHuntDesktopApplication.FullClient.Project_Utilities  5      B    <TreasureHuntDesktopApplication.FullClient.Views.RegisterView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      `
validationIclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities     5      $	��400��5      $	��750��5	      .��5
      � 
�������.+�5   
   � $
��Left=�6   $	��380��6*   $��	10,10,0,0q�67   $	��Top=�6J   $	��730��6b   ���	#FFBDA78A  J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5      .+�5      �� ���	#FF54C8E2$��	ExtraBold"�6+   $��Stereofidelic)�6B   $��70(�6]   $��Register��6k   $
��Left=�6~   $��	10,10,0,0q�6�   $	��Top=�6�   �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Name:��6^   $
��Left=�6n   $��159,111,0,0q�6�   $	��Top=�6�    J� RenderTransformOrigin$ -0.125,0.474&�6�   �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Email Address:��6^   $
��Left=�6w   $��
87,159,0,0q�6�   $	��Top=�6�   �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��	Password:��6^   $
��Left=�6r   $��124,211,0,0q�6�   $	��Top=�6�   �� 5      -   5        $��NameTextBox��5   �   $��Tw Cen MT Condensed)�6   $��20(�67   $
��Left=�6�   $��43��6�   $��234,111,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6k  $	��304��6�   � ToolTip 6E   ��  �� ElementName$ NameTextBox�� �� Path$2 ,(Validation.Errors).CurrentItem.ErrorContent���6  �� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . 6�  G� 5      -   5        $��PasswordTextBox��5   �   $��20(�6   V   PTreasureHuntDesktopApplication.FullClient.Project_Utilities.PasswordBoxAssistant	   BindPassword	 . $
��Left=�6g  $��233,213,0,0q�6�  $	��Top=�6�  $	��307��6�  $��41��6�  $ 0.5,0.5&�6�   6(   �� $ PasswordTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent�
   BoundPassword
 6�   �� *
Password+$ TwoWay=�$ PropertyChanged=� . z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.216Q�6*   T� 5      5      5      �� 5      -   5        $��EmailAddressTextBox��5   �   $��Tw Cen MT Condensed)�6   $��20(�67   $
��Left=�6�   $��44��6�   $��234,162,0,0q�6�   $
 Wrap=�6  $	��Top=�6�  $	��304��6�   6E   �� $ EmailAddressTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent���6  �� *EmailAddress+$ TwoWay=�$ PropertyChanged=� . 6�  �� 5       $��30(�6   $��Stereofidelic)�6$   ���	#FF54C8E2$��Register��6~   $
��Left=�6�   $��556,158,0,0q�6�   $	��Top=�6�   $	��143��6�   $��44��6�   ��6V   �� *RegisterUserCommand+ J� BitmapEffect 5!      F� 5#       F� Color$ SaddleBrown��6-    F� 	Direction$	 320Q�6A    F� ShadowDepth$ 25Q�6Q    F� Softness$ 1Q�6b    F� Opacity$	 0.5Q�6p   5%      �� 5&      $��30(�6   $��Stereofidelic)�6$   ���	#FF54C8E2$
��Back��6v   $
��Left=�6�   $��556,211,0,0q�6�   $	��Top=�6�   $	��143��6�   $��43��6�   ��6V   �� *BackCommand+ 5'      F� 5)      $ SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   5+      �� 5,      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Re-type Password:��6_   $
��Left=�6{   $��
53,259,0,0q�6�   $	��Top=�6�   G� 5-      -   5        $��RetypePasswordTextBox��5-   �   $��20(�6   	 . $
��Left=�6z  $��234,263,0,0q�6�  $	��Top=�6�  $	��304��6�  $��39��6�   6(   �� $ RetypePasswordTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent�
 6�   �� *RetypedPassword+$ TwoWay=�$ PropertyChanged=� . 6�  53      55      56      