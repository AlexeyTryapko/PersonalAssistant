using PersonalAssistantWCF.Entity;
using PersonalAssistantWCF.Strategy.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Strategy.Classes
{
    class TXTExport : IExport
    {
        ServicePersonalAssistant service;
        public void ExportData(User user)
        {
            service = new ServicePersonalAssistant();
            Note[] notes = service.RefreshNotes(user.Id);
            string notesSTR = "\tNOTES\n" + string.Join("\n",  Array.ConvertAll<object, string>(notes, Convert.ToString));
            Meetup[] meetups = service.RefreshMeetups(user.Id);
            string meetupsSTR = "\tMEETUPS\n" + string.Join("\n", Array.ConvertAll<object, string>(meetups, Convert.ToString));
            Contact[] contacts = service.RefreshContacts(user.Id);
            string contactsSTR = "\tCONTACTS\n" + string.Join("\n", Array.ConvertAll<object, string>(contacts, Convert.ToString));
            string[] res = { notesSTR, meetupsSTR, contactsSTR };

            string docPath =
              @"D:\Обучение\3 курс\3 курс 1 сем\Тряпко\TRPZ\trpzlab\PersonalAssistant\PersonalAssistantData.txt";

            using (StreamWriter file = new StreamWriter(docPath))
            {
                foreach (string line in res)
                {
                    Console.WriteLine(line);
                        file.WriteLine(line);
                }
            }
        }
    }
}
