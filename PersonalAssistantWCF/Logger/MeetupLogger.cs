using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Logger
{
    class MeetupLogger
    {
        ServicePersonalAssistant service;
        public void printAll(User user)
        {
            service = new ServicePersonalAssistant();
            Meetup[] meetups = service.RefreshMeetups(user.Id);
            Console.WriteLine("\t MEETUPS \n");

            foreach (Meetup meetup in meetups)
            {
                Console.WriteLine(meetup.ToString());
            }
        }
    }
}
