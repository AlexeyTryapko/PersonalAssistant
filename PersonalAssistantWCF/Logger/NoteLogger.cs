using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Logger
{
    class NoteLogger
    {
        ServicePersonalAssistant service;
        public void printAll(User user)
        {
            service = new ServicePersonalAssistant();
            Note[] notes = service.RefreshNotes(user.Id);
            Console.WriteLine("\t NOTES \n");

            foreach (Note note in notes)
            {
                Console.WriteLine(note.ToString());
            }
        }
    }
}
