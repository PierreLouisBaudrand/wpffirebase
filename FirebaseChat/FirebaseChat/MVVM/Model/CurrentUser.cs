using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirebaseChat.MVVM.Model
{
    public static class CurrentUser
    {
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
        public static string Mail { get; set; }
        public static string UsernameColor { get; set; }
        public static string UserKey { get; set; }
        public static string CodeRecup { get; set; }
    }
}
