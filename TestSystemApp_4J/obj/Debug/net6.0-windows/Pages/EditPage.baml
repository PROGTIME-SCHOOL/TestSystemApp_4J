   M S B A M L   `   `   `  ����K  GTestSystemApp_4J, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TestSystemApp_4J.PagesTestSystemApp_4J.Pages  5      %    TestSystemApp_4J.Pages.EditPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:TestSystemApp_4J.Pages     5        N� Title$  EditPage��5	      .��5      �  � RowDefinitions 5   
   �� 5      $��60 �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ backToMainPage��5      $��0��6)   $	��100��66   $��15q�6B   $
��Back��6N   
�������$
��Left=�5      6,   �� 5   
   -   5         ��Name$ comboBoxTables��5      $	��130��6+   $��15q�67   6C   �� 5   
   -   5        $ saveChanges��5      $��0��6&   $	��100��63   $��15q�6?   $
��Save��6K   
��� ���$��Right=�5      6-   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ testDataGrid��5      $��1��6)      AutoGenerateColumns$ False��66      CanUserAddRows$
 True��5      
����İ�$y�Hidden=�6E      Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Id��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 0.5* 6H      
IsReadOnly$
 True��6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6U     5"      $
	 Test��6%   $
 * 6L    63   �� *Name+6L     5#      $	 Description��6%   $
 * 6Z    6:   �� *Description+6Z   5%        5'   
   -   5        $ questionDataGrid��5'      $��1��6-   $��1��6:   $ False��6J   $
 True��5(      
�������$y�Hidden=�6B   ${�False��6V    5*        5+      $	 Id��6%   $

 0.5* 6H   $
 True��6U    61   �� *Id+6U     5,      $	 Question��6%   $
 * 6P    67   �� *Text+6P     5-      $	 Answer��6%   $
 * 6P    65   �� *Answer+6P   5/        51   
   -   5        $ userDataGrid��51      $��1��6)   $ False��66   $ False��52      
�������$y�Hidden=�6>   $��2��6R    54        55      $	 Id��6%   $

 0.5* 6H   $
 True��6U    61   �� *Id+6U     56      $
	 User��6%   $
 * 6M    63   �� *Login+6M     57      $
	 Role��6%   $
 * 6L    63   �� *Role+6L   59      5:      5;      