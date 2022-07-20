using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseChat.Core;
using FirebaseChat.MVVM.Model;

namespace FirebaseChat.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public RelayCommand SendCommand { get; set; }

        private ContactModel _selectedcontact;
        public ContactModel SelectedContact
        {
            get { return _selectedcontact; }
            set 
            { 
                _selectedcontact = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set 
            { 
                _message = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            SendCommand = new RelayCommand(o =>
            {
                Messages.Add(new MessageModel
                {
                    Message = Message,
                    IsFirstMessage = false
                });
                Message = "";
            });

            Messages.Add(new MessageModel
            {
                Username = "Chat Limayrac",
                UsernameColor = "#409aff",
                ImageSource = "./Icons/limayraclogo.png",
                Message = "Test",
                Time = DateTime.Now,
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
                    Message = "Test",
                    Time = DateTime.Now,
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
