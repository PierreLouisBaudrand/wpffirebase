using FirebaseChat.MVVM.Model;
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
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace FirebaseChat
{
    /// <summary>
    /// Logique d'interaction pour Param.xaml
    /// </summary>
    public partial class Param : Window
    {
        string _color;
        IFirebaseClient fclient;
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv",
            BasePath = "https://chatapplimayrac-default-rtdb.firebaseio.com/"
        };
        public Param()
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
            MailTxt.Text = CurrentUser.Mail;
            UsernameTxt.Text = CurrentUser.UserName;
            PasswordTxt1.Text = CurrentUser.PassWord;
            PasswordTxt2.Text = CurrentUser.PassWord;
            //MessageBox.Show(CurrentUser.UsernameColor + "FF");
            ClrPcker.SelectedColor = (Color)ColorConverter.ConvertFromString(CurrentUser.UsernameColor.Insert(1,"FF"));
        }
        private void ClrPcker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var _temp = (Color)e.NewValue;
            _color = _temp.ToString();
        }

        private void UpdateParam_Click(object sender, RoutedEventArgs e)
        {
            {
                bool _checkMaj = false;
                bool _checkMin = false;
                bool _checkLong = false;
                bool _checkSpe = false;
                bool _mail = false;
                bool _exist = false;

                if (UsernameTxt.Text != "" && PasswordTxt1.Text != "" && PasswordTxt2.Text != "" && MailTxt.Text != "")
                {
                    FirebaseResponse res = fclient.Get("Users/");
                    Dictionary<string, UserModel> data = JsonConvert.DeserializeObject<Dictionary<string, UserModel>>(res.Body.ToString());
                    foreach (var item in data)
                    {
                        if (item.Value.Mail == MailTxt.Text || item.Value.UserName == UsernameTxt.Text)
                        {
                            if(item.Value.Mail != CurrentUser.Mail || item.Value.UserName != CurrentUser.UserName)
                            {
                                _exist = true;
                                break;
                            }
                        }
                    }
                    if (_exist != true)
                    {
                        var trimmedEmail = MailTxt.Text.Trim();
                        if (trimmedEmail.EndsWith("."))
                        {
                            _mail = false;
                        }
                        try
                        {
                            var addr = new System.Net.Mail.MailAddress(MailTxt.Text);
                            if (addr.Address == trimmedEmail)
                            {
                                _mail = true;
                            }
                        }
                        catch
                        {
                            _mail = false;
                        }
                        if (_mail == true)
                        {
                            if (PasswordTxt1.Text == PasswordTxt2.Text)
                            {
                                char[] _pswd = PasswordTxt1.Text.ToCharArray();
                                if (PasswordTxt1.Text.Length > 8)
                                {
                                    _checkLong = true;
                                }
                                foreach (char ch in _pswd)
                                {
                                    if (_checkMaj != true && Char.IsUpper(ch) == true)
                                    {
                                        _checkMaj = true;
                                    }
                                    if (_checkMin != true && Char.IsLower(ch) == true)
                                    {
                                        _checkMin = true;
                                    }
                                    if (_checkSpe != true && Char.IsLetterOrDigit(ch) == false)
                                    {
                                        _checkSpe = true;
                                    }
                                }

                                if (_checkLong == true)
                                {
                                    if (_checkMaj == true)
                                    {
                                        if (_checkMin == true)
                                        {
                                            if (_checkSpe == true)
                                            {
                                                if (CurrentUser.UserKey != "")
                                                {
                                                    UserModel user = new UserModel()
                                                    {
                                                        UserName = UsernameTxt.Text,
                                                        PassWord = PasswordTxt1.Text,
                                                        Mail = MailTxt.Text,
                                                        UsernameColor = _color.Remove(1,2),
                                                        UserKey = CurrentUser.UserKey
                                                    };

                                                    var setter = fclient.Update("Users/" + CurrentUser.UserKey, user);
                                                    MessageBox.Show("Mise à jour des informations", "Information");
                                                    CurrentUser.UserName = UsernameTxt.Text;
                                                    CurrentUser.Mail = MailTxt.Text;
                                                    CurrentUser.PassWord = PasswordTxt1.Text;
                                                    CurrentUser.UsernameColor = _color.Remove(1,2);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Un caractère spécial minimum", "Erreur");
                                                PasswordTxt1.Text = "";
                                                PasswordTxt2.Text = "";
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Une minuscule minimum", "Erreur");
                                            PasswordTxt1.Text = "";
                                            PasswordTxt2.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Une majuscule minimum", "Erreur");
                                        PasswordTxt1.Text = "";
                                        PasswordTxt2.Text = "";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Mot de passe trop court (8 caractères minimum)", "Erreur");
                                    PasswordTxt1.Text = "";
                                    PasswordTxt2.Text = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Confirmation de Mot de passe Incorrect", "Erreur");
                                PasswordTxt1.Text = "";
                                PasswordTxt2.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mail Incorrect", "Erreur");
                            MailTxt.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("L'utilisateur existe déjà", "Erreur");
                        UsernameTxt.Text = "";
                        PasswordTxt1.Text = "";
                        PasswordTxt2.Text = "";
                        MailTxt.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Informations incomplètes", "Erreur");
                }
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Supp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CurrentUser.UserKey != "")
            {
                var delete = fclient.Delete("Users/" + CurrentUser.UserKey);
                MessageBox.Show("L'utilisateur a bien été supprimé", "Information");
            }
            else
            {
                MessageBox.Show("Erreur d'utilisateur", "Erreur");
            }
        }
    }
}
