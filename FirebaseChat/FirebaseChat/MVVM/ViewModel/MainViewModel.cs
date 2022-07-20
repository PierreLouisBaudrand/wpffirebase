using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseChat.Core;
using FirebaseChat.MVVM.Model;
using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace FirebaseChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        //Connexion FireBase
        public IFirebaseClient fclient;
        public FirebaseClient firebaseClient;

        //Variables
        public string username = "pierre";
        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedcontact;
        private string _message;

        //Affichage
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }
        
        public ContactModel SelectedContact
        {
            get { return _selectedcontact; }
            set 
            { 
                _selectedcontact = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                OnPropertyChanged();
            }
        }
        public void PostMsgFirebase()
        {
            if(Message!="")
            {
                var msg = new MessageModel
                {
                    Username = "Chat Limayrac",
                    UsernameColor = "#409aff",
                    ImageSource = "",
                    MessageTxt = Message,
                    Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    IsNativeOrigin = false,
                    IsFirstMessage = true
                };
                var setter = fclient.Set("Messages/" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").Replace('/', '-'), msg);
            }
        }

        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv",
            BasePath = "https://chatapplimayrac-default-rtdb.firebaseio.com/"
        };

        public MainViewModel()
        {
            var auth = "lVE5xwYYR6693QYet0L3XAteKwAObA2Y4hS6W6lv";
            firebaseClient = new FirebaseClient(
              "https://chatapplimayrac-default-rtdb.firebaseio.com/",
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(auth)
              });

            try
            {
                fclient = new FireSharp.FirebaseClient(fcon);
            }
            catch
            {
                Console.WriteLine("il y a eu un problème avec internet");
            }

            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                PostMsgFirebase();
                //Messages.Add(msg);
                Message = "";
            });

            Messages.Add(new MessageModel
            {
                Username = "Chat Limayrac",
                UsernameColor = "#409aff",
                ImageSource = "./Icons/limayraclogo.png",
                MessageTxt = "Test",
                Time = DateTime.Now.ToString(),
                IsNativeOrigin = false,
                IsFirstMessage = true
            });

            for (int i = 0; i < 3; i++)
            {
                Messages.Add(new MessageModel
                {
                    Username = "Chat Limayrac",
                    UsernameColor = "#409aff",
                    ImageSource = "./Icons/limayraclogo.png",
                    MessageTxt = "Test",
                    Time = DateTime.Now.ToString(),
                    IsNativeOrigin = false,
                    IsFirstMessage = false
                });
            }

            Contacts.Add(new ContactModel
            {
                Username = "Chat Limayrac",
                ImageSource = "./Icons/limayraclogo.png",
                Messages = Messages
            });
        }
    }
}
