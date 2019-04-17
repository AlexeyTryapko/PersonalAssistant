using Castle.ActiveRecord;
using CastleActiveRecord.Persistence;
using NHibernate.Criterion;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalAssistantWCF.Entity
{
    [ActiveRecord(Table = "meetups", Schema = "public")]
    public class Meetup : GenericEntity<Meetup>, INotifyPropertyChanged
    {
        private int id;
        private string address;
        private string description;
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

        [Property("address")]
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");

            }
        }


        [Property("description")]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");

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

        public static Meetup[] GetMeetupsByUser(int userId)
        {
            DetachedCriteria dc = DetachedCriteria.For<Meetup>();
            dc.Add(Expression.Eq("User.Id", userId));
            return FindAll(dc);
        }

        public override string ToString()
        {
            return "Meetup address: " + this.Address + "\tMeetup description: " + this.Description;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
