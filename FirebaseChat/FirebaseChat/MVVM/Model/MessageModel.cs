using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FirebaseChat.MVVM.Model
{
    public class MessageModel
    {
        public string Username { get; set; }
        public string UsernameColor { get; set; }
        public string ImageSource { get; set; }
        public string MessageTxt { get; set; }
        public BitmapImage Image { get; set; }
        public string Time { get; set; }
        public bool HasImage { get; set; }
        public string ImageMsg { get; set; }
        public bool? IsFirstMessage { get; set; }
    }
}
