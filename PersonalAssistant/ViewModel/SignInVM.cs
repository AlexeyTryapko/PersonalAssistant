using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PersonalAssistant.ServicePersonalAssistant;

namespace PersonalAssistant.ViewModel
{
    public class SignInVM : INotifyPropertyChanged, IServicePersonalAssistantCallback
    {
        
        private IMainWindow _MainWindow;
        ServicePersonalAssistantClient client;

        public SignInVM(IMainWindow mainWindow)
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

        private RelayCommand _LoginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _LoginCommand ??
                    (_LoginCommand = new RelayCommand(obj => 
                    {
                        try
                        {
                            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                            var passwordBx = obj as PasswordBox;
                            var password = passwordBx.Password;
                            User user = client.Login(InputTextLogin, password);
                            if (user != null)
                            {
                                AuthUser = user;
                                _MainWindow.LoadView(ViewType.Main);
                            }
                            else
                            {
                                MessageBox.Show("Uncorrect Login or Password");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Uncaught Error");
                        }
                    }));
            }
        }


        private RelayCommand _LoadSignUpUCCommand;
        public RelayCommand LoadSignUpUCCommand
        {
            get
            {
                return _LoadSignUpUCCommand ??
                  (_LoadSignUpUCCommand = new RelayCommand(obj => {
                      _MainWindow.LoadView(ViewType.SignUp);
                  }));
            }
        }

        public void MsgCallback(string msg)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
