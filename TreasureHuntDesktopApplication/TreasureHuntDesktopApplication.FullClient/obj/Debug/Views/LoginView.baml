   M S B A M L   `   `   `  ����d  `TreasureHuntDesktopApplication.FullClient, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null�Iclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities;TreasureHuntDesktopApplication.FullClient.Project_Utilities  5      ?    9TreasureHuntDesktopApplication.FullClient.Views.LoginView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      `
validationIclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities     5      $	��400��5      $	��750��5	      .��5
      � $��1,0,-10,-10q�6     J� RenderTransformOrigin$  0.5,0.5&�6    
�������z�5   
   V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.054Q�6"   T� 5      5      .+�5   
   � ���	#FFBDAA8A$
��Left=�6&   $	��390��6A   $��	10,10,0,0q�6N   $	��Top=�6a   $	��739��6y    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      .+�5      �� ���	#FF54C8E2$��	ExtraBold"�6+   $��Stereofidelic)�6B   $��70(�6]   $��Login��6k   $
��Left=�6{   $��	26,10,0,0q�6�   $	��Top=�6�   $	��142��6�   $  -3.312,-0.128&�6�   $��74��6�   �� 5      -   5        $��EmailAddressTextBox��5   �   $��20(�6}   $��Tw Cen MT Condensed)�6�   $
��Left=�6�   $��44��6�   $��173,147,0,0q�6�    �� TextWrapping$
 Wrap=�6  $	��Top=�6�  $	��327��6�   � ToolTip 6   ��  �� ElementName$ EmailAddressTextBox�� �� Path$2 ,(Validation.Errors).CurrentItem.ErrorContent���6  �� *EmailAddress+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�	 �� ValidatesOnDataErrors	 . 6�  �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��Email Address:��6^   $
��Left=�6w   $��
21,144,0,0q�6�   $	��Top=�6�   $��47��6�   $	��147��6�   �� 5      
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��	Password:��6^   $
��Left=�6r   $��
64,196,0,0q�6�   $	��Top=�6�   $  
0.51,0.133&�6�   $��47��6�   $	��104��6�   �� 5      ���	#FF54C8E2$��Stereofidelic)�6S   $��30(�6n   
���OO/�$��Login��6�   $
��Left=�6�   $��543,170,0,0q�6�   $	��Top=�6�   $	��167��6�   $��42��6�   ��6   �� *LoginUserCommand+
 J� BitmapEffect
 5      F� 5       F� Color$ SaddleBrown��6-    F� 	Direction$	 320Q�6A    F� ShadowDepth$ 25Q�6Q    F� Softness$ 1Q�6b    F� Opacity$	 0.5Q�6p   5!      �� 5$      ���	#FF54C8E2
���OO/�$��Stereofidelic)�6l   $��30(�6�   $��Register��6�   $
��Left=�6�   $��543,343,0,0q�6�   $	��Top=�6�   $	��167��6�   $��37��6�   ��6-   �� *RegisterCommand+
 5&      F� 5(      $ SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   5+      G� 5,      -   5        $��PasswordTextBox��5,   �   $��20(�6   V   PTreasureHuntDesktopApplication.FullClient.Project_Utilities.PasswordBoxAssistant   BindPassword . $
��Left=�6g  $��173,199,0,0q�6�  $	��Top=�6�  $	��327��6�  $��44��6�   6(   �� $ PasswordTextBox��$2 ,(Validation.Errors).CurrentItem.ErrorContent�   BoundPassword 6�   �� *
Password+$ TwoWay=�$ PropertyChanged=�	 . 6�  �� 5-      $��Tw Cen MT Condensed)�6   $��15(�65   $#��Join the fun! Register now...��6C   $
��Left=�6k   $��404,353,0,0q�6�   $	��Top=�6�   51      53       � 	Resources 55       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� Self �� RelativeSource��%   '��       �� 56   
   " ��6    57      P� 58       �@=�HasError . . �59      ��  �@�ToolTip 60   �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent��� 5:       � �� �	#FF54C8E25;      5<      5>      5@      5B      