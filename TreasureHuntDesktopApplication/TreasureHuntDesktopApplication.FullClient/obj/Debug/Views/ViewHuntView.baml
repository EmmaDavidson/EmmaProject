   M S B A M L   `   `   `  ����d  `TreasureHuntDesktopApplication.FullClient, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null�Iclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities;TreasureHuntDesktopApplication.FullClient.Project_Utilities  5      B    <TreasureHuntDesktopApplication.FullClient.Views.ViewHuntView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      `
validationIclr-namespace:TreasureHuntDesktopApplication.FullClient.Project_Utilities     5      .��5	      � 
�������.+�5
   
   � ���	#FFBDA78A$
��Left=�6&   $	��380��6A   $��	10,10,0,0q�6N   $	��Top=�6a   $	��730��6y     J� Effect  5      3  -System.Windows.Media.Effects.DropShadowEffect  5       � ColumnDefinitions 5      �� 5      $
��134* �6#   �� 5      $	��61* �6#   �� 5      $
��555* �6#   .+�5      �� 
���OO/�$��Tw Cen MT Condensed)�6/   $��30(�6P   $��New Question:��6^   $
��Left=�6v   $��	10,93,0,0q�6�   $	��Top=�6�   $��44��6�   $	��143��6�   $��2��6�   �� 5      -   5        $��NewQuestionTextBox��5   �   $��Tw Cen MT Condensed)�6   $��25(�67   $��Right=�60  $��54��6L  $��
0,87,202,0q�6X   �� TextWrapping$
 Wrap=�6l  $	��Top=�6�  $	��378��6�  $��2��6�  $��1��6�   � ToolTip 6E   ��  �� ElementName$ NewQuestionTextBox�� �� Path$2 ,(Validation.Errors).CurrentItem.ErrorContent���6�   �� *NewQuestion+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . .��6�  5      �� 5      ���	#FF54C8E2$��Stereofidelic)�6-   $��30(�6H   $��Add Question��6W   $
��Left=�6n   $��
351,97,0,0q�6�   $	��Top=�6�   $	��179��6�   $��2��6�   $��34��6�   ��6�   �� *SaveQuestionCommand+	 J� BitmapEffect	 5      F� 5      
 F� Color$
 SaddleBrown��6-    F� 	Direction$	 320Q�6A    F� ShadowDepth$ 25Q�6Q    F� Softness$ 1Q�6b    F� Opacity$	 0.5Q�6p   5      �� 5      ���	#FF54C8E2$��Stereofidelic)�6-   $��30(�6H   $��Print QR Codes��6   $��2��6�   $
��Left=�6�   $��351,213,0,0q�6�   $	��Top=�6�   $	��181��6�   $��35��6�   ��6W   �� *PrintQRCodesCommand+	 5      F� 5!      $
 SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   5#      �� 5$      ���	#FF54C8E2 �� SelectionMode$ Single=�6z   $
��Left=�6�   $	��224��6�   $��
10,146,0,0q�6�   $	��Top=�6�   $	��518��6�   $��3��6�   ��6.   �� *	Questions+ �� SelectedItem 6P   �� *CurrentQuestion+ �� View 5%      �� 5&      .P�5'      ��  �� Width$	 530��6)   $��Question��65    �� DisplayMemberBinding 6G   �� $ 	Question1�5(      5+      �� 5.      ���	#FF54C8E2$��Stereofidelic)�6-   $��30(�6H   $
��Back��6w   $��2��6�   $
��Left=�6�   $��351,294,0,0q�6�   $	��Top=�6�   $	��181��6�   $��38��6�   ��6W   �� *BackCommand+	 5/      F� 51      $
 SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   53      �� 54      ���	#FF54C8E2$��	ExtraBold"�6+   $��Stereofidelic)�6B   $��70(�6]   $��Current Treasure Hunt:��6k   $
��Left=�6�   $��	10,10,0,0q�6�   $	��Top=�6�   $��3��6�   $	��518��6�   �� 55      $��Tw Cen MT Condensed)�6   $��30(�65   $
��Left=�6t   $	��Top=�6�   $��
251,27,0,0q�6�   $��2��6�   $��55��6�   $	��279��6�   ��6C   �� *CurrentTreasureHunt.HuntName+6�   �� 56      ���	#FF54C8E2$��Stereofidelic)�6-   $��30(�6H   $��Leaderboard��6~   $
��Left=�6�   $��351,253,0,0q�6�   $	��Top=�6�   $	��181��6�   $��36��6�   $��2��6�   ��6W   �� *LeaderboardCommand+	 57      F� 59      $
 SaddleBrown��6-   $	 320Q�6A   $ 25Q�6Q   $ 1Q�6b   $	 0.5Q�6p   5;      �� 5<      $��Stereofidelic)�67   $��20(�6R   ���	#FF54C8E2$��Logout��6w   $��2��6�   $
��Left=�6�   $��	457,5,0,0q�6�   $	��Top=�6�   $��75��6�   ��6   �� *LogoutCommand+6�   5>      5C       � 	Resources 5D       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� Self �� RelativeSource��%�   '��       �� 5F   
   " ��6    5G      P� 5H       �@=�HasError . . �5I      ��  �@�ToolTip 60   �� # Z $2 ,(Validation.Errors).CurrentItem.ErrorContent�5J      5L      5M      5N      