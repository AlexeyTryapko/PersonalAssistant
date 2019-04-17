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
    public class NotesVM : INotifyPropertyChanged, IServicePersonalAssistantCallback
    {

        private IMainWindow _MainWindow;

        ServicePersonalAssistantClient client;

        public NotesVM(IMainWindow mainWindow, User currentUser)
        {
            _MainWindow = mainWindow;

            CurrentUser = currentUser;

            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Notes = new ObservableCollection<Note> { };
            Note[] notes_from_db = client.RefreshNotes(CurrentUser.Id);
            for (int i = 0; i < notes_from_db.Length; i++)
                Notes.Add(notes_from_db[i]);
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

        private Note selectedNote;
        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                if(SelectedNote != null)
                {
                    InputNoteMain = SelectedNote.NoteMain;
                    InputNoteTitle = SelectedNote.NoteTitle;
                }else
                {
                    InputNoteMain = "";
                    InputNoteTitle = "";
                }

                OnPropertyChanged("SelectedNote");
            }
        }

        private string inputNoteTitle;
        public string InputNoteTitle
        {
            get { return inputNoteTitle; }
            set
            {
                inputNoteTitle = value;
                OnPropertyChanged("InputNoteTitle");
            }
        }

        private string inputNoteMain;
        public string InputNoteMain
        {
            get { return inputNoteMain; }
            set
            {
                inputNoteMain = value;
                OnPropertyChanged("InputNoteMain");
            }
        }

        private RelayCommand addNoteCommand;
        public RelayCommand AddNoteCommand
        {
            get
            {
                return addNoteCommand ??
                    (addNoteCommand = new RelayCommand(obj =>
                    {
                        if (InputNoteTitle != null && InputNoteMain != null)
                        {
                            try
                            {
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                Note newNote = client.AddNote(InputNoteTitle, InputNoteMain, CurrentUser);
                                if (newNote != null)
                                {
                                    Notes.Insert(0, newNote);
                                    RefreshNotes();
                                    InputNoteTitle = "";
                                    InputNoteMain = "";
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

        private RelayCommand editNoteCommand;
        public RelayCommand EditNoteCommand
        {
            get
            {
                return editNoteCommand ??
                    (editNoteCommand = new RelayCommand(obj =>
                    {
                        if (InputNoteTitle != null && InputNoteMain != null)
                        {
                            try
                            {
                                Note note = obj as Note;
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                client.DeleteNote(note);
                                Note newNote = client.AddNote(InputNoteTitle, InputNoteMain, CurrentUser);
                                if (newNote != null)
                                {
                                    Notes.Insert(0, newNote);
                                    RefreshNotes();
                                    Notes.Remove(note);
                                    InputNoteTitle = "";
                                    InputNoteMain = "";
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

        private RelayCommand removeNoteCommand;
        public RelayCommand RemoveNoteCommand
        {
            get
            {
                return removeNoteCommand ??
                (removeNoteCommand = new RelayCommand(obj =>
                {
                    Note note = obj as Note;
                    if (note != null)
                    {
                        try
                        {
                                client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
                                client.DeleteNote(note);
                                Notes.Remove(note);
                                RefreshNotes();
                            InputNoteTitle = "";
                            InputNoteMain = "";
                        }
                        catch
                        {

                        }
                    }
                }));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public void RefreshNotes()
        {
            Notes = new ObservableCollection<Note> { };
            client = new ServicePersonalAssistantClient(new System.ServiceModel.InstanceContext(this));
            Note[] notes_from_db = client.RefreshNotes(CurrentUser.Id);
            for (int i = 0; i < notes_from_db.Length; i++)
                    Notes.Add(notes_from_db[i]);
            OnPropertyChanged("Notes");
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
