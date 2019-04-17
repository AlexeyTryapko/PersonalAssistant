using PersonalAssistantWCF.Entity;
using System.ServiceModel;

namespace PersonalAssistantWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicePersonalAssistant" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServerPersonalAssitantCallback))]
    public interface IServicePersonalAssistant
    {
        [OperationContract]
        User Login(string login, string password);

        [OperationContract]
        User Registration(string login, string password, string mail);

        [OperationContract]
        void Write(string text);

        [OperationContract]
        void InitializeActiveRecord();

        [OperationContract]
        Note[] RefreshNotes(int userId);

        [OperationContract]
        void DeleteNote(Note note);

        [OperationContract]
        Note AddNote(string noteTitle, string noteMain, User user);

        [OperationContract]
        Meetup[] RefreshMeetups(int userId);

        [OperationContract]
        void DeleteMeetup(Meetup meetup);

        [OperationContract]
        Meetup AddMeetup(string address, string description, User user);

        [OperationContract]
        Contact[] RefreshContacts(int userId);

        [OperationContract]
        void DeleteContact(Contact contact);

        [OperationContract]
        Contact AddContact(string name, string email, User user);

        [OperationContract]
        void Export(string method, User user);
    }

    public interface IServerPersonalAssitantCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }
}
