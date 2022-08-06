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
using Newtonsoft.Json;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FirebaseChat.MVVM.Model;

namespace FirebaseChat
{
    /// <summary>
    /// Logique d'interaction pour PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        IFirebaseClient fclient;
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv",
            BasePath = "https://chatapplimayrac-default-rtdb.firebaseio.com/"
        };
        public PageLogin()
        {
            InitializeComponent();
            try
            {
                fclient = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                Console.WriteLine("il y a eu un problème avec internet");
            }
        }

        private void Connexion_clicked(object sender, RoutedEventArgs e)
        {
            string _mail = "";
            string _pswd = "";
            string _username = "";
            string _usernamecolor = "";
            string _userkey = "";
            if (MailTxt.Text != "" && PasswordTxt.Password != "")
            {
                FirebaseResponse res = fclient.Get("Users/");
                Dictionary<string, UserModel> data = JsonConvert.DeserializeObject<Dictionary<string, UserModel>>(res.Body.ToString());
                foreach (var item in data)
                {
                    if(item.Value.Mail == MailTxt.Text)
                    {
                        _mail = item.Value.Mail;
                        _pswd = item.Value.PassWord;
                        _username = item.Value.UserName;
                        _usernamecolor = item.Value.UsernameColor;
                        _userkey = item.Value.UserKey;
                        break;
                    }
                }
                if (_mail != "")
                {
                    if (_pswd == PasswordTxt.Password)
                    {
                        if (_username != "")
                        {
                            MessageBox.Show("Connecté", "Test");
                            CurrentUser.Mail = _mail;
                            CurrentUser.PassWord = _pswd;
                            CurrentUser.UserName = _username;
                            CurrentUser.UsernameColor = _usernamecolor;
                            CurrentUser.UserKey = _userkey;
                        }
                        else
                        {
                            MessageBox.Show("Nom d'utilisateur Invalide", "Erreur");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe Incorrect", "Erreur");
                    }
                }
                else
                {
                    MessageBox.Show("Compte non Trouvé", "Erreur");
                }
                
            }
        }
    }
}
