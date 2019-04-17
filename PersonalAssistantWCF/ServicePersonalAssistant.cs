using System;
using System.ServiceModel;
using PersonalAssistantWCF.Entity;
using PersonalAssistantWCF.Logger;
using PersonalAssistantWCF.Persistence;
using PersonalAssistantWCF.Strategy.Classes;

namespace PersonalAssistantWCF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    public class ServicePersonalAssistant : IServicePersonalAssistant
    {
        Facade facade = new Facade(new UserLogger(), new ContactLogger(), new MeetupLogger(), new NoteLogger());
        public User Login(string login, string password)
        {
            try
            {
                InitializeActiveRecord();
                User[] usersALL = User.FindAll();
                facade.initializeSystem(usersALL);
                User[] users = User.FindAllByProperty("Login", login);
                if (users[0].Password == password)
                {
                    Console.WriteLine("\nUSER WAS LOGGED\n");
                    facade.printUserData(users[0]);
                    return users[0];
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public User Registration(string login, string password, string mail)
        {
            try
            {
                InitializeActiveRecord();
                Boolean checkUniq = true;
                User[] users = User.FindAll();
                facade.initializeSystem(users);
                for (int i = 0; i < users.Length; i++)
                {
                    if (users[i].Login == login)
                    {
                        checkUniq = false;
                        break;
                    }
                }
                if (checkUniq)
                {
                    User user = new User();
                    user.Login = login;
                    user.Password = password;
                    user.Mail = mail;
                    User.Save(user);
                    Console.WriteLine("User registred" + user.ToString());
                    return user;
                }
                else
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }
        }

        public void InitializeActiveRecord()
        {
            ActiveRecordPostgresConfig arPostgresConfig =
                new ActiveRecordPostgresConfig(activeRecordClassesAssemblyName: "PersonalAssistantWCF", showSql: false);

            arPostgresConfig.Initialize();
        }

        public Contact AddContact(string name, string email, User user)
        {
            Boolean checkUniq = true;
            Contact[] contactsDB = Contact.FindAll();
            for (int i = 0; i < contactsDB.Length; i++)
            {
                if (contactsDB[i].Name == name && contactsDB[i].User.Id == user.Id)
                {
                    checkUniq = false;
                }
            }
            if (checkUniq)
            {
                Contact contact = new Contact();
                contact.Name = name;
                contact.Email = email;
                contact.User = user;
                Contact.Save(contact);
                return contact;
            }
            else
            {
                return null;
            }
        }

        public Meetup AddMeetup(string address, string description, User user)
        {
            try
            {

                Meetup meetup = new Meetup();
                meetup.Address = address;
                meetup.Description = description;
                meetup.User = user;

                Meetup.Save(meetup);
                return meetup;
            }
            catch
            {
                return null;
            }
        }

        public Note AddNote(string noteTitle, string noteMain, User user)
        {
            InitializeActiveRecord();
            Boolean checkUniq = true;
            Note[] notesDB = Note.FindAll();
            for (int i = 0; i < notesDB.Length; i++)
            {
                if (notesDB[i].NoteTitle == noteTitle && notesDB[i].User.Id == user.Id)
                {
                    checkUniq = false;
                }
            }
            if (checkUniq)
            {
                Note note = new Note();
                note.NoteTitle = noteTitle;
                note.NoteMain = noteMain;
                note.User = user;
                Note.Save(note);
                return note;
            }
            else
            {
                return null;
            }
        }

        public void DeleteContact(Contact contact)
        {
            try
            {
                Contact.Delete(contact);
            }
            catch
            {

            }
        }

        public void DeleteMeetup(Meetup meetup)
        {
            try
            {
                Meetup.Delete(meetup);
            }
            catch
            {

            }
        }

        public void DeleteNote(Note note)
        {
            try
            {
                Note.Delete(note);
            }
            catch
            {

            }
        } 

        public Contact[] RefreshContacts(int userId)
        {
            return Contact.GetContactsByUser(userId);
        }

        public Meetup[] RefreshMeetups(int userId)
        {
            return Meetup.GetMeetupsByUser(userId);
        }

        public Note[] RefreshNotes(int userId)
        {
            return Note.GetNotesByUser(userId);
        }

        public void Write(string text)
        {
            Console.WriteLine(text);
        }

        public void Export(string method, User user)
        {
            switch(method)
            {
                case "file":
                    TXTExport exportTXT = new TXTExport();
                    exportTXT.ExportData(user);
                    break;
                case "mail":
                    MailExport exportMail = new MailExport();
                    exportMail.ExportData(user);
                    break;
            }
        }
    }
}
