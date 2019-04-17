using PersonalAssistant.ServicePersonalAssistant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PersonalAssistant.ViewModel
{
    public class SignUpVM : INotifyPropertyChanged, IServicePersonalAssistantCallback
    {

        private  IMainWindow _MainWindow;
        ServicePersonalAssistantClient client;

        public SignUpVM(IMainWindow mainWindow)
        {
            _MainWindow = mainWindow;
        }

        private string inputTextLogin;
        public string InputTextLogin
        {
            get { return inputTextLogin; }
            set
            {
                inputTextLogin = value;
                OnPropertyChanged("InputTextLogin");
            }
        }

        private string inputTextMail;
        public string InputTextMail
        {
            get { return inputTextMail; }
            set
            {
                inputTextMail = value;
                OnPropertyChanged("InputTextMail");
            }
        }

        private string inputTextPassword;
        public string InputTextPassword
        {
            get { return inputTextPassword; }
            set
            {
                inputTextPassword = value;
                OnPropertyChanged("InputTextPassword");
            }
        }

        private User authUser;
        public User AuthUser
        {
            get { return authUser; }
            set
            {
                authUser = value;
                OnPropertyChanged("AuthUser");
            }
        }
       
        private RelayCommand _RegistrationCommand;
        public RelayCommand RegistrationCommand
        {
            get
            {
                return _RegistrationCommand ??
                   (_RegistrationCommand = new RelayCommand(obj =>
                   {
                       try
                       {
                                   client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                   var passwordBx = obj as PasswordBox;
                                   var password = passwordBx.Password;
                                   User regUser = client.Registration(InputTextLogin, password, InputTextMail);
                                   if(regUser != null)
                                   {
                                       AuthUser = regUser;
                                       _MainWindow.LoadView(ViewType.Main);
                                   }else
                                   {
                                       MessageBox.Show("Error try again");
                                   }
   
                       }
                       catch(Exception e)
                       {
                           Console.WriteLine(e.Message);
                           MessageBox.Show("Uncaught Error");
                       }
                   }));
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void MsgCallback(string msg)
        {
        }
    }
}
