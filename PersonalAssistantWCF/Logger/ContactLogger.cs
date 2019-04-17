using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Logger
{

    class ContactLogger
    {
        ServicePersonalAssistant service;
        public void printAll(User user)
        {
            service = new ServicePersonalAssistant();
            Contact[] contacts = service.RefreshContacts(user.Id);
            Console.WriteLine("\t CONTACTS \n");
            foreach(Contact contact in contacts)
            {
                Console.WriteLine(contact.ToString());
            }

        }
    }
}
