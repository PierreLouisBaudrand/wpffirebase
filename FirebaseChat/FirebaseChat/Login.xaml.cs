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
using System.Windows.Shapes;

namespace FirebaseChat
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        PageLogin pageLog = new PageLogin();
        PageRegister pageReg = new PageRegister();

        public Login()
        {
            InitializeComponent();
            MainLogin.Content = new PageLogin();
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
            //Finir Minimize
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            //Pas de Plein Ecran
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GoToLogin_clicked(object sender, RoutedEventArgs e)
        {
            if (MainLogin.Content != pageLog)
            {
                MainLogin.Content = pageLog;
            }
        }

        private void GoToRegister_clicked(object sender, RoutedEventArgs e)
        {
            if (MainLogin.Content != pageReg)
            {
                MainLogin.Content = pageReg;
            }
        }
    }
}
