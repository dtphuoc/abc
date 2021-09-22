using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;

namespace WcfChat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public static ChatPrEntities db = new ChatPrEntities();

        public List<Chat> GetAllChat()
        {
            List<Chat> listChat = new List<Chat>();
            var chats = db.Chats.ToList();
            foreach (var c in chats)
            {
                Chat chat = new Chat();
                chat.Id = c.Id;
                chat.Content = c.Content;
                chat.UserName = c.UserName;
                chat.SentTime = c.SentTime;
                listChat.Add(chat);
            }
            return listChat;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public bool Login(string Username, string Password)
        {
            var getUsr = from u in db.Users where u.UserName == Username && u.Password == Password select u;
            var res = getUsr.Any() && getUsr != null ? true : false;
            return res;
        }

        public string Register(string Username, string Password)
        {
            var check = db.Users.FirstOrDefault(u => u.UserName == Username);
            if (check == null)
            {
                User user = new User();
                user.UserName = Username;
                user.Password = Password;
                db.Users.Add(user);
                db.SaveChanges();
                return "Register thanh cong";
            }
            else
            {
                return "User is already exist";
            }
        }

        public string SendChat(string Content, string Username)
        {
            Chat chat = new Chat();
            chat.Content = Content;
            chat.UserName = Username;
            chat.SentTime = DateTime.Now;
            db.Chats.Add(chat);
            db.SaveChanges();
            return "Send chat thanh cong";
        }
    }
}
