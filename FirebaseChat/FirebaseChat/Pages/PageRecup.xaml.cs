using FirebaseChat.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace FirebaseChat.Pages
{
    /// <summary>
    /// Logique d'interaction pour PageRecup.xaml
    /// </summary>
    public partial class PageRecup : Page
    {
        IFirebaseClient fclient;
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv",
            BasePath = "https://chatapplimayrac-default-rtdb.firebaseio.com/"
        };
        public PageRecup()
        {
            InitializeComponent();
            PasswordLib.Content = "Code Mail :";
            try
            {
                fclient = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                Console.WriteLine("il y a eu un problème avec internet");
            }
        }

        private void Recup_clicked(object sender, RoutedEventArgs e)
        {
            bool _mail = false;
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
                string FromEmail = "recupmailchatwpf@gmail.com";
                string FromPswd = "kfftytczobpgztgo";
                CurrentUser.CodeRecup = DateTime.Now.ToString("MMssHH");
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(FromEmail);
                    mail.To.Add(MailTxt.Text);
                    mail.Subject = "Récupération de mot de passe";
                    mail.Body = "<html><body>Votre code est : " + CurrentUser.CodeRecup + "</body></html>";
                    mail.IsBodyHtml = true;

                    var smtp = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential(FromEmail, FromPswd),
                        EnableSsl = true
                    };
                    smtp.Send(mail);
                    recupmsg.Content = "envoyé";

                }
                catch (Exception ex)
                {
                    recupmsg.Content = ex.Message;
                }
            }
        }

        private void Code_Cliked(object sender, MouseButtonEventArgs e)
        {
            string _pswd = "";
            if (CurrentUser.CodeRecup != "")
            {
                if (PasswordTxt.Text == CurrentUser.CodeRecup)
                {
                    FirebaseResponse res = fclient.Get("Users/");
                    Dictionary<string, UserModel> data = JsonConvert.DeserializeObject<Dictionary<string, UserModel>>(res.Body.ToString());
                    foreach (var item in data)
                    {
                        if (item.Value.Mail == MailTxt.Text)
                        {
                            _pswd = item.Value.PassWord;
                            break;
                        }
                    }
                    PasswordTxt.Text = _pswd;
                }
            }
        }
    }
}
