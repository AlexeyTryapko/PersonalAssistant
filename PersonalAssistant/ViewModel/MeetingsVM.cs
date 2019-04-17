using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using PersonalAssistant.ServicePersonalAssistant;

namespace PersonalAssistant.ViewModel
{
    class MeetingsVM : INotifyPropertyChanged, IServicePersonalAssistantCallback
    {

        private IMainWindow _MainWindow;

        ServicePersonalAssistantClient client;
        public ObservableCollection<Meetup> Meetups { get; set; }

        public MeetingsVM(IMainWindow mainWindow, User currentUser)
        {
            _MainWindow = mainWindow;
            CurrentUser = currentUser;

            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Meetups = new ObservableCollection<Meetup> { };
            Meetup[] meetups_from_db = client.RefreshMeetups(CurrentUser.Id);
            for (int i = 0; i < meetups_from_db.Length; i++)
                Meetups.Add(meetups_from_db[i]);
        }

        private User currentUser;
        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        private Meetup selectedMeetup;
        public Meetup SelectedMeetup
        {
            get { return selectedMeetup; }
            set
            {
                selectedMeetup = value;
                if (SelectedMeetup != null)
                {
                    InputMeetupAddress = SelectedMeetup.Address;
                    InputMeetupDescription = SelectedMeetup.Description;
                }
                else
                {
                    InputMeetupAddress = "";
                    InputMeetupDescription = "";
                }

                OnPropertyChanged("SelectedMeetup");
            }
        }

        private string inputMeetupAddress;
        public string InputMeetupAddress
        {
            get { return inputMeetupAddress; }
            set
            {
                inputMeetupAddress = value;
                OnPropertyChanged("InputMeetupAddress");
            }
        }

        private string inputMeetupDescription;
        public string InputMeetupDescription
        {
            get { return inputMeetupDescription; }
            set
            {
                inputMeetupDescription = value;
                OnPropertyChanged("InputMeetupDescription");
            }
        }

        public void RefreshMeetups()
        {
            Meetups = new ObservableCollection<Meetup> { };
            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Meetup[] meetups_from_db = client.RefreshMeetups(CurrentUser.Id);
            for (int i = 0; i < meetups_from_db.Length; i++)
                Meetups.Add(meetups_from_db[i]);
            OnPropertyChanged("Meetups");
        }

        private RelayCommand addMeetupCommand;
        public RelayCommand AddMeetupCommand
        {
            get
            {
                return addMeetupCommand ??
                    (addMeetupCommand = new RelayCommand(obj =>
                    {
                        if (InputMeetupAddress != null && InputMeetupDescription != null)
                        {
                            try
                            {
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                Meetup newMeetup = client.AddMeetup(InputMeetupAddress, InputMeetupDescription, CurrentUser);
                                if (newMeetup != null)
                                {
                                    Meetups.Insert(0, newMeetup);
                                    RefreshMeetups();
                                    InputMeetupAddress = "";
                                    InputMeetupDescription = "";
                                }
                                else
                                {
                                    MessageBox.Show("Попробуйте еще раз.");
                                }
                            }
                            catch
                            {

                                MessageBox.Show("Ошибка. Попробуйте еще раз.");
                            }
                        }

                    }));
            }
        }

        private RelayCommand editMeetupCommand;
        public RelayCommand EditMeetupCommand
        {
            get
            {
                return editMeetupCommand ??
                    (editMeetupCommand = new RelayCommand(obj =>
                    {
                        if (InputMeetupDescription != null && InputMeetupAddress!= null)
                        {
                            try
                            {
                                Meetup meetup = obj as Meetup;
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                client.DeleteMeetup(meetup);
                                Meetup newMeetup = client.AddMeetup(InputMeetupAddress, InputMeetupDescription, CurrentUser);
                                if (newMeetup != null)
                                {
                                    Meetups.Insert(0, newMeetup);
                                    RefreshMeetups();
                                    Meetups.Remove(meetup);
                                    InputMeetupAddress = "";
                                    InputMeetupDescription = "";
                                }
                                else
                                {
                                    MessageBox.Show("Попробуйте еще раз.");
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка. Попробуйте еще раз.");
                            }
                        }

                    }));

            }
        }

        private RelayCommand removeMeetupCommand;
        public RelayCommand RemoveMeetupCommand
        {
            get
            {
                return removeMeetupCommand ??
                (removeMeetupCommand = new RelayCommand(obj =>
                {
                    Meetup meetup = obj as Meetup;
                    if (meetup != null)
                    {
                        try
                        {
                            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                            client.DeleteMeetup(meetup);
                            Meetups.Remove(meetup);
                            RefreshMeetups();
                            InputMeetupAddress = "";
                            InputMeetupDescription = "";
                        }
                        catch
                        {

                        }
                    }
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
