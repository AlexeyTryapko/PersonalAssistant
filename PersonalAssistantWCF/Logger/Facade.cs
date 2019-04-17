using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Logger
{
    class Facade
    {
        UserLogger userLoger;
        ContactLogger contactLogger;
        MeetupLogger meetupLogger;
        NoteLogger noteLogger;

        public Facade(UserLogger ul, ContactLogger cl, MeetupLogger ml, NoteLogger nl)
        {
            userLoger = ul;
            contactLogger = cl;
            meetupLogger = ml;
            noteLogger = nl;
        }

        public void initializeSystem(User[] users)
        {
            userLoger.printAll(users);
        }

        public void printUserData(User user)
        {
            userLoger.print(user);
            contactLogger.printAll(user);
            meetupLogger.printAll(user);
            noteLogger.printAll(user);
        }
    }
}
