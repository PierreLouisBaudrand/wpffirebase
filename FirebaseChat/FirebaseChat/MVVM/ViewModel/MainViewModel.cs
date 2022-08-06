﻿using System;
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
using System.Windows.Threading;
using System.Windows;

namespace FirebaseChat.MVVM.ViewModel
{
     public class MainViewModel : ObservableObject
    {
        //Connexion FireBase
        IFirebaseClient fclient;
        FirebaseClient firebaseClient;

        //Variables
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
            if (CurrentUser.UserName != "")
            {
                if (Message != "")
                {
                    var msg = new MessageModel
                    {
                        Username = CurrentUser.UserName,
                        UsernameColor = CurrentUser.UsernameColor,
                        ImageSource = "./Icons/limayraclogo.png",
                        MessageTxt = Message,
                        Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    };
                    var setter = fclient.Set("Messages/" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg);
                }
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

            //Data Stream
            var observable = firebaseClient.Child("Messages").AsObservable<MessageModel>().Subscribe
                (d => App.Current.Dispatcher.Invoke((System.Action)delegate
                {
                    if (d.Object.ImageSource != "")
                    {
                        //BitmapImage image = generateImageSource(d.Object.ImageSource);
                    
                        Messages.Add(new MessageModel
                        {
                            Username = d.Object.Username,
                            UsernameColor = d.Object.UsernameColor,
                            ImageSource = d.Object.ImageSource,
                            //PictureImage = image,
                            MessageTxt = d.Object.MessageTxt,
                            Time = d.Object.Time,
                            IsNativeOrigin = false,
                            IsFirstMessage = true
                        });
                    }
                    else
                    {
                        Messages.Add(new MessageModel
                        {
                            Username = d.Object.Username,
                            UsernameColor = d.Object.UsernameColor,
                            ImageSource = d.Object.ImageSource,
                            MessageTxt = d.Object.MessageTxt,
                            Time = d.Object.Time,
                            IsNativeOrigin = false,
                            IsFirstMessage = true
                        });
                    }
                }));

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
                Message = "";
            });

            Contacts.Add(new ContactModel
            {
                Username = "Chat Limayrac",
                ImageSource = "./Icons/limayraclogo.png",
                Messages = Messages
            });
        }
    }
}
