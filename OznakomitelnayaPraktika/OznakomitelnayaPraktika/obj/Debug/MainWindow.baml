   M S B A M L   `   `   `  ����R  NOznakomitelnayaPraktika, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:OznakomitelnayaPraktikaOznakomitelnayaPraktika  5   	   (    "OznakomitelnayaPraktika.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:OznakomitelnayaPraktika     5   	     � Title$  
MainWindow��5   	   $��526.3��6   $��1008.2��6+   .��5	      � $��0,0,-0.4,11q�6    � RowDefinitions 5
   
   �� 5      $
��195* �6   �� 5      $	��29* �6    � ColumnDefinitions 5   
   �� 5      $	��35* �6   �� 5      $
��362* �6   .+�5   
   �� -   5        $��Выбрать БД��5      $
��Left=�6&   $��2,20,0,0q�6A   $	��Top=�6S   $	��118��6k   $��19��6w   $��2��6�   6�   �� 5   
   -   5        $��Добавить��5      $
��Left=�6$   $��2,44,0,0q�6?   $	��Top=�6Q   $	��118��6i    J� RenderTransformOrigin$ -0.637,-0.323&�6u   $��19��6�   $��2��6�   6�   �� 5   
   -   5        $'��!Отделы/Управления��5      $��2��60   $
��Left=�6E   $��2,68,0,0q�6`   $	��Top=�6r   $	��118��6�   6�   �� 5   
   -   5        $��Обновить��5      $
��Left=�6$   $��2,92,0,0q�6?   $	��Top=�6Q   $	��118��6i   $��19��6u   $��2��6�   6�   �� 5   
   -   5        $#��Поиск/Фильрация��5      $
��Left=�6+   $��	2,116,0,0q�6F   $	��Top=�6Y   $	��118��6q   $��2��6}   6�   �� 5   
   -   5        $#��Экспорт таблицы��5      $
��Left=�6+   $��	2,140,0,0q�6F   $	��Top=�6Y   $	��118��6q   $ 0.5,0.5&�6}   $��2��6�   $��21��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ myGrid��5      $
��Left=�6#   $	��456��6>   $��	48,20,0,0q�6K   $	��Top=�6^   $	��855��6v   $ 0.5,0.5&�6�   $��1��6�      
IsReadOnly .    CanUserReorderColumns .     CanUserResizeColumns .     CanUserSortColumns .  	   AutoGenerateColumns	 . $��2��6`  
 � ContextMenu
 5      �� 5      .@�5      m� -   5         m�Name$ show_employ_workbook��5      $8��2Трудовая книжка сотрудника��6=   6a   m� 5      -	   5        $ show_employ_document��5      $-��'Документы сотрудника��6=   6[   m� 5      -
   5        $ Delete_employ��5      $)��#Удалить сотрудника��66   6R   5      5!      5"      5#      