   M S B A M L   `   `   `  ���� G  CFirebaseChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FirebaseChatFirebaseChat  I)clr-namespace:FirebaseChat.MVVM.ViewModelFirebaseChat.MVVM.ViewModel      FirebaseChat.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:FirebaseChat     ?	viewmodel)clr-namespace:FirebaseChat.MVVM.ViewModel     $	��650��$
��1200��  � WindowStyle$
  None=� � AllowsTransparency$
 True�� � 
ResizeMode$ CanResizeWithGrip=� � DataContext /   )FirebaseChat.MVVM.ViewModel.MainViewModel  .���  � RowDefinitions �� $��25 ���  � ColumnDefinitions �� $	��200 ��� .+��� -   $��2�����#236bac._�� $��Stretch=�.+��� $��Firebase Chat��
�������$��SemiBold"��� $��Right=� �� Orientation$ 
Horizontal=�.+��� -   $��20��$��20��$
��🗕��
������ $��0q�$
��Bold"�
��������� -   $��20��$��20��$	��□��
������ $��0q�$
��Bold"�
��������� -   $��20��$��20��$��X��
������ $��0q�$
��Bold"�
�������� ���#498bc7$��1�� �� $��50 ��� �� $��60 �.+��� $��Channel��$��Center=�$��Medium"�
�������$��8,0,0,0q��� 
������ $��0q�$��1��   ContactCard#��[  ���� *
Contacts+ �� SelectedItem �� *SelectedContact+�� $��2��$ 
Horizontal=����#236bac$
��Left=�$	��200��.+��� -    ��Name$ ConnectColor��	 �� CornerRadius$	 25��$��20��$��20�����#3bff6f$��5,0,0,0q��� $��Center=�.+��� -   
 ��Name$
 UsernameBox��$��Username��
�������$��SemiBold"�$��	0,-5,0,-5q��� -   $
 ConnectStatus��$��	Connecté��
�������$��SemiBold"�$��10(�$��	0,-8,0,-5q��� -   $
 ConnectMail��$�� ��$y�Hidden=�$��0��$��0���� -	    ��Name$ ConnectButton��$��	Connexion��
�������$��SemiBold"�$��24��$��80�����#6dabe4$��0,0,0,0q�
������ �� -
   $��./Icons/param.png��$��20��$��20�� �� BitmapScalingMode$
 Fant=�$��5,0,0,0q�� $��1��$��1��
������� �� $��50 ��� �� $��70 �.+��� ���#236bac$��0,0,0,2q����#498bc7._�� $��Stretch=�$��8q�.+��� $��	@Username��
�������$
��Bold"�$��5,0,5,0q�$��Center=��� ���#236bac$��2,0,0,0q�
������ $��1��._��� -    ��Name$ ListMessageTxt��
������ $��0q�  ChatItem#��[ $��8,0,0,0q�$��1������ *SelectedContact.Messages+�� ���#236bac$��2,0,0,0q�
������ $��2��._��  �� �� $��90 �.+��� -    ��Name$ TextMessage��$��1��$��50��$��Message @Username��  
MessageBox#��[ $��2���� -    ��Name$ ImageUpload��$
 Fant=�$��50��$��50��$��5,0,5,0q�$��Right=��� $ 
Horizontal=�$��Right=�$��1��$��0,0,16,0q�.+��� -   $��./Icons/plus.png��$��20��$��20��$
 Fant=�$��5,5,5,0q��� -   $��./Icons/send.png��$��20��$��20��$
 Fant=�$��5,5,16,0q�