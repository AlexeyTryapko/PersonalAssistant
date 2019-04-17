using Castle.ActiveRecord;
using CastleActiveRecord.Persistence;
using NHibernate.Criterion;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalAssistantWCF.Entity
{
        [ActiveRecord(Table = "contacts", Schema = "public")]
        public class Contact : GenericEntity<Contact>, INotifyPropertyChanged
        {
            private int id;
            private string name;
            private string email;
            private User user;

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
            [Property("name")]
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");

                }
            }
            [Property("email")]
            public string Email
            {
                get { return email; }
                set
                {
                    email = value;
                    OnPropertyChanged("Email");

                }
            }

            [BelongsTo("user_id")]
            public User User
            {
                get { return user; }
                set
                {
                    user = value;
                    OnPropertyChanged("User");

                }
            }

            public static Contact[] GetContactsByUser(int userId)
            {
                DetachedCriteria dc = DetachedCriteria.For<Contact>();
                dc.Add(Expression.Eq("User.Id", userId));
                return FindAll(dc);
            }


        public override string ToString()
        {
            return "Contact name: " + this.Name + "\tContact email: " + this.Email;
        }

        public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
}
