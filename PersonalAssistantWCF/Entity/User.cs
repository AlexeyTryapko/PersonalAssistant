using Castle.ActiveRecord;
using CastleActiveRecord.Persistence;
using NHibernate.Criterion;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalAssistantWCF.Entity
{
    [ActiveRecord(Table = "users", Schema = "public")]
    public class User : GenericEntity<User>, INotifyPropertyChanged
    {
        private int id;
        private string login;
        private string password;
        private string mail;

        [PrimaryKey(PrimaryKeyType.Increment, "id")]
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");

            }
        }

        [Property("login")]
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");

            }
        }

        [Property("password")]
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");

            }
        }

        [Property("mail")]
        public string Mail
        {
            get { return mail; }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");

            }
        }

        public override string ToString()
        {
            return "\tLogin " + this.Login + "\tPassword: " + this.Password + "\tMail: " + this.Mail;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
