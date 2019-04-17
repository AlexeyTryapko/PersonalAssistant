using Castle.ActiveRecord;
using CastleActiveRecord.Persistence;
using NHibernate.Criterion;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalAssistantWCF.Entity
{
    [ActiveRecord(Table = "notes", Schema = "public")]
    public class Note : GenericEntity<Note>, INotifyPropertyChanged
    {
        private int id;
        private string noteTitle;
        private string noteMain;
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

        [Property("note_title")]
        public string NoteTitle
        {
            get { return noteTitle; }
            set
            {
                noteTitle = value;
                OnPropertyChanged("NoteTitle");

            }
        }

        [Property("note_main")]
        public string NoteMain
        {
            get { return noteMain; }
            set
            {
                noteMain = value;
                OnPropertyChanged("NoteMain");

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

        public static Note[] GetNotesByUser(int userId)
        {
            DetachedCriteria dc = DetachedCriteria.For<Note>();
            dc.Add(Expression.Eq("User.Id", userId));
            return FindAll(dc);
        }

        public override string ToString()
        {
            return "Note title: " + this.NoteTitle + "\tNote main: " + this.NoteMain;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
