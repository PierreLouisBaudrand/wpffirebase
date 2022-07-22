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
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FirebaseChat.MVVM.Model;
using Newtonsoft.Json;

namespace FirebaseChat
{
    /// <summary>
    /// Logique d'interaction pour PageRegister.xaml
    /// </summary>
    public partial class PageRegister : Page
    {
        IFirebaseClient fclient;
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv",
            BasePath = "https://chatapplimayrac-default-rtdb.firebaseio.com/"
        };
        public PageRegister()
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

        private void Register_Click(object sender, RoutedEventArgs e)
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
                        _exist = true;
                        break;
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
                                            UserModel user = new UserModel()
                                            {
                                                UserName = UsernameTxt.Text,
                                                PassWord = PasswordTxt1.Text,
                                                Mail = MailTxt.Text,
                                                UsernameColor = "#404040"
                                            };

                                            var setter = fclient.Set("Users/" + UsernameTxt.Text, user);
                                            MessageBox.Show("Utilisateur créé", "Information");
                                            UsernameTxt.Text = "";
                                            PasswordTxt1.Text = "";
                                            PasswordTxt2.Text = "";
                                            MailTxt.Text = "";
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
}
