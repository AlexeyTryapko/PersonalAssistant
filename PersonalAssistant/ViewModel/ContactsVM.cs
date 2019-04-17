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
    class ContactsVM : INotifyPropertyChanged, IServicePersonalAssistantCallback
    {

        private IMainWindow _MainWindow;

        ServicePersonalAssistantClient client;
        public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsVM(IMainWindow mainWindow, User currentUser)
        {
            _MainWindow = mainWindow;
            CurrentUser = currentUser;

            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Contacts = new ObservableCollection<Contact> { };
            Contact[] contacts_from_db = client.RefreshContacts(CurrentUser.Id);
            for (int i = 0; i < contacts_from_db.Length; i++)
                Contacts.Add(contacts_from_db[i]);
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

        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                if (SelectedContact != null)
                {
                    InputContactName = SelectedContact.Name;
                    InputContactEmail = SelectedContact.Email;
                }
                else
                {
                    InputContactName = "";
                    InputContactEmail = "";
                }

                OnPropertyChanged("SelectedContact");
            }
        }

        private string inputContactName;
        public string InputContactName
        {
            get { return inputContactName; }
            set
            {
                inputContactName = value;
                OnPropertyChanged("InputContactName");
            }
        }

        private string inputContactEmail;
        public string InputContactEmail
        {
            get { return inputContactEmail; }
            set
            {
                inputContactEmail = value;
                OnPropertyChanged("InputContactEmail");
            }
        }

        public void RefreshContacts()
        {
            Contacts = new ObservableCollection<Contact> { };
            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Contact[] contacts_from_db = client.RefreshContacts(CurrentUser.Id);
            for (int i = 0; i < contacts_from_db.Length; i++)
                Contacts.Add(contacts_from_db[i]);
            OnPropertyChanged("Contacts");
        }

        private RelayCommand addContactCommand;
        public RelayCommand AddContactCommand
        {
            get
            {
                return addContactCommand ??
                    (addContactCommand = new RelayCommand(obj =>
                    {
                        if (InputContactName != null && InputContactEmail != null)
                        {
                            try
                            {
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                Contact newContact = client.AddContact(InputContactName, InputContactEmail, CurrentUser);
                                if (newContact != null)
                                {
                                    Contacts.Insert(0, newContact);
                                    RefreshContacts();
                                    InputContactName = "";
                                    InputContactEmail = "";
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

        private RelayCommand editContactCommand;
        public RelayCommand EditContactCommand
        {
            get
            {
                return editContactCommand ??
                    (editContactCommand = new RelayCommand(obj =>
                    {
                        if (InputContactName != null && InputContactEmail != null)
                        {
                            try
                            {
                                Contact contact= obj as Contact;
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                client.DeleteContact(contact);
                                Contact newContact = client.AddContact(InputContactName, InputContactEmail, CurrentUser);
                                if (newContact != null)
                                {
                                    Contacts.Insert(0, newContact);
                                    RefreshContacts();
                                    Contacts.Remove(newContact);
                                    InputContactName = "";
                                    InputContactEmail = "";
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

        private RelayCommand removeContactCommand;
        public RelayCommand RemoveContactCommand
        {
            get
            {
                return removeContactCommand ??
                (removeContactCommand = new RelayCommand(obj =>
                {
                    Contact contact = obj as Contact;
                    if (contact != null)
                    {
                        try
                        {
                            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                            client.DeleteContact(contact);
                            Contacts.Remove(contact);
                            RefreshContacts();
                            InputContactName = "";
                            InputContactEmail = "";
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
