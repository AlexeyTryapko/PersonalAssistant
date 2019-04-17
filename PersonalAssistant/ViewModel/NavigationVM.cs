using PersonalAssistant.ServicePersonalAssistant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistant.ViewModel
{
    class NavigationVM : IServicePersonalAssistantCallback
    {
        private IMainWindow _MainWindow;

        ServicePersonalAssistantClient client;

        public NavigationVM(IMainWindow mainWindow, User currentUser)
        {
            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            _MainWindow = mainWindow;
            CurrentUser = currentUser;
            CurrentUserName = currentUser.Login;
            CurrentUserMail = currentUser.Mail;
        }


        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
            }
        }

        private string currentUserName;
        public string CurrentUserName
        {
            get { return currentUserName; }
            set
            {
                currentUserName = value;
            }
        }
        private string currentUserMail;
        public string CurrentUserMail
        {
            get { return currentUserMail; }
            set
            {
                currentUserMail = value;
            }
        }
        private RelayCommand _LoadSignInUCCommand;
        public RelayCommand LoadSignInUCCommand
        {
            get
            {
                return _LoadSignInUCCommand ??
                  (_LoadSignInUCCommand = new RelayCommand(obj => {
                      _MainWindow.LoadView(ViewType.SignIn);
                  }));
            }
        }

        private RelayCommand _LoadNotesUCCommand;
        public RelayCommand LoadNotesUCCommand
        {
            get
            {
                return _LoadNotesUCCommand ??
                  (_LoadNotesUCCommand = new RelayCommand(obj => {
                      _MainWindow.LoadView(ViewType.Notes);
                  }));
            }
        }

        private RelayCommand _LoadContactsUCCommand;
        public RelayCommand LoadContactsUCCommand
        {
            get
            {
                return _LoadContactsUCCommand ??
                  (_LoadContactsUCCommand = new RelayCommand(obj => {
                      _MainWindow.LoadView(ViewType.Contacts);
                  }));
            }
        }

        private RelayCommand _LoadMeetingsUCCommand;
        public RelayCommand LoadMeetingsUCCommand
        {
            get
            {
                return _LoadMeetingsUCCommand ??
                  (_LoadMeetingsUCCommand = new RelayCommand(obj => {
                      _MainWindow.LoadView(ViewType.Meetings);
                  }));
            }
        }

        private RelayCommand exportToFile;
        public RelayCommand ExportToFIle
        {
            get
            {
                return exportToFile ??
                    (exportToFile = new RelayCommand(obj =>
                    {
                       client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                       client.Export("file", CurrentUser);
                    }));
            }
        }

        private RelayCommand exportToMail;
        public RelayCommand ExportToMail
        {
            get
            {
                return exportToMail ??
                    (exportToMail = new RelayCommand(obj =>
                    {
                        client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                        client.Export("mail", CurrentUser);
                    }));
            }
        }

        public void MsgCallback(string msg)
        {

        }
    }
}
