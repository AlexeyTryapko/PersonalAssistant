using PersonalAssistantWCF.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Logger
{
    class UserLogger
    {
        ServicePersonalAssistant service;
        public void printAll(User[] users)
        {
            Console.WriteLine("All USERS\n");
            foreach (User user in users) Console.WriteLine(user.ToString());
        }

        public void print(User user)
        {
            Console.WriteLine(user.ToString());
        }
    }
}
