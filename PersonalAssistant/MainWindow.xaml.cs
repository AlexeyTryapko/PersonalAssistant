using PersonalAssistant.ServicePersonalAssistant;
using PersonalAssistant.View;
using PersonalAssistant.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalAssistant
{
    public interface IMainWindow
    {
        void LoadView(ViewType typeView);
    }

    public enum ViewType
    {
        SignIn,
        SignUp,
        Main,
        Notes,
        Contacts,
        Meetings,
    }
    public partial class MainWindow : Window, IMainWindow
    {
        public SignInVM _SignInVM { get; set; }
        public SignUpVM _SignUpVM { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            LoadView(ViewType.SignIn);
        }

        public void LoadView(ViewType typeView)
        {
            switch (typeView)
            {
                case ViewType.SignIn:
                    SignInUC signInV = new SignInUC();
                    SignInVM signInVM = new SignInVM(this);
                    _SignInVM = signInVM;
                    signInV.DataContext = signInVM;
                    this.AccessView.Content = signInV;
                    this.NavView.Content = null;
                    this.MainView.Content = null;
                    break;
                case ViewType.SignUp:
                    SignUpUC signUpV = new SignUpUC();
                    SignUpVM signUpVM = new SignUpVM(this);
                    _SignUpVM = signUpVM;
                    signUpV.DataContext = signUpVM;
                    this.AccessView.Content = signUpV;
                    this.NavView.Content = null;
                    this.MainView.Content = null;
                    break;
                case ViewType.Main:
                    NavigationUC navV = new NavigationUC();
                    if(_SignInVM.AuthUser != null)
                    {
                        NavigationVM navVM = new NavigationVM(this, _SignInVM.AuthUser);
                        navV.DataContext = navVM;
                        NotesUC notesV = new NotesUC();
                        NotesVM notesVM = new NotesVM(this, _SignInVM.AuthUser);
                        notesV.DataContext = notesVM;
                        this.NavView.Content = navV;
                        this.MainView.Content = notesV;
                        this.AccessView.Content = null;
                    }
                    else
                    {
                        NavigationVM navVM = new NavigationVM(this, _SignUpVM.AuthUser);
                        navV.DataContext = navVM;
                        NotesUC notesV = new NotesUC();
                        NotesVM notesVM = new NotesVM(this, _SignUpVM.AuthUser);
                        notesV.DataContext = notesVM;
                        this.NavView.Content = navV;
                        this.MainView.Content = notesV;
                        this.AccessView.Content = null;
                    }

                    
                    break;
                case ViewType.Notes:
                    if(_SignInVM.AuthUser != null)
                    {
                        NotesUC notesV1 = new NotesUC();
                        NotesVM notesVM1 = new NotesVM(this, _SignInVM.AuthUser);
                        notesV1.DataContext = notesVM1;
                        this.MainView.Content = notesV1;
                    }else
                    {
                        NotesUC notesV1 = new NotesUC();
                        NotesVM notesVM1 = new NotesVM(this, _SignUpVM.AuthUser);
                        notesV1.DataContext = notesVM1;
                        this.MainView.Content = notesV1;
                    }              
                    break;
                case ViewType.Contacts:
                    if (_SignInVM.AuthUser != null)
                    {
                        ContactsUC contactsV = new ContactsUC();
                        ContactsVM contactsVM = new ContactsVM(this, _SignInVM.AuthUser);
                        contactsV.DataContext = contactsVM;
                        this.MainView.Content = contactsV;
                    }
                    else
                    {
                        ContactsUC contactsV = new ContactsUC();
                        ContactsVM contactsVM = new ContactsVM(this, _SignUpVM.AuthUser);
                        contactsV.DataContext = contactsVM;
                        this.MainView.Content = contactsV;
                    }
                    break;
                case ViewType.Meetings:
                    if (_SignInVM.AuthUser != null)
                    {
                        Meetings meetingsV = new Meetings();
                        MeetingsVM meetingsVM = new MeetingsVM(this, _SignInVM.AuthUser);
                        meetingsV.DataContext = meetingsVM;
                        this.MainView.Content = meetingsV;
                    }
                    else
                    {
                        Meetings meetingsV = new Meetings();
                        MeetingsVM meetingsVM = new MeetingsVM(this, _SignUpVM.AuthUser);
                        meetingsV.DataContext = meetingsVM;
                        this.MainView.Content = meetingsV;
                    }
                    break;

            }

        }
    }
}
