﻿using FirebaseChat.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using FirebaseChat.MVVM.Model;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;

namespace FirebaseChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Deconnexion();
            DeconnexionVisuel();
            CurrentUser.CodeRecup = "";
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {

                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Enter_clicked(object sender, MouseButtonEventArgs e)
        {
            //Finir
        }

        private void Connexion_clicked(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.UserName == "")
            {
                Login log = new Login();
                log.ShowDialog();
                if (CurrentUser.UserName != "")
                {
                    ConnexionVisuel();
                }
            }
            else
            {
                Deconnexion();
                DeconnexionVisuel();
            }
            
        }
        private void ConnexionVisuel()
        {
            if (CurrentUser.UserName != "")
            {
                UsernameBox.Content = CurrentUser.UserName;
                ConnectStatus.Content = "Connecté  ";
                ConnectColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(59, 255, 111));
                ConnectButton.Content = "Déconnexion";
            }
        }
        private void DeconnexionVisuel()
        {
            if (CurrentUser.UserName == "")
            {
                UsernameBox.Content = "Pseudo";
                ConnectStatus.Content = "Déconnecté";
                ConnectButton.Content = "Connexion";
                ConnectColor.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(128, 128, 128));
            }
        }
        private static void Deconnexion()
        {
            CurrentUser.UserName = "";
            CurrentUser.PassWord = "";
            CurrentUser.Mail = "";
            CurrentUser.UsernameColor = "";
            CurrentUser.UserKey = "";
        }
        private void Param_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (CurrentUser.UserName != "")
            {
                Param param = new Param();
                param.ShowDialog();
                ConnexionVisuel();
            }
        }

        private void PlusImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image Files(*.jpg) | *jpg";
            if (ofd.ShowDialog() == true)
            {
                Bitmap img = new Bitmap(ofd.FileName);
                ImageUpload.Source = BitmapToImageSource(img);
                System.IO.MemoryStream ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                byte[] byteImage = ms.ToArray();
                CurrentUser.CodeImage = Convert.ToBase64String(byteImage);
            }
        }
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit(); 
                return bitmapimage;
            }
        }
    }
}
